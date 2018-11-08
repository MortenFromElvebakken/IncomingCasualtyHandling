using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.BL
{
    class SortETA
    {
        private OverviewViewModel _overviewViewModel;
        private DetailViewModel _detailViewModel;
        public SortETA(OverviewViewModel overviewViewModel, DetailViewModel detailViewModel)
        {
            _overviewViewModel = overviewViewModel;
            _detailViewModel = detailViewModel;
        }
        public void SortForETA(List<PatientModel> listOfPatients)
        {
            List<PatientModel> SortedETAList = listOfPatients.OrderBy(o => o.ETA).ToList();
            ETA nextEta = new ETA
            {
                AbsoluteTime = SortedETAList[0].ETA.ToShortTimeString(),
                RelativeTime = SortedETAList[0].ETA.ToShortTimeString()

            };
            _overviewViewModel.Eta = nextEta;
            //Læg listen af sorterede eta patienter over i model

        }
    }
}
