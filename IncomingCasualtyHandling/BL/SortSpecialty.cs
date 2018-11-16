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
    public class SortSpecialty
    {
        private ILoadConfigurationSettings LoadXMLSettings;
        private readonly List<Specialty> specialtiesList;
        private OverviewView_Model _overviewView_Model;
        private DetailView_Model _detailView_Model;
        private MainView_Model _mainView_Model;
        public SortSpecialty(ILoadConfigurationSettings _loadXMLSettings,OverviewView_Model overviewView_Model,DetailView_Model detailView_Model, MainView_Model mainview_Model, IGetPatientsFromFHIR RecievePatientsFromFhir)
        {
            RecievePatientsFromFhir.PatientDataReady += SortForSpecialty;
            LoadXMLSettings = _loadXMLSettings;
            specialtiesList = new List<Specialty>(LoadXMLSettings.SpecialtiesList);  //kopi af liste fra xml
            _overviewView_Model = overviewView_Model;
            _detailView_Model = detailView_Model;
            _mainView_Model = mainview_Model;
        }
        public void SortForSpecialty(List<PatientModel> listOfPatients)
        {
            //Group by specialty, all the results
            var results = listOfPatients.GroupBy(p => p.Specialty).ToList();

            var _tempListe = new List<List<PatientModel>>();
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
                        var testList = specialtyResultList.ToList();
                        testList.Sort((a, b) => a.ETA.CompareTo(b.ETA));
                        _tempListe.Add(testList);

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
            //specialtiesList.Sort((a,b)=>b.Amount -a.Amount);
            var FinalList = specialtiesList.OrderByDescending(a => a.Amount).ThenBy(a => a.Name).ToList();
            //specialtiesList.GroupBy(a => a.Amount).OrderBy(a => a.Name);
            _overviewView_Model.ListOfSpecialities = FinalList;
            _mainView_Model.Specialty1 = FinalList[0]; // Sætter specialty 1 i mainmodel så overviewcomponent kan se den med flest
            _detailView_Model.ListOfSpecialties = FinalList;
            _detailView_Model.ListOfSpecialtiesPatientLists = _tempListe;
        }
    }
}
