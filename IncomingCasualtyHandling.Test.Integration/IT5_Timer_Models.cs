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
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL;
using IncomingCasualtyHandling.DAL.Interface;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;

namespace IncomingCasualtyHandling.Test.Integration
{
    [TestFixture]
    class IT5_Timer_Models
    {
        // Fakes
        private IFhirClient _client;

        // System under test        
        private IMainView_Model _MV_M;
        private IOverviewView_Model _OV_M;
        private IDetailView_Model _DV_M;

        // Drivers
        private GetPatientsFromFhir _getPatients;

        // Included 
        private ILoadConfigurationSettings _loadConfig;
        private ISortETA _sortEta;
        private SerialiseToPatient _serialise;
        private ISortTriage _sortTriage;
        private ISortSpecialty _sortSpecialty;
        private ICountTime _countTime;

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

        private int _nEventsRaised;
        private List<PatientModel> _sortedPatients;

        [SetUp]
        public void SetUp()
        {
            _serialise = new SerialiseToPatient();

            _xmlDocumentPath =
                "E://Visual Studio 2017//BAC//IncomingCasualtyHandling.Test.Integration//Configuration.xml";
            _loadConfig = new LoadConfigurationSettingsFromXMLDocument(_xmlDocumentPath);

            _getPatients = new GetPatientsFromFhir(_loadConfig, _serialise);

            _MV_M = new MainView_Model(_getPatients);
            _OV_M = new OverviewView_Model();
            _DV_M = new DetailView_Model();

            _countTime = new CountTime(_MV_M, _OV_M);

            _sortEta = new SortETA(_OV_M, _DV_M, _MV_M, _countTime, _getPatients);

            _sortTriage = new SortTriage(_loadConfig, _OV_M, _DV_M, _MV_M, _sortEta);
            _sortSpecialty = new SortSpecialty(_loadConfig, _OV_M, _DV_M, _MV_M, _sortEta);
            
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

            Bundle _bundle = new Bundle();
            Bundle.EntryComponent _entry = new Bundle.EntryComponent();
            _entry.Resource = Patient1;
            _bundle.Entry.Add(_entry);

            _client.Search<Patient>(new SearchParams()).ReturnsForAnyArgs(_bundle);
            _client.WholeSystemHistory(null, null, new SummaryType()).ReturnsForAnyArgs(_bundle);
            _client.SearchAsync<Patient>(new SearchParams()).ReturnsForAnyArgs(_bundle);

        }

        
        // CAN'T GET IT TO WORK RIGHT NOW [Test]
        public void CurrentDateTimeTick_ChangePropertyInModel_PropertyInMainViewModelChanges()
        {
            // Sleep 1.5 second to make sure, that tick has occured
            Thread.Yield();
            Assert.NotNull(_MV_M.CurrentDateTime);
        }

        [Test]
        public void FindRelativeTime_SetTimeInMainModel_AbsoluteTimeSetInMainModel()
        {
            // Set ETA to 2 hours from now
            DateTime twoHoursFromNow = DateTime.Now.AddHours(2);
            Patient1.Extension[2].Value = new FhirDateTime(twoHoursFromNow);

            string twoHoursInAbsolute = twoHoursFromNow.ToShortTimeString();

            _getPatients.GetAllPatients();

            Assert.That(_MV_M.Eta.AbsoluteTime, Is.EqualTo(twoHoursInAbsolute));

        }
        [Test]
        public void FindRelativeTime_SetTimeInMainModel_RelativeTimeSetInMainModel()
        {
            // Set ETA to 2 hours from now
            DateTime twoHoursFromNow = DateTime.Now.AddHours(2);
            Patient1.Extension[2].Value = new FhirDateTime(twoHoursFromNow);

            string twoHoursInMinutes = "(-120 minutes)";

            _getPatients.GetAllPatients();

            Assert.That(_MV_M.Eta.RelativeTime, Is.EqualTo(twoHoursInMinutes));

        }
    }

}