using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL.Interface;
using Task = Hl7.Fhir.Model.Task;

namespace IncomingCasualtyHandling.DAL
{
    public class GetPatientsFromFhir : IGetPatientsFromFHIR
    {
        private readonly string fhirServerURL;
        private readonly string hospitalShortName; //Til search params
        private FhirClient client;
        private ISerializeToPatient serialisePatient;
        private ILoadConfigurationSettings _loadConfigSettingsFromXmlDocument;
        private SearchParams sParameters;
        private static Thread myThread;
        private DateTime dateOfLastSearch;
        private bool internet;

        public GetPatientsFromFhir(ILoadConfigurationSettings _lcs, ISerializeToPatient _isp)
        {
            _loadConfigSettingsFromXmlDocument = _lcs;
            fhirServerURL = _loadConfigSettingsFromXmlDocument.ServerName;
            hospitalShortName = _loadConfigSettingsFromXmlDocument.HospitalShortName;

            //Initialize a client to call fhirserver
            //Hvordan testes internet connection???...
            //Måske set server op til at kigge på en anden fhir server, fjern internet, men lad localhost
            //på config fil. Så burde man kunne teste hvor den breaker henne
            client = new FhirClient(fhirServerURL);
            serialisePatient = _isp;
            internet = true;
            dateOfLastSearch = DateTime.MinValue;

            //Initialize seachparameters
            sParameters = new SearchParams();
            sParameters.Add("active", "true");
            //sParameters.Add("identifier", "AUH");


            //Create thread that gets new data
            myThread = new Thread(AsyncGetAllPatients);
            myThread.IsBackground = true;
        }

        public void GetAllPatients()
        {

            //Her er parametrene lagt ind, så det er disse en query er bygget på. De er meget case sensitive

            //http://docs.simplifier.net/fhirnetapi/client/search.html

            List<PatientModel> listOfPatients = new List<PatientModel>();
            var firstBundle = default(Bundle);
            try
            {
                firstBundle = client.Search<Patient>(sParameters);
                dateOfLastSearch = DateTime.Now;
                if (internet == false)
                    internet = true;
            }
            catch (Exception e)
            {
                
                //Sends event that there is no internetconnection, and sets internet bool to false.
                internet = false;
                NoInternetConnection(false);
                
            }

            //in a bundle, the entries are divided into pages. This while loop ensures every entry is added
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
            UpdatePatients(listOfPatients);
            if (!myThread.IsAlive)
            {
                myThread.Start();
            }

        }

        //eventlogic, creates Event with list of patients, and invokes it. 
        public delegate void PatientUpdateHandler(List<PatientModel> _listOfPatients);
        public event PatientUpdateHandler PatientDataReady;

        private void UpdatePatients(List<PatientModel> _patientList)
        {
            var handler = PatientDataReady;
            handler?.Invoke(_patientList);
        }

        public delegate void NoInterNetUpdateHandler(bool b);
        public event NoInterNetUpdateHandler NoInternet;

        private void NoInternetConnection(bool b)
        {
            var handler = NoInternet;
            handler?.Invoke(b);
        }

        

        private void AsyncGetAllPatients()
        {
            var anyChangedResources = default(Bundle);
            Thread.Sleep(5000);
            try
            {
                //throw new Exception("test");
                anyChangedResources = client.WholeSystemHistory(dateOfLastSearch, 10);
                dateOfLastSearch = DateTime.Now.AddSeconds(-1);
                if (internet == false)
                {
                    internet = true;
                    NoInternetConnection(true);
                }
                    
            }
            catch (Exception e)
            {
                internet = false;
                NoInternetConnection(false);
            }

            if (anyChangedResources != null && anyChangedResources.Entry.Count != 0)
            {
                var newBundle = client.SearchAsync<Patient>(sParameters).Result;
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

                UpdatePatients(listOfPatients);

            }
            AsyncGetAllPatients();
        }

        public void setFhirClientURL(string s)
        {
            client = new FhirClient(s);
            GetAllPatients();
        }
    }

    
}
