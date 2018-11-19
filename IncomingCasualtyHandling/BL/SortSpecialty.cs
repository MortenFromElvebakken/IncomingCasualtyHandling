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
    public class SortSpecialty : ISortSpecialty
    {
        private ILoadConfigurationSettings LoadXMLSettings;
        private readonly List<Specialty> specialtiesList;
        private IOverviewView_Model _overviewView_Model;
        private IDetailView_Model _detailView_Model;
        private IMainView_Model _mainView_Model;

        public SortSpecialty(ILoadConfigurationSettings _loadXMLSettings,IOverviewView_Model overviewView_Model,IDetailView_Model detailView_Model, IMainView_Model mainview_Model, ISortETA sortEta)
        {
            sortEta.SortedListReady += SortForSpecialty;
            LoadXMLSettings = _loadXMLSettings;
            specialtiesList = new List<Specialty>(LoadXMLSettings.SpecialtiesList);  //Copy of list from xml
            _overviewView_Model = overviewView_Model;
            _detailView_Model = detailView_Model;
            _mainView_Model = mainview_Model;
        }
        public void SortForSpecialty(List<PatientModel> listOfPatients)
        {
            //Group by specialty, all the results
            var results = listOfPatients.GroupBy(p => p.Specialty).ToList();

            var _tempListe = new List<List<PatientModel>>();
            var _listWithUnknownSpecialty = new List<PatientModel>();
            foreach (var specialtyResultList in results)
            {
                int counter = 0;
                bool knownSpecialty = false;
                foreach (var specialty in specialtiesList)
                {
                      
                    if (specialtyResultList.Key == specialty.Name)
                    {
                        specialty.Amount = specialtyResultList.Count();
                        specialty.ShowAs = Visibility.Visible;
                        specialtiesList[counter] = specialty;
                        var testList = specialtyResultList.ToList();
                        //testList.Sort((a, b) => a.ETA.CompareTo(b.ETA));
                        testList = testList.OrderBy(p => p.ETA).ThenBy(p => p.Name).ToList();
                        _tempListe.Add(testList);
                        knownSpecialty = true;
                        break;
                    }
                   
                    counter++;

                }

                // If the specialty is not in the list, it is unknown to the system and put in the unknown specialty list
                if (!knownSpecialty)
                {
                    // Find the unknownSpecialty and update it's properties
                    var unknownSpecialty = specialtiesList.Find(n => n.Name == "Unknown");
                    unknownSpecialty.Amount = unknownSpecialty.Amount + specialtyResultList.Count();
                    unknownSpecialty.ShowAs = Visibility.Visible;

                    // Set the specialty of the patients to "Unknown" in the system
                    foreach (var unknownSpecialtyPatient in specialtyResultList)
                    {
                        unknownSpecialtyPatient.Specialty = "Unknown";
                    }

                    // Add the patients to the list
                    _listWithUnknownSpecialty.AddRange(specialtyResultList.ToList());
                    
                }

            }

            // Sort the list with patients with unknown specialty if any
            if (_listWithUnknownSpecialty.Count != 0)
            {
                _listWithUnknownSpecialty.Sort((a, b) => a.ETA.CompareTo(b.ETA));
                // Add the unknown list to the list with lists of patients
                _tempListe.Add(_listWithUnknownSpecialty);
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
