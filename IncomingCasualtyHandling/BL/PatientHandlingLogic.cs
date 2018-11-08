using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL;
using IncomingCasualtyHandling.DAL.Interface;

namespace IncomingCasualtyHandling.BL
{
     class PatientHandlingLogic : IObserver
     {
        private List<PatientModel> listOfPatients;
         private SortETA sortEta;
         private SortSpecialty sortSpecialty;
         private SortTriage sortTriage;

         public PatientHandlingLogic(SortETA _sortEta, SortSpecialty _sortSpecialty, SortTriage _sortTriage)
         {
             sortEta = _sortEta;
             sortSpecialty = _sortSpecialty;
             sortTriage = _sortTriage;

         }

        
        public void Update(List<PatientModel> s)
        {
            listOfPatients = s;
            sortEta.SortForETA(listOfPatients);
            sortSpecialty.SortForSpecialty(listOfPatients);
            sortTriage.SortForTriage(listOfPatients);
        }
    }
}
