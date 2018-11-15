using Hl7.Fhir.Model;
using IncomingCasualtyHandling.BL.Object_classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomingCasualtyHandling.DAL.Interface
{
    public interface ISerializeToPatient
    {
        PatientModel ReturnPatient(Patient newEntry);
    }
}
