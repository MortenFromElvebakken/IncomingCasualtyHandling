﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL;
using IncomingCasualtyHandling.DAL.Interface;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;

namespace IncomingCasualtyHandling.Test.Integration
{
    [TestFixture]
    class IT2_GetPatients_Serialise
    {
        // Fakes
        private IFhirClient _client;

        // System under test        
        private SerialiseToPatient _serialise;

        // Drivers
        private GetPatientsFromFhir _getPatients;

        // Included 
        private ILoadConfigurationSettings _loadConfig;

        // Data
        private string _xmlDocumentPath;
        private string _xmlServerName = "http://localhost:8080/hapi-fhir-jpaserver-example/baseDstu3";
        private List<PatientModel> _patientList;
        public Patient Patient1 = new Patient();

        string triage = "TriageRed";
        string specialty = "Emergency medicine";
        DateTime eta = new DateTime(2018, 11, 22, 12, 00, 00, DateTimeKind.Local);
        string cpr = "201120001518";
        string givenName = "Test";
        string familyName = "Testson";
        string wholeName;
        private AdministrativeGender gender = AdministrativeGender.Unknown;
        private string toHospital = "Unknown";
        private DateTimeOffset lastUpdated = new DateTimeOffset(2018, 11, 22, 8, 0, 0, new TimeSpan(0, 0, 0, 0));

        [SetUp]
        public void SetUp()
        {
            _serialise = new SerialiseToPatient();

            _xmlDocumentPath =
                "E://Visual Studio 2017//BAC//IncomingCasualtyHandling.Test.Integration//Configuration.xml";
            _loadConfig = new LoadConfigurationSettingsFromXMLDocument(_xmlDocumentPath);

            _getPatients = new GetPatientsFromFhir(_loadConfig, _serialise);

            _getPatients.PatientDataReady += (o) => _patientList = o;

            // Create patient
            wholeName = givenName + " " + familyName;

            Patient1 = new Patient();
            Patient1.Identifier.Add(new Identifier("CPR", cpr));
            var name = new HumanName();
            name.WithGiven(givenName);
            name.AndFamily(familyName);
            name.Text = givenName + " " + familyName;
            Patient1.Name.Add(name);

            Patient1.Gender = gender;

            Patient1.Extension = new List<Extension>();
            Patient1.Extension.Add(new Extension("http://www.example.com/triagetest", new FhirString(triage)));
            Patient1.Extension.Add(new Extension("http://www.example.com/SpecialtyTest", new FhirString(specialty)));
            Patient1.Extension.Add(new Extension("http://www.example.com/datetimeTest", new FhirDateTime(eta)));
            Meta meta = new Meta();
            meta.LastUpdated = lastUpdated;
            Patient1.Meta = meta;

            _getPatients.Client = Substitute.For<IFhirClient>();
            _client = _getPatients.Client;
        }

        [Test]
        public void GetAllPatients_CallSerialisePatient_SerialisePatientsReturnsPatient()
        {
            Bundle _bundle = new Bundle();
            Bundle.EntryComponent _entry = new Bundle.EntryComponent();
            _entry.Resource = Patient1;
            _bundle.Entry.Add(_entry);
            _client.Search<Patient>(new SearchParams()).ReturnsForAnyArgs(_bundle);
            _getPatients.GetAllPatients();
            
            Assert.That(_patientList.Count, Is.EqualTo(1));
            
        }

        [Test]
        public void AsynchGetAllPatients_NoNewPatient_DoesNotCallSerialisePatient()
        {

            Bundle _bundle = new Bundle();
            Bundle.EntryComponent _entry = new Bundle.EntryComponent();
            _entry.Resource = Patient1;
            _bundle.Entry.Add(_entry);
            _client.Search<Patient>(new SearchParams()).ReturnsForAnyArgs(_bundle);
            _getPatients.GetAllPatients();

            // Clear the list for this raised event
            _patientList.Clear();

            // Wait for Async to get called
            Thread.Sleep(5000);

            // Verify, that no SerialisePatient class wasn't called => no patients in the list
            Assert.That(_patientList.Count, Is.EqualTo(0));

        }

        [Test]
        public void AsynchGetAllPatients_UpdateOnPatient_CallSerialisePatient()
        {
            Bundle _bundle = new Bundle();
            Bundle.EntryComponent _entry = new Bundle.EntryComponent();
            _entry.Resource = Patient1;
            _bundle.Entry.Add(_entry);
            _client.Search<Patient>(new SearchParams()).ReturnsForAnyArgs(_bundle);
            _getPatients.GetAllPatients();
            // Clear the list for this raised event
            _patientList.Clear();

            // Update Patient
            Meta meta = new Meta();
            meta.LastUpdated = new DateTimeOffset(2018, 11, 22, 10, 0, 0, new TimeSpan(0, 0, 0, 0));
            Patient1.Meta = meta;

            _client.WholeSystemHistory(null, null, new SummaryType()).ReturnsForAnyArgs(_bundle);
            _client.SearchAsync<Patient>(new SearchParams()).ReturnsForAnyArgs(_bundle);

            // Wait for Async to get called
            Thread.Sleep(5000);

            // Verify, that SerialisePatient class was called => patient list has a patient
            Assert.That(_patientList.Count, Is.EqualTo(1));

        }
    }

}