using Hl7.Fhir.Model;
using IncomingCasualtyHandling.BL.Object_classes;
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
    public interface IConvertToICHPatient
    {
        ICHPatient ReturnPatient(Patient newEntry);
    }
}
