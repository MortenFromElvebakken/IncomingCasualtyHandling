using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.DAL;
using IncomingCasualtyHandling.DAL.Interface;

namespace IncomingCasualtyHandling.BL
{
     class PatientHandlingLogic : IObserver
     {
        private List<OurPatient> listOfPatients;
         private SortETA sortEta;
         private SortSpecialty sortSpecialty;
         private SortTriage sortTriage;

         public PatientHandlingLogic(SortETA _sortEta, SortSpecialty _sortSpecialty, SortTriage _sortTriage)
         {
             sortEta = _sortEta;
             sortSpecialty = _sortSpecialty;
             sortTriage = _sortTriage;

         }

        
        public void Update(List<OurPatient> s)
        {
            listOfPatients = s;
            sortEta.SortForETA(listOfPatients);
            sortSpecialty.sortForSpecialty(listOfPatients);
            sortTriage.SortForTriage(listOfPatients);
        }
    }
}
