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
    class IT4_SortETA_TimerSortTriageSortSpecialtyDVModel
    {
        // Fakes
        private IFhirClient _client;
        private IMainView_Model _MV_M;
        private IOverviewView_Model _OV_M;


        // System under test        
        private SortTriage _sortTriage;
        private SortSpecialty _sortSpecialty;
        private ICountTime _countTime;
        private IDetailView_Model _DV_M;

        // Drivers
        private LoadData _getPatients;

        // Included 
        private LoadConfigurationSettings _loadConfig;
        private SortETA _sortEta;
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

        [SetUp]
        public void SetUp()
        {
            _MV_M = Substitute.For<IMainView_Model>();
            _OV_M = Substitute.For<IOverviewView_Model>();

            _convert = new ConvertToICHPatient();

            var currentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(
                TestContext.CurrentContext.TestDirectory));
            _xmlDocumentPath = currentDirectory + "\\Configuration.xml";
            _loadConfig = new LoadConfigurationSettings(_xmlDocumentPath);

            _getPatients = new LoadData(_loadConfig, _convert);

            _countTime = Substitute.For<ICountTime>();
            _DV_M = Substitute.For<IDetailView_Model>();

            _sortEta = new SortETA(_DV_M, _countTime, _getPatients);

            _sortTriage = new SortTriage(_loadConfig, _DV_M, _MV_M, _sortEta);
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

        #region  Timer
        [Test]
        public void SortForETA_CallCountTime_TimerReceivedCall()
        {
            _getPatients.GetAllPatients();

            _countTime.ReceivedWithAnyArgs().FindRelativeTime(new List<ICHPatient>());

        }

        #endregion

        #region SortTriage
        [Test]
        public void SortForETA_RaisesEvent_SortTriageReceivedCall()
        {
            _getPatients.GetAllPatients();

            Assert.That(_sortTriage.TriageList[0].Amount, Is.EqualTo(1));

        }

        #endregion

        #region SortSpecialty
        [Test]
        public void SortForETA_RaisesEvent_SortSpecialtyReceivedCall()
        {
            _getPatients.GetAllPatients();

            Assert.That(_sortSpecialty.SpecialtiesList.Find(s => s.Name == specialty).Amount, Is.EqualTo(1));

        }
        #endregion

        #region DetailView Model
        [Test]
        public void SortForETA_SetETAPatients_ETAPatientsSetInDetailModel()
        {
            _getPatients.GetAllPatients();

            Assert.IsTrue(_DV_M.ETAPatients.Exists(p => p.Name == wholeName));

        }
        #endregion

    }

}