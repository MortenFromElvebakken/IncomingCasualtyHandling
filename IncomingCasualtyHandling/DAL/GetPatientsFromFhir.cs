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
        private readonly string _fhirServerUrl;
        private FhirClient _client;
        private readonly ISerializeToPatient _serializePatient;
        private readonly ILoadConfigurationSettings _loadConfigSettingsFromXmlDocument;
        private readonly SearchParams _sParameters;
        private static Thread _myThread;
        private DateTime _dateOfLastSearch;
        private bool _internet;

        public GetPatientsFromFhir(ILoadConfigurationSettings _lcs, ISerializeToPatient _isp)
        {
            _loadConfigSettingsFromXmlDocument = _lcs;
            _fhirServerUrl = _loadConfigSettingsFromXmlDocument.ServerName;
            
            _client = new FhirClient(_fhirServerUrl);
            _serializePatient = _isp;
            _internet = true;
            _dateOfLastSearch = DateTime.MinValue;

            //Initialize seachparameters
            _sParameters = new SearchParams();
            _sParameters.Add("active", "true");
            _sParameters.Add("identifier", _loadConfigSettingsFromXmlDocument.HospitalShortName);


            //Create thread that checks for new data, and runs gets patients if there are updates
            _myThread = new Thread(AsyncGetAllPatients);
            _myThread.IsBackground = true;
        }

        public void GetAllPatients()
        {

            List<PatientModel> listOfPatients = new List<PatientModel>();
            var firstBundle = default(Bundle);

            //In a try catch, in case there is no server connection
            try
            {
                firstBundle = _client.Search<Patient>(_sParameters);
                _dateOfLastSearch = DateTime.Now;
                if (_internet == false)
                    _internet = true;
            }
            catch (Exception e)
            {
                
                //Sends event that there is no internetconnection, and sets internet bool to false.
                Debug.WriteLine(e.Message);
                _internet = false;
                NoInternetConnection(false);
                
            }

            //in a bundle, the entries are divided into pages. This while loop ensures every entry is added
            while (firstBundle != null)
            {
                foreach (var x in firstBundle.Entry)
                {
                    var testpatient = (Patient)x.Resource;
                    PatientModel op = _serializePatient.ReturnPatient(testpatient);
                    listOfPatients.Add(op);
                }
                firstBundle = _client.Continue(firstBundle, PageDirection.Next);
            }
            UpdatePatients(listOfPatients);
            if (!_myThread.IsAlive)
            {
                _myThread.Start();
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


        //Event for no internet
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
                anyChangedResources = _client.WholeSystemHistory(_dateOfLastSearch, 10);
                _dateOfLastSearch = DateTime.Now.AddSeconds(-1);
                if (_internet == false)
                {
                    _internet = true;
                    NoInternetConnection(true);
                }
                    
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                _internet = false;
                NoInternetConnection(false);
            }

            if (anyChangedResources != null && anyChangedResources.Entry.Count != 0)
            {
                var newBundle = _client.SearchAsync<Patient>(_sParameters).Result;
                List<PatientModel> listOfPatients = new List<PatientModel>();
                while (newBundle != null)
                {
                    foreach (var x in newBundle.Entry)
                    {
                        var testpatient = (Patient)x.Resource;
                        PatientModel op = _serializePatient.ReturnPatient(testpatient);
                        listOfPatients.Add(op);
                    }
                    newBundle = _client.Continue(newBundle, PageDirection.Next);
                }

                UpdatePatients(listOfPatients);

            }
            AsyncGetAllPatients();
        }

        public void setFhirClientURL(string s)
        {
            _client = new FhirClient(s);
            GetAllPatients();
        }
    }

    
}
