using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.BL.Interfaces
{
    public interface IMainView_Model
    {
        List<Triage> ListOfTriages { get; set; }
        string CurrentDateTime { get; set; }
        Specialty Specialty1 { get; set; }
        Triage Triage1 { get; set; }
        Triage Triage2 { get; set; }
        Triage Triage3 { get; set; }
        Triage Triage4 { get; set; }
        Triage Triage5 { get; set; }
        Triage Triage6 { get; set; }
        ETA Eta { get; set; }
        event PropertyChangedEventHandler PropertyChanged;
    }
}
