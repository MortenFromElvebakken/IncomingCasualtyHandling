using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.DAL;

namespace IncomingCasualtyHandling.BL
{
    class SortSpecialty
    {
        private LoadConfigurationSettingsFromXMLDocument LoadXMLSettings;
        private readonly List<Specialty> specialtiesList;
        public SortSpecialty(LoadConfigurationSettingsFromXMLDocument _loadXMLSettings)
        {
            LoadXMLSettings = _loadXMLSettings;
            specialtiesList = new List<Specialty>(LoadXMLSettings.SpecialtiesList);  //kopi af liste fra xml
        }
        public void SortForSpecialty(List<PatientModel> listOfPatients)
        {
            
            var results = listOfPatients.GroupBy(p => p.Specialty);
            foreach (var specialtyResultList in results)
            {
                foreach (var specialty in specialtiesList)
                {
                    //int counter = 0;
                    if (specialtyResultList.Key == specialty.Name)
                    {
                        specialty.Amount = specialtyResultList.Count();
                        specialty.ShowAs = Visibility.Visible;

                        //model.specialtylist[counter] <--- skal lægge dennne liste (specialtyResultList) ind på den plads der 
                        //passer med den triage dette foreach loop er kommet til.

                        break;
                    }
                    else
                    {
                        specialtiesList[13].Amount = specialtiesList[13].Amount + specialtyResultList.Count();
                        specialty.ShowAs = Visibility.Visible;
                        //Logic that handles an unknown specialty, and adds them to the unknown specialty
                    }
                    //counter++;
                }
            }

            var test = specialtiesList.Count;
        }
    }
}
