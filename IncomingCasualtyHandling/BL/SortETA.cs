using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.BL
{
    public class SortETA
    {
        private OverviewViewModel _overviewViewModel;
        private DetailViewModel _detailViewModel;
        private Timer _timer;

        // Constructor
        public SortETA(OverviewViewModel overviewViewModel, DetailViewModel detailViewModel, Timer timer)
        {
            _overviewViewModel = overviewViewModel;
            _detailViewModel = detailViewModel;
            _timer = timer;
        }

        // Method that sorts ETAs
        // Adds a sorted patientList to DetailViewModel
        public void SortForETA(List<PatientModel> listOfPatients)
        {
            List<PatientModel> SortedETAList = listOfPatients.OrderBy(o => o.ETA).ToList();
            FindNearestETA(SortedETAList);
            //ETA nextEta = new ETA
            //{
            //    AbsoluteTime = SortedETAList[0].ETA.ToShortTimeString(),
            //    RelativeTime = SortedETAList[0].ETA.ToShortTimeString()

            //};
            //_overviewViewModel.Eta = nextEta;
            ////Læg listen af sorterede eta patienter over i model

        }

        public void FindNearestETA(List<PatientModel> sortedPatients)
        {
            foreach (var patient in sortedPatients)
            {
                
                if (patient.ETA > DateTime.Now)
                {
                    _timer.CompareETATimeToCurrentTime(patient.ETA);
                    break;
                }
            }

            _timer.CompareETATimeToCurrentTime(sortedPatients.Last().ETA);

            

        }
    }
}
