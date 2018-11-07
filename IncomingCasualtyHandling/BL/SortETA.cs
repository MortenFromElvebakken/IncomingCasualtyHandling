using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Models;

namespace IncomingCasualtyHandling.BL
{
    class SortETA
    {
        public void SortForETA(List<OurPatient> listOfPatients)
        {
            List<OurPatient> SortedETAList = listOfPatients.OrderBy(o => o.ETA).ToList();
            
        }
    }
}
