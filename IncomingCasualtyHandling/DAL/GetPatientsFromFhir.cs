using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using IncomingCasualtyHandling.BL.Models;
using Task = Hl7.Fhir.Model.Task;

namespace IncomingCasualtyHandling.DAL
{
    internal class GetPatientsFromFhir: SubjectObserverPatients
    {
        private readonly string fhirServerURL;
        private readonly string hospitalShortName; //Til search params
        private FhirClient client;
        private SerialiseToPatient serialisePatient;
        private LoadConfigurationSettingsFromXMLDocument _loadConfigSettingsFromXmlDocument;
        private SearchParams sParameters;
        private Bundle lastBundle;
        private static Thread myThread;
        public GetPatientsFromFhir(LoadConfigurationSettingsFromXMLDocument _lcs)
        {
            _loadConfigSettingsFromXmlDocument = _lcs;
            fhirServerURL = _loadConfigSettingsFromXmlDocument.ServerName;
            hospitalShortName = _loadConfigSettingsFromXmlDocument.HospitalShortName;
            client = new FhirClient(fhirServerURL);
            serialisePatient = new SerialiseToPatient();
            //GetAllPatients();
            sParameters = new SearchParams();
            sParameters.Add("active", "true");

            myThread = new Thread(AsyncGetAllPatients);
        }

        public void GetAllPatients()
        {
            
            //Her er parametrene lagt ind, så det er disse en query er bygget på. De er meget case sensitive
            
            //http://docs.simplifier.net/fhirnetapi/client/search.html
            
            List<PatientModel> listOfPatients = new List<PatientModel>();

            lastBundle = client.Search<Patient>(sParameters);
            //Result er en bundle med "pages" i, hvor hver page loades med 10 patienter i. Hvis der er flere end 10 læses de 10 første og
            //går til næste side
            while (lastBundle != null)
            {
                foreach (var x in lastBundle.Entry)
                {
                    var testpatient = (Patient)x.Resource;
                    PatientModel op = serialisePatient.ReturnPatient(testpatient);
                    listOfPatients.Add(op);
                }
                lastBundle = client.Continue(lastBundle, PageDirection.Next);
            }
            Notify(listOfPatients);
            
            
            myThread.Start();
        }

        private void AsyncGetAllPatients()
        {
            Thread.Sleep(400);
            var newBundle = client.SearchAsync<Patient>(sParameters).Result;
            if (newBundle.IsExactly(lastBundle))
            {
                Debug.WriteLine("Same bundle returned");
            }
            else
            {
                List<PatientModel> listOfPatients = new List<PatientModel>();
                while (lastBundle != null)
                {
                    foreach (var x in lastBundle.Entry)
                    {
                        var testpatient = (Patient)x.Resource;
                        PatientModel op = serialisePatient.ReturnPatient(testpatient);
                        listOfPatients.Add(op);
                    }
                    lastBundle = client.Continue(lastBundle, PageDirection.Next);
                }
                Notify(listOfPatients);
            }
        }
    }
}
