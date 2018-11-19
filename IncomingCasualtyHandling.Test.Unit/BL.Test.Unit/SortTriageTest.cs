using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL.Interface;
using Hl7.Fhir.Model;
using NSubstitute;
using NUnit.Framework;

namespace IncomingCasualtyHandling.Test.Unit.BL.Test.Unit
{

    [TestFixture]
    public class SortTriageTest
    {
        #region Arrange

        private ISortTriage _uut;

        private ILoadConfigurationSettings _loadConfigSettings;
        //private IOverviewView_Model _overviewViewModel;
        private OverviewView_Model _overviewViewModel;
        private IDetailView_Model _detailViewModel;
        //private DetailView_Model _detailViewModel;
        //private IMainView_Model _mainViewModel;
        private MainView_Model _mainViewModel;

        private IGetPatientsFromFHIR _getPatientsFromFHIR;


        private List<Triage> _listOfTriages;
        private List<PatientModel> _listOfPatients, _sortedListOfPatients;
        private PatientModel _patient1, _patient2, _patient3, _patient4, _patient5;

        [SetUp]
        public void Setup()
        {
            _loadConfigSettings = Substitute.For<ILoadConfigurationSettings>();
            _overviewViewModel = Substitute.For<OverviewView_Model>();
            _detailViewModel = Substitute.For<IDetailView_Model>();
            _mainViewModel = Substitute.For<MainView_Model>();
            _getPatientsFromFHIR = Substitute.For<IGetPatientsFromFHIR>();

            _listOfTriages = new List<Triage>();
            _listOfTriages.Add(new Triage { Name = "TriageRed" });
            _listOfTriages.Add(new Triage { Name = "TriageOrange" });
            _listOfTriages.Add(new Triage { Name = "TriageYellow" });
            _listOfTriages.Add(new Triage { Name = "TriageGreen" });
            _listOfTriages.Add(new Triage { Name = "TriageBlue" });
            _listOfTriages.Add(new Triage { Name = "TriageUnknown" });


            _loadConfigSettings.TriageList = _listOfTriages;

            _uut = new SortTriage(_loadConfigSettings, _overviewViewModel, _detailViewModel, _mainViewModel,
                    _getPatientsFromFHIR);

            // Create a list with patients
            _listOfPatients = new List<PatientModel>();
            _sortedListOfPatients = new List<PatientModel>();
            _patient1 = new PatientModel

            {
                PatientId = "1",
                Name = "Patient One",
                Age = "10",
                Gender = AdministrativeGender.Male,
                Triage = "TriageRed",
                Specialty = "Neurology",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 22, 30, 00)
            };
            _patient2 = new PatientModel
            {
                PatientId = "2",
                Name = "Patient Two",
                Age = "20",
                Gender = AdministrativeGender.Female,
                Triage = "TriageYellow",
                Specialty = "Psychology",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 21, 30, 00)
            };

            _listOfPatients.Add(_patient1);
            _listOfPatients.Add(_patient2);

        }

        #endregion

        #region Act and Assert
        // Metode_Scenarie_Resultat

        // Test an empty list; no amounts in triagelist in MainView model
        [Test]
        public void SortForTriage_ListWithoutPatients_NoAmountInMainViewTriages()
        {
            _listOfPatients.Clear();
            _uut.SortForTriage(_listOfPatients);
            Assert.That(_mainViewModel.ListOfTriages[0].Amount, Is.EqualTo(0));
        }

        // Test an empty list; no amounts in triagelist in DetailView model
        [Test]
        public void SortForTriage_ListWithoutPatients_NoAmountInDetailViewTriages()
        {
            _listOfPatients.Clear();
            _uut.SortForTriage(_listOfPatients);
            Assert.That(_detailViewModel.ListOfTriages[0].Amount, Is.EqualTo(0));
        }

        // Test an empty list; no triage patients in DetailView model
        [Test]
        public void SortForTriage_ListWithoutPatients_NoSpecialtiesPatientsSendToModel()
        {
            _listOfPatients.Clear();
            _uut.SortForTriage(_listOfPatients);
            Assert.That(_detailViewModel.ListOfTriagePatientLists, Is.Empty);
        }

        // Test a list with 2 patients with different triages
        [Test]
        public void SortForTriage_ListWith2DifferentSpecialties_AddSortedListWith2ListsToModel()
        {
            _uut.SortForTriage(_listOfPatients);
            Assert.That(_detailViewModel.ListOfTriagePatientLists.Count, Is.EqualTo(2));
        }

        // Test a list with 3 patients with different triages
        [Test]
        public void SortForTriage_ListWith3DifferentSpecialties_AddSortedListWith3ListsToModel()
        {
            _patient3 = new PatientModel
            {
                PatientId = "3",
                Name = "Patient Three",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = "TriageGreen",
                Specialty = "Medicinal",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 21, 30, 00)
            };
            _listOfPatients.Add(_patient3);
            _uut.SortForTriage(_listOfPatients);
            Assert.That(_detailViewModel.ListOfTriagePatientLists.Count, Is.EqualTo(3));
        }

        // Test a list with patient with triage unknown to system
        [Test]
        public void SortForTriage_ListWithUnknownTriage_AddSortedListWith3ListsToModel()
        {
            _patient3 = new PatientModel
            {
                PatientId = "3",
                Name = "Patient Three",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = "TriageBlack",
                Specialty = "Test specialty",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 21, 30, 00)
            };
            _listOfPatients.Add(_patient3);
            _uut.SortForTriage(_listOfPatients);
            // Unknown triages are added last to the List of triages
            Assert.That(_detailViewModel.ListOfTriagePatientLists.Last().Count, Is.EqualTo(1));
        }


        #endregion






    }
}
