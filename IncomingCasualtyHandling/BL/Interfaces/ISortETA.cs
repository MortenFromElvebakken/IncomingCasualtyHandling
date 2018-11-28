using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.BL.Interfaces
{
    public interface ISortETA
    {
        void SortForETA(List<ICHPatient> listOfPatients);
        event SortETA.PatientUpdateHandler SortedListReady;
        List<ICHPatient> SortListOnEta(List<ICHPatient> listToSort);
    }
}
