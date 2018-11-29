using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomingCasualtyHandling.DAL.Interface
{
    public interface ILoadData
    {
        event LoadData.PatientUpdateHandler PatientDataReady;
        event LoadData.NoInterNetUpdateHandler NoInternet;
        void setFhirClientURL(string s);
    }
}
