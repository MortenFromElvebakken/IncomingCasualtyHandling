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
     class SortPatients : IObserver
     {
        private List<OurPatient> listOfPatients;

         public SortPatients()
        {

        }

        
        public void Update(List<OurPatient> s)
        {
            listOfPatients = s;
        }
    }
}
