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
    public class LoadData : ILoadData
    {
        public string FhirServerUrl { get; set; }
        public IFhirClient Client { get; set; }
        private readonly IConvertToICHPatient _convertICHPatient;
        private readonly ILoadConfigurationSettings _loadConfigurationSettings;
        private SearchParams _sParameters;
        private static Thread _myThread;
        private DateTime _dateOfLastSearch;
        private bool _internet;

        public LoadData(ILoadConfigurationSettings _lcs, IConvertToICHPatient _isp)
        {
            _loadConfigurationSettings = _lcs;
            FhirServerUrl = GetServerUrl();
            
            Client = new FhirClient(FhirServerUrl);
            _convertICHPatient = _isp;
            _internet = true;
            _dateOfLastSearch = DateTime.MinValue;

            //Initialize seachparameters
            _sParameters = new SearchParams();
            _sParameters.Add("active", "true");
            _sParameters.Add("identifier", _loadConfigurationSettings.HospitalShortName);


            //Create thread that checks for new data, and runs gets patients if there are updates
            _myThread = new Thread(AsyncGetAllPatients);
            _myThread.IsBackground = true;
        }

        private string GetServerUrl()
        {
            return _loadConfigurationSettings.ServerName;
        }

        public void GetAllPatients()
        {

            List<ICHPatient> listOfPatients = new List<ICHPatient>();
            var firstBundle = default(Bundle);

            //In a try catch, in case there is no server connection
            try
            {
                firstBundle = Client.Search<Patient>(_sParameters);
                _dateOfLastSearch = DateTime.Now;
                if (_internet == false)
                {
                    _internet = true;
                    InternetConnection(true);
                }
            }
            catch (Exception e)
            {
                
                //Sends event that there is no internetconnection, and sets internet bool to false.
                Debug.WriteLine(e.Message);
                _internet = false;
                InternetConnection(false);
                
            }

            //in a bundle, the entries are divided into pages. This while loop ensures every entry is added
            while (firstBundle != null)
            {
                foreach (var x in firstBundle.Entry)
                {
                    var testpatient = (Patient)x.Resource;
                    ICHPatient op = _convertICHPatient.ReturnPatient(testpatient);
                    listOfPatients.Add(op);
                }
                firstBundle = Client.Continue(firstBundle, PageDirection.Next);
            }
            UpdatePatients(listOfPatients);
            if (!_myThread.IsAlive)
            {
                _myThread.Start();
            }

        }

        //eventlogic, creates Event with list of patients, and invokes it. 
        public delegate void PatientUpdateHandler(List<ICHPatient> _listOfPatients);
        public event PatientUpdateHandler PatientDataReady;

        private void UpdatePatients(List<ICHPatient> _patientList)
        {
            var handler = PatientDataReady;
            handler?.Invoke(_patientList);
        }


        //Event for no internet
        public delegate void NoInterNetUpdateHandler(bool b);
        public event NoInterNetUpdateHandler NoInternet;

        private void InternetConnection(bool b)
        {
            var handler = NoInternet;
            handler?.Invoke(b);
        }


        private bool _sameAsLast;
        private List<Patient> _lastChangedPatients = default(List<Patient>);

        private bool CheckIfSamePatientsReturned(Bundle b)
        {
            if (b.Entry.Count == 0)
            {
                return true;
            }
            else
            {
                int counterTest = 0;
                List<Patient> changedPatients = new List<Patient>();
                foreach (var entry in b.Entry)
                {
                    var testEntry = Client.Read<Patient>(FhirServerUrl + "/Patient/" + b.Entry[counterTest].Resource.Id);
                    changedPatients.Add(testEntry);
                    counterTest++;
                }

                int CheckIfPatientsAreTheSame = counterTest;
                for (int i = 0; i < counterTest; i++)
                {
                    if (_lastChangedPatients != null && changedPatients[i].Meta.LastUpdated == _lastChangedPatients[i].Meta.LastUpdated)
                    {
                        CheckIfPatientsAreTheSame--;
                    }
                }

                if (CheckIfPatientsAreTheSame == 0)
                {
                    return true;
                }
                else
                {
                    _lastChangedPatients = changedPatients;
                    return false;
                }
                
            }
        }

        private void AsyncGetAllPatients()
        {
            _sameAsLast = true;
            var anyChangedResources = default(Bundle);
            Thread.Sleep(5000);
            try
            {
                //throw new Exception("test");
                anyChangedResources = Client.WholeSystemHistory(_dateOfLastSearch, 10);
                _dateOfLastSearch = DateTime.Now.AddSeconds(-1);
                if (_internet == false)
                {   
                    _internet = true;
                    InternetConnection(true);
                }

                _sameAsLast = CheckIfSamePatientsReturned(anyChangedResources);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                _internet = false;
                InternetConnection(false);
            }

            if (anyChangedResources != null && !_sameAsLast)
            {
                var newBundle = Client.SearchAsync<Patient>(_sParameters).Result;
                List<ICHPatient> listOfPatients = new List<ICHPatient>();
                
                while (newBundle != null)
                {
                    foreach (var entries in newBundle.Entry)
                    {
                        var testpatient = (Patient)entries.Resource;
                        ICHPatient op = _convertICHPatient.ReturnPatient(testpatient);
                        listOfPatients.Add(op);
                    }
                    newBundle = Client.Continue(newBundle, PageDirection.Next);
                }
                foreach (var patient in _lastChangedPatients)
                {
                    if (patient.Active == false)
                    {
                        var cpr = patient.Identifier[0].Value;
                        foreach (var p in listOfPatients)
                        {
                            if (p.CPR == cpr)
                            {
                                listOfPatients.Remove(p);
                                break;
                            }
                        }
                    }
                    if (patient.Active == true)
                    {
                        var cpr = patient.Identifier[0].Value;
                        int counter2 = 0;
                        bool didItContainElement = false;
                        foreach (var p in listOfPatients)
                        {
                            if (p.CPR == cpr)
                            {
                                    listOfPatients[counter2] = _convertICHPatient.ReturnPatient(patient);
                                    didItContainElement = true;
                                break;
                            }

                            counter2++;
                        }

                        if (!didItContainElement)
                        {
                            listOfPatients.Add(_convertICHPatient.ReturnPatient(patient));
                        }
                    }
                }
                    UpdatePatients(listOfPatients);
            }
            AsyncGetAllPatients();
        }

        public void SetFhirClientURL(string s)
        {
            Client = new FhirClient(s); 
            //var newSearchParams = new SearchParams();
            //newSearchParams.Add("active", "true");
            //newSearchParams.Add("identifier", _loadConfigurationSettings.HospitalShortName);
            //_sParameters = newSearchParams;
            FhirServerUrl = s;
            GetAllPatients();
        }
    }

    
}
