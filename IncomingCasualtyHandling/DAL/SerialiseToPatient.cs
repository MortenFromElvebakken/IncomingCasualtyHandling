using System;
using System.Collections.Generic;
using System.Linq;
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

            
            OurPatient _newPatient = new OurPatient();
            
            _newPatient.PatientId = newEntry.Identifier[0].Value.ToString();
            _newPatient.GivenName = newEntry.Name[0].GivenElement[0].ToString();
            _newPatient.FamilyName = newEntry.Name[0].Family.ToString();
            _newPatient.Gender = newEntry.Gender.ToString();

            //Logic i forhold til extensions her og lav logic der ser om felter er lig null?

            _newPatient.toHospital = Convert.ToString(newEntry.Extension[0].Value);
            _newPatient.Triage = Convert.ToString(newEntry.Extension[1].Value);
            _newPatient.Specialty = Convert.ToString(newEntry.Extension[2].Value);
            _newPatient.ETA = Convert.ToDateTime(newEntry.Extension[3].Value.ToString());
            


            return _newPatient;
            
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
