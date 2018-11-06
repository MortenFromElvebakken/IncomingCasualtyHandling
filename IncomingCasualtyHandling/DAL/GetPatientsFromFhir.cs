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
    internal class GetPatientsFromFhir: SubjectObserverPatients
    {
        private string fhirServerURL;
        private string hospitalShortName;
        private FhirClient client;
        private SerialiseToPatient serialisePatient;
        private LoadConfigurationSettings loadConfigSettings;

        public GetPatientsFromFhir(LoadConfigurationSettings _lcs)
        {
            fhirServerURL = loadConfigSettings.ReturnServerName();
            hospitalShortName = loadConfigSettings.ReturnHospitalShortName();
            client = new FhirClient(fhirServerURL);
            serialisePatient = new SerialiseToPatient();
            GetAllPatients();
            
        }

        private void GetAllPatients()
        {
            
            SearchParams sParameters = new SearchParams();
            sParameters.Add("active", "true");
            //sParameters.Add("family", "Doe");
            sParameters.Add("Valuestring", hospitalShortName);

            //Her er parametrene lagt ind, så det er disse en query er bygget på. De er meget case sensitive
            
            //http://docs.simplifier.net/fhirnetapi/client/search.html
            
            List<OurPatient> listOfPatients = new List<OurPatient>();

            var resultOfSearch = client.Search<Patient>(sParameters);
            //Result er en bundle med "pages" i, hvor hver page loades med 10 patienter i. Hvis der er flere end 10 læses de 10 første og
            //går til næste side
            while (resultOfSearch != null)
            {
                foreach (var x in resultOfSearch.Entry)
                {
                    var testpatient = (Patient)x.Resource;
                    OurPatient op = serialisePatient.returnPatient(testpatient);
                    listOfPatients.Add(op);
                }
                resultOfSearch = client.Continue(resultOfSearch, PageDirection.Next);
            }
            Notify(listOfPatients);
            
            
        }
    }
}
