using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.BL.Interfaces
{
    public interface IOverviewView_Model
    {
        ETA ETA { get; set; }
        List<Specialty> ListOfSpecialities { get; set; }
        int MaximumTriagePatients { get; }
        Specialty Specialty1 { get; set; }
        Specialty Specialty2 { get; set; }
        Specialty Specialty3 { get; set; }
        Specialty Specialty4 { get; set; }
        Specialty Specialty5 { get; set; }
        Specialty Specialty6 { get; set; }
        Specialty Specialty7 { get; set; }
        Specialty Specialty8 { get; set; }
        Specialty Specialty9 { get; set; }
        Specialty Specialty10 { get; set; }
        Specialty Specialty11 { get; set; }
        Specialty Specialty12 { get; set; }
        Specialty Specialty13 { get; set; }
        Specialty Specialty14 { get; set; }
        Specialty Specialty15 { get; set; }
        Specialty Specialty16 { get; set; }
        event PropertyChangedEventHandler PropertyChanged;
    }
}
