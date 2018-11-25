using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Hl7.Fhir.Model;
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
        private List<Specialty> specialtiesList;
        private IOverviewView_Model _overviewView_Model;
        private IDetailView_Model _detailView_Model;
        private IMainView_Model _mainView_Model;
        private ISortETA _sortEta;

        public SortSpecialty(ILoadConfigurationSettings _loadXMLSettings,IOverviewView_Model overviewView_Model,IDetailView_Model detailView_Model, IMainView_Model mainview_Model, ISortETA sortEta)
        {
            _sortEta = sortEta;
            _sortEta.SortedListReady += SortForSpecialty;
            LoadXMLSettings = _loadXMLSettings;
            
            _overviewView_Model = overviewView_Model;
            _detailView_Model = detailView_Model;
            _mainView_Model = mainview_Model;
        }
        public void SortForSpecialty(List<PatientModel> listOfPatients)
        {
            // Take all patients and group by specialty
            // Gives a "list of groups"
            var results = listOfPatients.GroupBy(p => p.Specialty).ToList();

            // Create a list to contain lists with patients
            var listOfPatientLists = new List<List<PatientModel>>();
            // Create a list to contain patients with an unknown specialty
            var listWithUnknownSpecialty = new List<PatientModel>();
            // Get a copy of list from xml
            specialtiesList = new List<Specialty>(LoadXMLSettings.ReturnSpecialtyList());  

            // Run through the list of lists (based on specialties)
            foreach (var specialtyResultList in results)
            {
                // Boolean to keep track of, whether the specialty is known (recognized from the XML-file) or not
                bool knownSpecialty = false;

                foreach (var specialty in specialtiesList)
                {
                      // If the specialty in the list matches a specialty from the XML-file:
                    if (specialtyResultList.Key == specialty.Name)
                    {
                        // Update the properties of the Specialty object
                        specialty.Amount = specialtyResultList.Count();
                        specialty.ShowAs = Visibility.Visible;

                        // Add the list to the list of lists
                        listOfPatientLists.Add(specialtyResultList.ToList());
                        // Set the boolean to true, as the specialty matched a known specialty
                        knownSpecialty = true;
                        break;
                    }

                }

                // If the specialty is not in the list (based on the XML-file), it is unknown to the system and put in the unknown specialty list
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
                    listWithUnknownSpecialty.AddRange(specialtyResultList.ToList());
                    
                }

            }

            // Sort the list with patients with unknown specialty if any
            if (listWithUnknownSpecialty.Count != 0)
            {
                // Check if an Unknown-specialty list already exists
                if (listOfPatientLists.Exists(l => l.Exists(p => p.Specialty == "Unknown")))
                {
                    // Get the list with unknown specialty patients
                    var tempUnknownPatientList = listOfPatientLists.Find(l => l.Exists(p => p.Specialty == "Unknown"));
                    var index = listOfPatientLists.FindIndex(l => l.Exists(p => p.Specialty == "Unknown"));

                    // Add the unknown specialty patient(s)
                    tempUnknownPatientList.AddRange(listWithUnknownSpecialty);
                    // Sort the unknown specialty list based on ETA again:
                    tempUnknownPatientList = _sortEta.SortListOnEta(tempUnknownPatientList);

                    // Make sure, that the list is in the list of lists with the updated values
                    listOfPatientLists[index] = tempUnknownPatientList;
                }
                else // If the list doesn't already exist
                {
                    listWithUnknownSpecialty = _sortEta.SortListOnEta(listWithUnknownSpecialty);
                    // Add the unknown list to the list with lists of patients
                    listOfPatientLists.Add(listWithUnknownSpecialty);
                }
                
            }


            var amountSortedSpecialtyList = specialtiesList.OrderByDescending(a => a.Amount).ThenBy(a => a.Name).ToList();
            
            _overviewView_Model.ListOfSpecialities = amountSortedSpecialtyList;
            _mainView_Model.Specialty1 = amountSortedSpecialtyList[0]; // Sætter specialty 1 i mainmodel så overviewcomponent kan se den med flest
            _detailView_Model.ListOfSpecialties = amountSortedSpecialtyList;
            _detailView_Model.ListOfSpecialtiesPatientLists = listOfPatientLists;
        }
    }
}
