﻿using System;
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
// **********************************
// Group: 2018E73
// Anni Lykke Wilhelmsen, 201509504
// Morten From Elvebakken, 201509095
// **********************************
namespace IncomingCasualtyHandling.BL
{
    public class SortTriage : ISortTriage
    {
        private ILoadConfigurationSettings LoadXMLSettings;
        public List<Triage> TriageList;
        private IDetailView_Model _detailView_Model;
        private IMainView_Model _mainView_Model;

        private ISortETA _sortEta;

        string unknownTriageName = "TriageUnknown";

        public SortTriage(ILoadConfigurationSettings _loadXMLSettings, IDetailView_Model detailView_Model, IMainView_Model mainView_Model, ISortETA sortEta)
        {
            _sortEta = sortEta;
            _sortEta.SortedListReady += SortForTriage;
            LoadXMLSettings = _loadXMLSettings;
            _detailView_Model = detailView_Model;
            _mainView_Model = mainView_Model;

        }

        public void SortForTriage(List<ICHPatient> listOfPatients)
        {
            // Create a list of lists of patients
            List<List<ICHPatient>> listOfPatientLists = new List<List<ICHPatient>>();
            List<ICHPatient> listWithUnknownTriage = new List<ICHPatient>();
            var results = listOfPatients.GroupBy(p => p.Triage.Name);
            TriageList = new List<Triage>(LoadXMLSettings.ReturnTriageList());
            //TriageList = new List<Triage>(LoadXMLSettings.TriageList);  //To get a copy of the list loaded from XML-file
            // Create an unknown triage
            Triage unknownTriage1 = new Triage();
            unknownTriage1.Name = unknownTriageName;
            unknownTriage1.Colour = "#cecece";
            unknownTriage1.Amount = 0;
            unknownTriage1.ShowAs = Visibility.Collapsed;
            TriageList.Add(unknownTriage1);
            
            foreach (var triageResultList in results)
            {
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

                        // Add the list to the list of patient lists
                        listOfPatientLists.Add(patientList);
                        // As the triage matched the configuration file triage, the triage is known
                        knownTriage = true;
                        break;
                    }

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
                        unknownTriagePatient.Triage.Name = unknownTriageName;
                        unknownTriagePatient.Triage.Colour = unknownTriage.Colour;
                    }

                    // Add the patients to the list
                    listWithUnknownTriage.AddRange(triageResultList.ToList());

                }
            }

            // Check whether there are any patients with a triage unknown to the system
            if (listWithUnknownTriage.Count != 0)
            {
                // Add the patients to the Unknown-triage patient list
                // Check whether the list already exists
                if (listOfPatientLists.Exists(l => l.Exists(p => p.Triage.Name == unknownTriageName)))
                {
                    var tempUnknownPatientList =
                        listOfPatientLists.Find(l => l.Exists(p => p.Triage.Name == unknownTriageName));
                    var index = listOfPatientLists.FindIndex(l => l.Exists(p => p.Triage.Name == unknownTriageName));

                    // Add the unknown triage patients
                    tempUnknownPatientList.AddRange(listWithUnknownTriage);
                    // Sort the patients on ETA again
                    tempUnknownPatientList = _sortEta.SortListOnEta(tempUnknownPatientList);

                    listOfPatientLists[index] = tempUnknownPatientList;

                }
                else // If the list doesn't already exist, add it
                {
                    listWithUnknownTriage = _sortEta.SortListOnEta(listWithUnknownTriage);
                    listOfPatientLists.Add(listWithUnknownTriage);
                }
                
               
            }

            _mainView_Model.ListOfTriages = TriageList;
            _detailView_Model.ListOfTriages = TriageList;
            _detailView_Model.ListOfTriagePatientLists = listOfPatientLists;

        }
    }
}
