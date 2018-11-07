using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Models;

namespace IncomingCasualtyHandling.BL
{
    class SortTriage
    {
        public void SortForTriage(List<OurPatient> listOfPatients)
        {
            throw new NotImplementedException();
            //Lav logic så den laver lister baseret på hvor de forskellige triager der kommer fra settings
            //List<OurPatient> SortedTriageList = listOfPatients.Select(o => o.Triage).ToList();
        }
    }
}
