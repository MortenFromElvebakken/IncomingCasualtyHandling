using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// **********************************
// Group: 2018E73
// Anni Lykke Wilhelmsen, 201509504
// Morten From Elvebakken, 201509095
// **********************************
namespace IncomingCasualtyHandling.DAL.Interface
{
    public interface ILoadData
    {
        event LoadData.PatientUpdateHandler PatientDataReady;
        event LoadData.NoInterNetUpdateHandler NoInternet;
        void SetFhirClientURL(string s);
    }
}
