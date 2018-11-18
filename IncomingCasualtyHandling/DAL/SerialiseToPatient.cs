using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL.Interface;


namespace IncomingCasualtyHandling.DAL
{
    public class SerialiseToPatient : ISerializeToPatient
    {
        public SerialiseToPatient()
        {

        }

        public PatientModel ReturnPatient(Patient newEntry)
        {
            PatientModel newPatientModel = new PatientModel();

            // Uses ?? Operand to determine if lefthandside of argument is null, in that case use right hand side.
            newPatientModel.PatientId = newEntry.Identifier[0].Value ?? "*E_CPR";
            newPatientModel.Name = newEntry.Name[0].Text ?? "John Doe";
            newPatientModel.Gender = newEntry.Gender ?? AdministrativeGender.Unknown;
            //newPatientModel.CPR

            //Logic i forhold til extensions her og lav logic der ser om felter er lig null?
            //_newPatient.ToHospital = Convert.ToString(newEntry.Extension[0].Value);
            //_newPatient.Triage = Convert.ToString(newEntry.Extension[1].Value);
            //_newPatient.Specialty = Convert.ToString(newEntry.Extension[2].Value);
            //_newPatient.ETA = Convert.ToDateTime(newEntry.Extension[3].Value.ToString());

            newPatientModel.ToHospital =
                newEntry.GetStringExtension("http://www.example.com/hospitalTest") ?? "To hospital went wrong";
            newPatientModel.Triage =
                newEntry.GetStringExtension("http://www.example.com/triagetest") ?? "Unknown";
            newPatientModel.Specialty =
                newEntry.GetStringExtension("http://www.example.com/SpecialtyTest") ?? "Unknown";
            newPatientModel.ETA = 
                Convert.ToDateTime(newEntry.GetExtension("http://www.example.com/datetimeTest").Value.ToString()); 
            // Hvad  kan vi gøre hvis der ingen ETA er?
            
            
           
            return newPatientModel;
            
        }

        //Todo with clinical impression ressource on fhir
        public void SetMedicinalNote()
        {
            //Lav kald til database lag der tjekker på patientens id om der ligger en clinical impression?
        }
        public void SetTraumaNote()
        {

        }
        public void SetAmbulanceNumber()
        {

        }


    }
}
