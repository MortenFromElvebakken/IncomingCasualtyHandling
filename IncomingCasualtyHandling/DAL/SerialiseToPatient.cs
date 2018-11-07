using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using IncomingCasualtyHandling.BL.Models;


namespace IncomingCasualtyHandling.DAL
{
    internal class SerialiseToPatient
    {
        public SerialiseToPatient()
        {

        }

        public OurPatient returnPatient(Patient newEntry)
        {
            OurPatient newPatient = new OurPatient();

            // Uses ?? Operand to determine if lefthandside of argument is null, in that case use right hand side.
             newPatient.PatientId = newEntry.Identifier[0].Value ?? "*E_CPR";
            newPatient.Name = newEntry.Name[0].Text ?? "John Doe";
            newPatient.Gender = newEntry.Gender ?? AdministrativeGender.Unknown;


            //Logic i forhold til extensions her og lav logic der ser om felter er lig null?
            //_newPatient.ToHospital = Convert.ToString(newEntry.Extension[0].Value);
            //_newPatient.Triage = Convert.ToString(newEntry.Extension[1].Value);
            //_newPatient.Specialty = Convert.ToString(newEntry.Extension[2].Value);
            //_newPatient.ETA = Convert.ToDateTime(newEntry.Extension[3].Value.ToString());

            newPatient.ToHospital =
                newEntry.GetStringExtension("http://www.example.com/hospitalTest") ?? "To hospital went wrong";
            newPatient.Triage =
                newEntry.GetStringExtension("http://www.example.com/triagetest") ?? "Unknown";
            newPatient.Specialty =
                newEntry.GetStringExtension("http://www.example.com/SpecialtyTest") ?? "Unknown";
            newPatient.ETA = 
                Convert.ToDateTime(newEntry.GetExtension("http://www.example.com/datetimeTest")); 
            // Hvad  kan vi gøre hvis der ingen ETA er?
            
            
           
            return newPatient;
            
        }

        //Todo with clinical impression ressource on fhir
        public void setMedicinalNote()
        {
            //Lav kald til database lag der tjekker på patientens id om der ligger en clinical impression?
        }
        public void setTraumaNote()
        {

        }
        public void setAmbulanceNumber()
        {

        }


    }
}
