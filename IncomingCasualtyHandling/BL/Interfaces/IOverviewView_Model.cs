using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.BL.Interfaces
{
    public interface IOverviewView_Model
    {
        List<Specialty> ListOfSpecialities { get; set; }
        ETA Eta { get; set; }
    }
}
