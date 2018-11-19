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

        private ISortETA _sortEta;

        string unknownTriageName = "TriageUnknown";

        public SortTriage(ILoadConfigurationSettings _loadXMLSettings, IOverviewView_Model overviewView_Model, IDetailView_Model detailView_Model, IMainView_Model mainView_Model, ISortETA sortEta)
        {
            _sortEta = sortEta;
            _sortEta.SortedListReady += SortForTriage;
            LoadXMLSettings = _loadXMLSettings;
            TriageList = new List<Triage>(LoadXMLSettings.TriageList);  //To get a copy of the list loaded from XML-file
            // Create an unknown triage
            Triage unknownTriage = new Triage();
            unknownTriage.Name = unknownTriageName;
            unknownTriage.Colour = "#cecece";
            unknownTriage.Amount = 0;
            unknownTriage.ShowAs = Visibility.Collapsed;
            TriageList.Add(unknownTriage);

            _overviewView_Model = overviewView_Model;
            _detailView_Model = detailView_Model;
            _mainView_Model = mainView_Model;
        }

        public void SortForTriage(List<PatientModel> listOfPatients)
        {
            // Create a list of lists of patients
            List<List<PatientModel>> listOfPatientLists = new List<List<PatientModel>>();
            List<PatientModel> listWithUnknownTriage = new List<PatientModel>();
            var results = listOfPatients.GroupBy(p => p.Triage);
            foreach (var triageResultList in results)
            {
                int counter = 0;
                // Bolean for whether the triage is known
                bool knownTriage = false;
                foreach (var triage in TriageList)
                {
                    // Check the triage list with patients to the configuration file triages
                    // If the triage matches:
                    if (triageResultList.Key == triage.Name)
                    {
                        // Set the amount of patients
                        triage.Amount = triageResultList.Count();
                        // Set visibility to visible, as there are patients with that triage
                        triage.ShowAs = Visibility.Visible;
                        // Create a list from the LINQ statement
                        var patientList = triageResultList.ToList();
                        //patientList.Sort((a, b) => a.ETA.CompareTo(b.ETA));
                        // Add the list to the list of patient lists
                        listOfPatientLists.Add(patientList);
                        // As the triage matched the configuration file triage, the triage is known
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
                    listWithUnknownTriage.AddRange(triageResultList.ToList());

                }
            }

            // Sort the list with patients with unknown triage if any
            if (listWithUnknownTriage.Count != 0)
            {
                // Nedenstående linje giver vel en cirkulær afhængighed? Kan i hvert fald ikke teste, da den jo er mocket ud
                //listWithUnknownTriage = _sortEta.SortListOnEta(listWithUnknownTriage);

                // Så for nu er det duplikering af kode:
                listWithUnknownTriage = listWithUnknownTriage.OrderBy(p => p.ETA).ThenBy(p => p.Name).ToList();
                var _patientsWithoutEta = new List<PatientModel>();
                int _range = 0;
                foreach (var patient in listWithUnknownTriage)
                {
                    // IF the patient's ETA matches DateTime.MinValue it means, that no ETA is set
                    if (patient.ETA == DateTime.MinValue)
                    {
                        // Save the patient in the no-ETA list and save the index
                        _patientsWithoutEta.Add(patient);
                        _range = ++_range;
                    }
                }

                // When all patients have been checked, move the patients without ETAs to the end of the list
                // by adding them and removing their old placements
                // IF there are any patients without ETA
                if (_patientsWithoutEta.Count != 0)
                {

                    listWithUnknownTriage.AddRange(_patientsWithoutEta);
                    listWithUnknownTriage.RemoveRange(0, _range);
                }

                //listWithUnknownTriage.Sort((a, b) => a.ETA.CompareTo(b.ETA));
                // Add the unknown list to the list with lists of patients
                listOfPatientLists.Add(listWithUnknownTriage);
            }

            _mainView_Model.ListOfTriages = TriageList;
            _detailView_Model.ListOfTriages = TriageList;
            _detailView_Model.ListOfTriagePatientLists = listOfPatientLists;

        }
    }
}
