using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL;
using IncomingCasualtyHandling.DAL.Interface;

namespace IncomingCasualtyHandling.BL
{
    public class SortETA : ISortETA
    {
        private IOverviewView_Model _overviewViewModel;
        private IDetailView_Model _detailView_Model;
        private IMainView_Model _mainView_Model;
        private ICountTime _countTime;

        // List to hold patients without ETA
        private readonly List<ICHPatient> _patientsWithoutEta = new List<ICHPatient>();
        // Variable to hold the range to "remove" patients without ETA within
        private int _range;

        //Constructor
        public SortETA(IOverviewView_Model overviewView_Model, IDetailView_Model detailView_Model, IMainView_Model mainView_Model, ICountTime countTime, ILoadData receivePatientsFromFhir)
        {

            receivePatientsFromFhir.PatientDataReady += SortForETA;
            _overviewViewModel = overviewView_Model;
            _detailView_Model = detailView_Model;
            _mainView_Model = mainView_Model;
            _countTime = countTime;
        }

        // Method that sorts ETAs
        // Adds a sorted patientList to DetailView_Model
        public void SortForETA(List<ICHPatient> listOfPatients)
        {
            //List<ICHPatient> SortedETAList = listOfPatients.OrderBy(o => o.ETA).ThenBy(n => n.Name).ToList();

            listOfPatients = SortListOnEta(listOfPatients);

            OnSortedListReady(listOfPatients);

            _detailView_Model.ETAPatients = listOfPatients;

            // When the ETA's are sorted, they are send to the CountTime-class to work on relative time
            _countTime.FindRelativeTime(listOfPatients);

        }

        public delegate void PatientUpdateHandler(List<ICHPatient> sortedPatients);
        public event PatientUpdateHandler SortedListReady;

        private void OnSortedListReady(List<ICHPatient> patientList)
        {
            var handler = SortedListReady;
            handler?.Invoke(patientList);
        }

        public List<ICHPatient> SortListOnEta(List<ICHPatient> listToSort)
        {
            _patientsWithoutEta.Clear();
            _range = 0;
            listToSort = listToSort.OrderBy(p => p.ETA).ThenBy(p => p.Name).ToList();
            foreach (var patient in listToSort)
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

                listToSort.AddRange(_patientsWithoutEta);
                listToSort.RemoveRange(0, _range);
            }

            return listToSort;

        }
    }
}
