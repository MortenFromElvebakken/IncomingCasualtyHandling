using System;
using System.Collections.Generic;
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
    public class SortETA
    {
        private OverviewView_Model _overviewViewModel;
        private DetailView_Model _detailViewModel;
        private ITimer _timer;


        // Constructor
        public SortETA(OverviewView_Model overviewViewModel, DetailView_Model detailViewModel, ITimer timer, IGetPatientsFromFHIR RecievePatientsFromFhir)
        {
            
            RecievePatientsFromFhir.PatientDataReady += SortForETA;
            _overviewViewModel = overviewViewModel;
            _detailViewModel = detailViewModel;
            _timer = timer;
        }

        // Method that sorts ETAs
        // Adds a sorted patientList to DetailView_Model
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
