using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.BL
{
    public class SortingListOnETA : ISortingListOnETA
    {
        private List<PatientModel> _patientsWithoutETA = new List<PatientModel>();
        private int _range;

        public void SortListOnETA(List<PatientModel> listToSort)
        {
            _range = 0;
            listToSort = listToSort.OrderBy(p => p.ETA).ThenBy(p => p.Name).ToList();
            foreach (var patient in listToSort)
            {
                // IF the patient's ETA matches DateTime.MinValue it means, that no ETA is set
                if (patient.ETA == DateTime.MinValue)
                {
                    // Save the patient in the no-ETA list and save the index
                    _patientsWithoutETA.Add(patient);
                    _range = ++_range;
                }
            }

            // When all patients have been checked, move the patients without ETAs to the end of the list
            // by adding them and removing their old placements
            // IF there are any patients without ETA
            if (_patientsWithoutETA.Count != 0)
            {

                listToSort.AddRange(_patientsWithoutETA);
                listToSort.RemoveRange(0, _range);
            }


            OnSortedListReady(new PatientListEventArgs(listToSort));
        }

        private void OnSortedListReady(PatientListEventArgs patientList)
        {
            var handler = SortedListReady;
            handler?.Invoke(this, patientList);
        }

        public event EventHandler<PatientListEventArgs> SortedListReady;
    }

    
}
