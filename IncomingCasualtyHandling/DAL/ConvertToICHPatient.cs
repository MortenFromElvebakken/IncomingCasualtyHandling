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
    public class ConvertToICHPatient : IConvertToICHPatient
    {
        public ConvertToICHPatient()
        {

        }

        public ICHPatient ReturnPatient(Patient newEntry)
        {
            ICHPatient newIchPatient = new ICHPatient();

            // Uses ?? Operand to determine if lefthandside of argument is null, in that case use right hand side.
            
            newIchPatient.Name = newEntry.Name[0].Text ?? "John Doe";
            newIchPatient.Gender = newEntry.Gender ?? AdministrativeGender.Unknown;
            try
            {
                newIchPatient.CPR = newEntry.Identifier[0].Value;
            }
            catch (Exception e)
            {
                newIchPatient.CPR = "*E_CPR";
            }
            
            try
            {
                newIchPatient.ToHospital = newEntry.Identifier[1].Value;

            }
            catch (Exception e)
            {
                newIchPatient.ToHospital = "Unknown";
            }

            try
            {
                newIchPatient.FromDestination = newEntry.Identifier[2].Value;
            }
            catch (Exception e)
            {
                newIchPatient.FromDestination = "Unknown";
            }
            

            newIchPatient.Triage =
                newEntry.GetStringExtension("http://www.example.com/triagetest") ?? "Unknown";
            newIchPatient.Specialty =
                newEntry.GetStringExtension("http://www.example.com/SpecialtyTest") ?? "Unknown";
            // Get the ETA. Find the seconds-element and remove it from the ETA that is put on the patient object
            // This is done due to the UI only showing minutes; when the relative time is calculated, this should rely on minutes too
            var eta = Convert.ToDateTime(newEntry.GetExtension("http://www.example.com/datetimeTest").Value.ToString());
            var seconds = eta.Second;
            newIchPatient.ETA = eta.AddSeconds(-seconds);

            newIchPatient.Age = CalculateAge(Convert.ToDateTime(newEntry.BirthDate));
            newIchPatient.LastUpdated = newEntry.Meta.LastUpdated.Value;

            newIchPatient.Age = CalculateAge(Convert.ToDateTime(newEntry.BirthDate));
            newIchPatient.LastUpdated = newEntry.Meta.LastUpdated.Value;
            return newIchPatient;
            
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
            //NotYetImplemented
        }
        public void SetTraumaNote()
        {
            //NotYetImplemented
        }
        public void SetAmbulanceNumber()
        {
            //NotYetImplemented
        }


    }
}
