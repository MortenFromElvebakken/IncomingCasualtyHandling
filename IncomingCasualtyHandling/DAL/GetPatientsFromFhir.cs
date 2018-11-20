using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL.Interface;
using Task = Hl7.Fhir.Model.Task;

namespace IncomingCasualtyHandling.DAL
{
    public class GetPatientsFromFhir:IGetPatientsFromFHIR
    {
        private readonly string fhirServerURL;
        private readonly string hospitalShortName; //Til search params
        private FhirClient client;
        private ISerializeToPatient serialisePatient;
        private ILoadConfigurationSettings _loadConfigSettingsFromXmlDocument;
        private SearchParams sParameters;
        private Bundle lastBundle;
        private static Thread myThread;

        public GetPatientsFromFhir(ILoadConfigurationSettings _lcs, ISerializeToPatient _isp)
        {
            _loadConfigSettingsFromXmlDocument = _lcs;
            fhirServerURL = _loadConfigSettingsFromXmlDocument.ServerName;
            hospitalShortName = _loadConfigSettingsFromXmlDocument.HospitalShortName;

            //Initialize a client to call fhirserver
            client = new FhirClient(fhirServerURL);
            serialisePatient = _isp;

            
            //Initialize seachparameters
            sParameters = new SearchParams();
            sParameters.Add("active", "true");

            //Create thread that gets new data
            myThread = new Thread(AsyncGetAllPatients);
            myThread.IsBackground = true;
        }

        public void GetAllPatients()
        {
            
            //Her er parametrene lagt ind, så det er disse en query er bygget på. De er meget case sensitive
            
            //http://docs.simplifier.net/fhirnetapi/client/search.html
            
            List<PatientModel> listOfPatients = new List<PatientModel>();
            var firstBundle = client.Search<Patient>(sParameters);
            
            //Result er en bundle med "pages" i, hvor hver page loades med 10(lige nu) patienter i. Hvis der er flere end 10 læses de 10 første og
            //går til næste side
            lastBundle = firstBundle;
            while (firstBundle != null)
            {
                foreach (var x in firstBundle.Entry)
                {
                    var testpatient = (Patient)x.Resource;
                    PatientModel op = serialisePatient.ReturnPatient(testpatient);
                    listOfPatients.Add(op);
                }
                firstBundle = client.Continue(firstBundle, PageDirection.Next);
            }
            //Notify(listOfPatients);
            UpdatePatients(listOfPatients);
            
            myThread.Start();
        }

        //eventlogic, creates Event with list of patients, and invokes it. 
        public delegate void PatientUpdateHandler(List<PatientModel> _listOfPatients);
        public event PatientUpdateHandler PatientDataReady;

        private void UpdatePatients(List<PatientModel> _patientList)
        {
            var handler = PatientDataReady;
            handler?.Invoke(_patientList);
        }

        private void AsyncGetAllPatients()
        {
            
            Thread.Sleep(20000);
            var newBundle = client.SearchAsync<Patient>(sParameters).Result;
            if (newBundle.Link.IsExactly(lastBundle.Link))
            {
                Debug.WriteLine("Same bundle returned");
            }
            else
            {
                //Lave en foreach rundt om det her, der løber alle patienter igennem i hvert entry's side og tjekker om ens, vil virke til at
                //tjekke om alt er ens??
                lastBundle = newBundle;
                List<PatientModel> listOfPatients = new List<PatientModel>();
                while (newBundle != null)
                {
                    foreach (var x in newBundle.Entry)
                    {
                        var testpatient = (Patient)x.Resource;
                        PatientModel op = serialisePatient.ReturnPatient(testpatient);
                        listOfPatients.Add(op);
                    }
                    newBundle = client.Continue(newBundle, PageDirection.Next);
                }
                //Notify(listOfPatients);
                UpdatePatients(listOfPatients);

            }
            AsyncGetAllPatients();
        }

        public void setFhirClientURL(string s)
        {
            client = new FhirClient(s);
        }
    }
}
