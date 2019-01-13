using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL;
using IncomingCasualtyHandling.DAL.Interface;
using NSubstitute;
using NUnit.Framework;

namespace IncomingCasualtyHandling.Test.Unit.DAL.Test.Unit
{
    [TestFixture]
    public class LoadDataTest
    {
        #region Arrange

        private LoadData _uut;

        private IFhirClient _client;
        private ILoadConfigurationSettings _loadConfig;
        private IConvertToICHPatient _convert;

        private string _testServerName = "http://localhostas:8080/hapi-fhir-jpaserver-example/baseDstu3";
        private List<ICHPatient> _patientList;

        // Create FHIR patient
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
        public void Setup()
        {
            _loadConfig = Substitute.For<ILoadConfigurationSettings>();
            _convert = Substitute.For<IConvertToICHPatient>();
            _loadConfig.ServerName.ReturnsForAnyArgs(_testServerName);

            _uut = new LoadData(_loadConfig, _convert);
            _uut.Client = Substitute.For<IFhirClient>();
            _client = _uut.Client;

            _uut.PatientDataReady += (o) => _patientList = o;

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

            Patient1.Active = true;
        }


        #endregion

        #region Act and Assert

        //Get a patient and raise event
        [Test]
        public void GetAllPatients_GetsPatientsAndRaisesEvent_EventRaised()
        {
            Bundle _bundle = new Bundle();
            Bundle.EntryComponent _entry = new Bundle.EntryComponent();
            _entry.Resource = Patient1;
            _bundle.Entry.Add(_entry);
            _convert.ReturnPatient(new Patient()).ReturnsForAnyArgs(new ICHPatient());
            _client.Search<Patient>(new SearchParams()).ReturnsForAnyArgs(_bundle);
            _uut.GetAllPatients();
            
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
            _uut.GetAllPatients();

            // Clear the list for this raised event
            _patientList.Clear();

            // Wait for Async to get called
            Thread.Sleep(5000);

            // Verify, that no SerialisePatient class wasn't called => no patients in the list
            Assert.That(_patientList.Count, Is.EqualTo(0));

        }

        [Test]
        public void AsynchGetAllPatients_UpdateOnPatient_RaisesEvent()
        {
            Bundle _bundle = new Bundle();
            Bundle.EntryComponent _entry = new Bundle.EntryComponent();
            _entry.Resource = Patient1;
            _bundle.Entry.Add(_entry);
            _client.Search<Patient>(new SearchParams()).ReturnsForAnyArgs(_bundle);
            _convert.ReturnPatient(new Patient()).ReturnsForAnyArgs(new ICHPatient());
            _uut.GetAllPatients();
            // Clear the list for this raised event
            _patientList.Clear();

            // Update Patient
            Meta meta = new Meta();
            meta.LastUpdated = new DateTimeOffset(2018, 11, 22, 10, 0, 0, new TimeSpan(0, 0, 0, 0));
            Patient1.Meta = meta;

            _client.WholeSystemHistory(null, null, new SummaryType()).ReturnsForAnyArgs(_bundle);
            _client.SearchAsync<Patient>(new SearchParams()).ReturnsForAnyArgs(_bundle);
            _client.Read<Patient>("Test").ReturnsForAnyArgs(Patient1);
            // Wait for Async to get called
            Thread.Sleep(6000);

            // Verify event was raised
            Assert.That(_patientList.Count, Is.GreaterThan(0));

        }
        #endregion
    }
}
