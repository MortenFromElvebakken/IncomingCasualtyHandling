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
            
            newPatientModel.Name = newEntry.Name[0].Text ?? "John Doe";
            newPatientModel.Gender = newEntry.Gender ?? AdministrativeGender.Unknown;
            try
            {
                newPatientModel.CPR = newEntry.Identifier[0].Value;
            }
            catch (Exception e)
            {
                newPatientModel.CPR = "*E_CPR";
            }
            
            try
            {
                newPatientModel.ToHospital = newEntry.Identifier[1].Value;

            }
            catch (Exception e)
            {
                newPatientModel.ToHospital = "Unknown";
            }

            try
            {
                newPatientModel.FromDestination = newEntry.Identifier[2].Value;
            }
            catch (Exception e)
            {
                newPatientModel.FromDestination = "Unknown";
            }
            

            newPatientModel.Triage =
                newEntry.GetStringExtension("http://www.example.com/triagetest") ?? "Unknown";
            newPatientModel.Specialty =
                newEntry.GetStringExtension("http://www.example.com/SpecialtyTest") ?? "Unknown";
            newPatientModel.ETA = Convert.ToDateTime(newEntry.GetExtension("http://www.example.com/datetimeTest").Value.ToString());

            newPatientModel.Age = CalculateAge(Convert.ToDateTime(newEntry.BirthDate));

            //DateTime eta = Convert.ToDateTime(newEntry.GetStringExtension("http://www.example.com/datetimeTest")?? DateTime.MinValue.ToString());
            // Hvad  kan vi gøre hvis der ingen ETA er?
            //newPatientModel.ETA = eta;
            return newPatientModel;
            
        }

        public string CalculateAge(DateTime t)
        {
            //https://stackoverflow.com/questions/9/how-do-i-calculate-someones-age-in-c
            t = t.Date;
            var today = DateTime.Now.Date;
            var age = today.Year - t.Year;
            if (t.AddYears(age)<today)
            {
                age--;
            }
            return age.ToString();
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
