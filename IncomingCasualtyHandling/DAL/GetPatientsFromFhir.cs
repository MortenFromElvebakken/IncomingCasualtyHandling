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
        private string _fhirServerUrl;
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


        private bool sameAsLast;
        List<Patient> lastChangedPatients = default(List<Patient>);

        private bool checkIfSamePatientsReturned(Bundle b)
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
                    var testEntry = _client.Read<Patient>(_fhirServerUrl + "/Patient/" + b.Entry[counterTest].Resource.Id);
                    changedPatients.Add(testEntry);
                    counterTest++;
                }

                int CheckIfPatientsAreTheSame = counterTest;
                for (int i = 0; i < counterTest; i++)
                {
                    if (lastChangedPatients != null && changedPatients[i].Meta.LastUpdated == lastChangedPatients[i].Meta.LastUpdated)
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
                    lastChangedPatients = changedPatients;
                    return false;
                }
                
            }
        }

        private void AsyncGetAllPatients()
        {
            sameAsLast = true;
            var anyChangedResources = default(Bundle);
            Thread.Sleep(5000);
            try
            {
                //throw new Exception("test");
                anyChangedResources = _client.WholeSystemHistory(_dateOfLastSearch, 10);
                _dateOfLastSearch = DateTime.Now.AddSeconds(-5);
                if (_internet == false)
                {
                    _internet = true;
                    NoInternetConnection(true);
                }

                sameAsLast = checkIfSamePatientsReturned(anyChangedResources);

                //int counterTest = 0;
                //foreach (var entry in anyChangedResources.Entry)
                //{
                //    var testEntry = _client.Read<Patient>(_fhirServerUrl + "/Patient/" + anyChangedResources.Entry[counterTest].Resource.Id);
                //    counterTest++;
                //}

                //int CheckIfPatientsAreTheSame = counterTest;
                //for (int i = 0; i < counterTest; i++)
                //{
                //    if (lastChangedPatients != null && changedPatients[i].Meta.LastUpdated == lastChangedPatients[i].Meta.LastUpdated)
                //    {
                //        CheckIfPatientsAreTheSame--;
                //    }
                //}

                //if (anyChangedResources.Entry.Count != 0 && CheckIfPatientsAreTheSame == 0)
                //{
                //    sameAsLast = false;
                //}



            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                _internet = false;
                NoInternetConnection(false);
            }

            if (anyChangedResources != null && !sameAsLast)
            {
                //Logik på alle patienter som dukker i ændrede resources, henter dem enkeltvis og sammenligner med det bundle den får tilbage, hvis de er active true, og ikke
                // findes i bundle, tilføj, hvis de er active false og findes i bundle, skal den fjerne. 
                //
                //List<Patient> otherEntries = new List<Patient>();
                //int counter = 0;
                //foreach (var x in anyChangedResources.Entry)
                //{
                //    var testEntry = _client.Read<Patient>(_fhirServerUrl + "/Patient/" + anyChangedResources.Entry[counter].Resource.Id);
                //    otherEntries.Add(testEntry);
                //    counter++;
                //}
               
                var newBundle = _client.Search<Patient>(_sParameters);
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
                foreach (var patient in lastChangedPatients)
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
                                    listOfPatients[counter2] = _serializePatient.ReturnPatient(patient);
                                    didItContainElement = true;
                                break;
                            }

                            counter2++;
                        }

                        if (!didItContainElement)
                        {
                            listOfPatients.Add(_serializePatient.ReturnPatient(patient));
                        }
                    }
                }
                    UpdatePatients(listOfPatients);
            }
            AsyncGetAllPatients();
        }

        public void setFhirClientURL(string s)
        {
            _client = new FhirClient(s);
            _fhirServerUrl = s;
            GetAllPatients();
        }
    }

    
}
