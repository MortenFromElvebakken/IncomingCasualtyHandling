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
    class SortSpecialty
    {
        private LoadConfigurationSettingsFromXMLDocument LoadXMLSettings;
        private readonly List<Specialty> specialtiesList;
        private OverviewViewModel _overviewViewModel;
        private DetailViewModel _detailViewModel;
        public SortSpecialty(LoadConfigurationSettingsFromXMLDocument _loadXMLSettings,OverviewViewModel overviewViewModel,DetailViewModel detailViewModel)
        {
            LoadXMLSettings = _loadXMLSettings;
            specialtiesList = new List<Specialty>(LoadXMLSettings.SpecialtiesList);  //kopi af liste fra xml
            _overviewViewModel = overviewViewModel;
            _detailViewModel = detailViewModel;
        }
        public void SortForSpecialty(List<PatientModel> listOfPatients)
        {
            
            var results = listOfPatients.GroupBy(p => p.Specialty).ToList();
            //results.Sort((a,b)=>b.Count() -a.Count());
            //var SortedSpecialtyList = new List<Specialty>();
            
            foreach (var specialtyResultList in results)
            {
                int counter = 0;
                foreach (var specialty in specialtiesList)
                {
                      
                    if (specialtyResultList.Key == specialty.Name)
                    {
                        
                        specialty.Amount = specialtyResultList.Count();
                        specialty.ShowAs = Visibility.Visible;
                        specialtiesList[counter] = specialty;
                        //SortedSpecialtyList.Add(specialty);
                        //model.specialtylist[counter] <--- skal lægge dennne liste (specialtyResultList) ind på den plads der 
                        //passer med den triage dette foreach loop er kommet til.

                        break;
                    }
                    else
                    { //tjek hvorfor det er redundant?
                        //specialtiesList[13].Amount = specialtiesList[13].Amount + specialtyResultList.Count();
                        //specialty.ShowAs = Visibility.Visible;
                        //Logic that handles an unknown specialty, and adds them to the unknown specialty
                    }
                    counter++;

                }

                
            }
            //specialtiesList.RemoveAll(p => p.Amount == 0);
            specialtiesList.Sort((a,b)=>b.Amount -a.Amount);
            _overviewViewModel.ListOfSpecialities = specialtiesList;
        }
    }
}
