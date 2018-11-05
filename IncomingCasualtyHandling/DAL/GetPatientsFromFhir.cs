using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using IncomingCasualtyHandling.BL.Models;

namespace IncomingCasualtyHandling.DAL
{
    internal class GetPatientsFromFhir
    {
        private string fhirServerURL;
        private string hospitalShortName;
        private FhirClient client;
        private SerialiseToPatient stp;
        private GetPatientsFromFhir fhirPatients;
        private LoadConfigurationSettings lcs;

        public GetPatientsFromFhir(LoadConfigurationSettings _lcs)
        {


            fhirServerURL = lcs.ReturnServerName();
            hospitalShortName = lcs.ReturnHospitalShortName();
            client = new FhirClient(fhirServerURL);
            stp = new SerialiseToPatient();
            
        }

        private List<OurPatient> GetAllPatients()
        {
            
            SearchParams sParameters = new SearchParams();
            sParameters.Add("active", "true");
            //sParameters.Add("family", "Doe");
            //sParameters.Add("Valuestring", "AUH");

            //Her er parametrene lagt ind, så det er disse en query er bygget på. De er meget case sensitive
            
            //http://docs.simplifier.net/fhirnetapi/client/search.html
            
            List<OurPatient> test = new List<OurPatient>();

            var result = client.Search<Patient>(sParameters);
            //Result er en bundle med "pages" i, hvor hver page loades med 10 patienter i. Hvis der er flere end 10 læses de 10 første og
            //går til næste side
            while (result !=null)
            {
                foreach (var x in result.Entry)
                {
                    var testpatient = (Patient)x.Resource;
                    OurPatient op = stp.returnPatient(testpatient);
                    test.Add(op);
                }
                result = client.Continue(result,PageDirection.Next);
            }
             
            return test;
            
              
        }
    }
}
