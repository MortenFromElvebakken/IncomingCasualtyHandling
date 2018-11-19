using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL;
using IncomingCasualtyHandling.DAL.Interface;

namespace IncomingCasualtyHandling.BL
{
    public class SortTriage
    {
        public ILoadConfigurationSettings LoadXMLSettings;
        public List<Triage> TriageList;
        private IOverviewView_Model _overviewView_Model;
        private IDetailView_Model _detailView_Model;
        private IMainView_Model _mainView_Model;
        public SortTriage(ILoadConfigurationSettings _loadXMLSettings, IOverviewView_Model overviewView_Model, IDetailView_Model detailView_Model, IMainView_Model mainView_Model, IGetPatientsFromFHIR RecievePatientsFromFhir)
        {
            RecievePatientsFromFhir.PatientDataReady += SortForTriage;
            LoadXMLSettings = _loadXMLSettings;
            TriageList = new List<Triage>(LoadXMLSettings.TriageList);  //Gøres for at tage en kopi af den liste der er hentet
            _overviewView_Model = overviewView_Model;
            _detailView_Model = detailView_Model;
            _mainView_Model = mainView_Model;
        }

        public void SortForTriage(List<PatientModel> listOfPatients)
        {
            List<List<PatientModel>> _TempList = new List<List<PatientModel>>();
            var results = listOfPatients.GroupBy(p => p.Triage);
            foreach (var triageResultList in results)
            {
                int counter = 0;
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
                       
                        break;
                    }
                    else
                    {
                        //Logic for unknown triage? maybe
                    }

                    
                }
                counter++;

            }

            _mainView_Model.ListOfTriages = TriageList;
            _detailView_Model.ListOfTriages = TriageList;
            _detailView_Model.ListOfTriagePatientLists = _TempList;
            
        }
    }
}
