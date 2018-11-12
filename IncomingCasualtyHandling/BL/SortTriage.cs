using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL;

namespace IncomingCasualtyHandling.BL
{
    public class SortTriage
    {
        public LoadConfigurationSettingsFromXMLDocument LoadXMLSettings;
        public List<Triage> TriageList;
        private OverviewViewModel _overviewViewModel;
        private DetailViewModel _detailViewModel;
        public SortTriage(LoadConfigurationSettingsFromXMLDocument _loadXMLSettings, OverviewViewModel overviewViewModel, DetailViewModel detailViewModel)
        {
            LoadXMLSettings = _loadXMLSettings;
            TriageList = new List<Triage>(LoadXMLSettings.TriageList);  //Gøres for at tage en kopi af den liste der er hentet
            _overviewViewModel = overviewViewModel;
            _detailViewModel = detailViewModel;
        }

        public void SortForTriage(List<PatientModel> listOfPatients)
        {

            
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


                        break;
                    }
                    else
                    {
                        //Logic for unknown triage? maybe
                    }

                    counter++;
                }

                
            }

            _overviewViewModel.ListOfTriages = TriageList;

            ////Lige nu initialiseres 5 lister med triage - gør dette ud fra triage list hvis muligt, hvor der oprettes
            ////De lister der findes i triagelisten fra configurations fil
            //List<PatientModel> triage1 = new List<PatientModel>();
            //List<PatientModel> triage2 = new List<PatientModel>();
            //List<PatientModel> triage3 = new List<PatientModel>();
            //List<PatientModel> triage4 = new List<PatientModel>();
            //List<PatientModel> triage5 = new List<PatientModel>();


            //    foreach (var patients in listOfPatients)
            //{
            //    if (patients.Triage == TriageList[0].Name)
            //    {
            //        triage1.Add(patients);
            //        TriageList[0].Amount++;
            //        TriageList[0].ShowAs = Visibility.Visible;
            //    }
            //    if (patients.Triage == TriageList[1].Name)
            //    {
            //        triage2.Add(patients);
            //        TriageList[1].Amount++;
            //        TriageList[1].ShowAs = Visibility.Visible;
            //    }
            //    if (patients.Triage == TriageList[2].Name)
            //    {
            //        triage3.Add(patients);
            //        TriageList[2].Amount++;
            //        TriageList[2].ShowAs = Visibility.Visible;
            //    }
            //    if (patients.Triage == TriageList[3].Name)
            //    {
            //        triage4.Add(patients);
            //        TriageList[3].Amount++;
            //        TriageList[3].ShowAs = Visibility.Visible;
            //    }
            //    if (patients.Triage == TriageList[4].Name)
            //    {
            //        triage5.Add(patients);
            //        TriageList[4].Amount++;
            //        TriageList[4].ShowAs = Visibility.Visible;
            //    }

            //}

        }
    }
}
