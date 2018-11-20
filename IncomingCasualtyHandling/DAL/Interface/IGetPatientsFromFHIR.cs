using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomingCasualtyHandling.DAL.Interface
{
    public interface IGetPatientsFromFHIR
    {
        event GetPatientsFromFhir.PatientUpdateHandler PatientDataReady;
        void setFhirClientURL(string s);
    }
}
