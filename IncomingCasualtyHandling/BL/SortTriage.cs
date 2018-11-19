using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL;
using IncomingCasualtyHandling.DAL.Interface;

namespace IncomingCasualtyHandling.BL
{
    public class SortTriage : ISortTriage
    {
        public ILoadConfigurationSettings LoadXMLSettings;
        public List<Triage> TriageList;
        private IOverviewView_Model _overviewView_Model;
        private IDetailView_Model _detailView_Model;
        private IMainView_Model _mainView_Model;

        string unknownTriageName = "TriageUnknown";

        public SortTriage(ILoadConfigurationSettings _loadXMLSettings, IOverviewView_Model overviewView_Model, IDetailView_Model detailView_Model, IMainView_Model mainView_Model, IGetPatientsFromFHIR ReceivePatientsFromFhir)
        {
            ReceivePatientsFromFhir.PatientDataReady += SortForTriage;
            LoadXMLSettings = _loadXMLSettings;
            TriageList = new List<Triage>(LoadXMLSettings.TriageList);  //To get a copy of the list loaded from XML-file
            // Create an unknown triage
            Triage unknownTriage = new Triage();
            unknownTriage.Name = unknownTriageName;
            unknownTriage.Colour = "#cecece";
            TriageList.Add(unknownTriage);

            _overviewView_Model = overviewView_Model;
            _detailView_Model = detailView_Model;
            _mainView_Model = mainView_Model;
        }

        public void SortForTriage(List<PatientModel> listOfPatients)
        {
            List<List<PatientModel>> _TempList = new List<List<PatientModel>>();
            List<PatientModel> _listWithUnknownTriage = new List<PatientModel>();
            var results = listOfPatients.GroupBy(p => p.Triage);
            foreach (var triageResultList in results)
            {
                int counter = 0;
                // Bolean for whether the triage is known
                bool knownTriage = false;
                foreach (var triage in TriageList)
                {
                    //int counter = 0;
                    if (triageResultList.Key == triage.Name)
                    {
                        triage.Amount = triageResultList.Count();
                        triage.ShowAs = Visibility.Visible;
                        var testList = triageResultList.ToList();
                        testList.Sort((a, b) => a.ETA.CompareTo(b.ETA));
                        _TempList.Add(testList);
                        knownTriage = true;
                        break;
                    }

                    counter++;
                }

                // If the triage is not in the list, it is unknown to the system and put in the unknown triage list
                if (!knownTriage)
                {
                    // Find the unknownTriage and update it's properties
                    var unknownTriage = TriageList.Find(n => n.Name == unknownTriageName);
                    unknownTriage.Amount = unknownTriage.Amount + triageResultList.Count();
                    unknownTriage.ShowAs = Visibility.Visible;

                    // Set the triage of the patients to "Unknown" in the system
                    foreach (var unknownTriagePatient in triageResultList)
                    {
                        unknownTriagePatient.Triage = unknownTriageName;
                    }

                    // Add the patients to the list
                    _listWithUnknownTriage.AddRange(triageResultList.ToList());

                }
            }

            // Sort the list with patients with unknown triage if any
            if (_listWithUnknownTriage.Count != 0)
            {
                _listWithUnknownTriage.Sort((a, b) => a.ETA.CompareTo(b.ETA));
                // Add the unknown list to the list with lists of patients
                _TempList.Add(_listWithUnknownTriage);
            }

            _mainView_Model.ListOfTriages = TriageList;
            _detailView_Model.ListOfTriages = TriageList;
            _detailView_Model.ListOfTriagePatientLists = _TempList;

        }
    }
}
