using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.BL.Interfaces
{
    public interface IMainView_Model
    {
        #region Triage properties
        List<Triage> ListOfTriages { get; set; }
        Triage Triage1 { get; set; }
        Triage Triage2 { get; set; }
        Triage Triage3 { get; set; }
        Triage Triage4 { get; set; }
        Triage Triage5 { get; set; }
        #endregion

        Specialty Specialty1 { get; set; }
    }
}
