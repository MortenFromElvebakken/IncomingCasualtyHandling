using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.DAL.Interface
{
    public interface ILoadConfigurationSettings
    {
        string ServerName { get;}
        string HospitalShortName { get;}
        List<Specialty> SpecialtiesList { get; set; }
        List<Triage> TriageList { get; set; }
    }
}
