using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using IncomingCasualtyHandling.BL;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL;
using IncomingCasualtyHandling.DAL.Interface;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;

namespace IncomingCasualtyHandling.Test.Integration
{
    [TestFixture]
    class IT3_LoadData_SortETA
    {
        // Fakes
        private IFhirClient _client;
        private ISortTriage _sortTriage;
        private ISortSpecialty _sortSpecialty;
        private ICountTime _countTime;
        private IDetailView_Model _DV_M;


        // System under test        
        private ISortETA _sortEta;

        // Drivers
        private LoadData _getPatients;

        // Included 
        private ILoadConfigurationSettings _loadConfig;
        private ConvertToICHPatient _convert;

        // Data
        private string _xmlDocumentPath;
        private string _xmlServerName = "http://localhost:8080/hapi-fhir-jpaserver-example/baseDstu3";
        private List<ICHPatient> _patientList;
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

        private int _nEventsRaised;
        private List<ICHPatient> _sortedPatients;

        [SetUp]
        public void SetUp()
        {

            _sortTriage = Substitute.For<ISortTriage>();
            _sortSpecialty = Substitute.For<ISortSpecialty>();
            _countTime = Substitute.For<ICountTime>();
            _DV_M = Substitute.For<IDetailView_Model>();


            _convert = new ConvertToICHPatient();

            var currentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(
                TestContext.CurrentContext.TestDirectory));
            _xmlDocumentPath = currentDirectory + "\\Configuration.xml";
            _loadConfig = new LoadConfigurationSettings(_xmlDocumentPath);

            _getPatients = new LoadData(_loadConfig, _convert);

            _getPatients.PatientDataReady += (o) => _patientList = o;

            _sortEta = new SortETA(_DV_M, _countTime, _getPatients);

            _sortEta.SortedListReady += (o) =>
            {
                ++_nEventsRaised;
                _sortedPatients = o;
            };

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

            _getPatients.Client = Substitute.For<IFhirClient>();
            _client = _getPatients.Client;

            Bundle _bundle = new Bundle();
            Bundle.EntryComponent _entry = new Bundle.EntryComponent();
            _entry.Resource = Patient1;
            _bundle.Entry.Add(_entry);

            _client.Search<Patient>(new SearchParams()).ReturnsForAnyArgs(_bundle);
            _client.WholeSystemHistory(null, null, new SummaryType()).ReturnsForAnyArgs(_bundle);
            _client.SearchAsync<Patient>(new SearchParams()).ReturnsForAnyArgs(_bundle);
            _client.Read<Patient>("Test").ReturnsForAnyArgs(Patient1);
        }

       [Test]
        public void GetAllPatients_RaisesEvent_SortETAReactsOnEvent()
        {
            _getPatients.GetAllPatients();
            Assert.That(_sortedPatients[0].Name, Is.EqualTo(wholeName));
        }

        [Test]
        public void AsynchGetAllPatients_RaisesEvent_SortETAReactsOnEvent()
        {
            _getPatients.GetAllPatients();

            // Update Patient
            string newFirstName = "Integration";
            string newFamilyName = "Test";
            string newWholeName = newFirstName + " " + newFamilyName;
            var newName = new HumanName();
            newName.WithGiven(givenName);
            newName.AndFamily(familyName);
            newName.Text = newWholeName;
            Patient1.Name.Insert(0, newName);
            Meta meta = new Meta();
            meta.LastUpdated = new DateTimeOffset(2018, 11, 22, 10, 0, 0, new TimeSpan(0, 0, 0, 0));
            Patient1.Meta = meta;

            _nEventsRaised = 0;

            // Wait for Async to get called
            Thread.Sleep(7000);

            // Verify, that SerialisePatient class was called => patient list has a patient
            Assert.That(_sortedPatients[0].Name, Is.EqualTo(newWholeName));

        }

    }

}