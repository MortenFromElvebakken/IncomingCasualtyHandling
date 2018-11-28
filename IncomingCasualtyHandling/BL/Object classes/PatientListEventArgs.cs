using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomingCasualtyHandling.BL.Object_classes
{
    public class PatientListEventArgs
    {
        public PatientListEventArgs(List<ICHPatient> sortedList)
        {
            this.SortedList = sortedList;
        }

        public List<ICHPatient> SortedList { get; }
    }
}
