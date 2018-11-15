using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private OverviewView_Model _overviewViewModel;
        private DetailView_Model _detailViewModel;
        public SortTriage(ILoadConfigurationSettings _loadXMLSettings, OverviewView_Model overviewViewModel, DetailView_Model detailViewModel, IGetPatientsFromFHIR RecievePatientsFromFhir)
        {
            RecievePatientsFromFhir.PatientDataReady += SortForTriage;
            LoadXMLSettings = _loadXMLSettings;
            TriageList = new List<Triage>(LoadXMLSettings.TriageList);  //Gøres for at tage en kopi af den liste der er hentet
            _overviewViewModel = overviewViewModel;
            _detailViewModel = detailViewModel;
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
                        
                        //model.triagelist[counter] <--- skal lægge dennne liste (triageResultList) ind på den plads der 
                        //passer med den triage dette foreach loop er kommet til.
                        _TempList.Add(triageResultList.ToList());
                       
                        break;
                    }
                    else
                    {
                        //Logic for unknown triage? maybe
                    }

                    
                }
                counter++;

            }

            _overviewViewModel.ListOfTriages = TriageList;
            _detailViewModel.ListOfTriages = TriageList;
            _detailViewModel.ListOfTriagePatientLists = _TempList;
            
        }
    }
}
