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
using NSubstitute.ClearExtensions;
using NUnit.Framework;

namespace IncomingCasualtyHandling.Test.Unit.BL.Test.Unit
{

    [TestFixture]
    public class SortTriageTest
    {
        #region Arrange

        private ISortTriage _uut;

        private ILoadConfigurationSettings _loadConfigSettings;
        private IOverviewView_Model _overviewViewModel;
        private IDetailView_Model _detailViewModel;
        private IMainView_Model _mainViewModel;

        private ISortETA _sortEta;


        private List<Triage> _listOfTriages;
        private List<ICHPatient> _listOfPatients, _sortedListOfPatients;
        private ICHPatient _patient1, _patient2, _patient3, _patient4;
        string unknownTriageName = "TriageUnknown";

        [SetUp]
        public void Setup()
        {
            _loadConfigSettings = Substitute.For<ILoadConfigurationSettings>();
            _overviewViewModel = Substitute.For<IOverviewView_Model>();
            _detailViewModel = Substitute.For<IDetailView_Model>();
            _mainViewModel = Substitute.For<IMainView_Model>();
            _sortEta = Substitute.For<ISortETA>();

            _listOfTriages = new List<Triage>();
            _listOfTriages.Add(new Triage { Name = "TriageRed" });
            _listOfTriages.Add(new Triage { Name = "TriageOrange" });
            _listOfTriages.Add(new Triage { Name = "TriageYellow" });
            _listOfTriages.Add(new Triage { Name = "TriageGreen" });
            _listOfTriages.Add(new Triage { Name = "TriageBlue" });


            _loadConfigSettings.ReturnTriageList().Returns(_listOfTriages);

            _uut = new SortTriage(_loadConfigSettings, _detailViewModel, _mainViewModel,
                    _sortEta);

            // Create a list with patients
            _listOfPatients = new List<ICHPatient>();
            _sortedListOfPatients = new List<ICHPatient>();
            _patient1 = new ICHPatient

            {
                CPR = "1",
                Name = "Patient One",
                Age = "10",
                Gender = AdministrativeGender.Male,
                Triage = "TriageRed",
                Specialty = "Neurology",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 22, 30, 00)
            };
            _patient2 = new ICHPatient
            {
                CPR = "2",
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
        public void SortForTriage_ListWith2DifferentTriages_AddSortedListWith2ListsToModel()
        {
            _uut.SortForTriage(_listOfPatients);
            Assert.That(_detailViewModel.ListOfTriagePatientLists.Count, Is.EqualTo(2));
        }

        // Test a list with 3 patients with different triages
        [Test]
        public void SortForTriage_ListWith3DifferentSpecialties_AddSortedListWith3ListsToModel()
        {
            _patient3 = new ICHPatient
            {
                CPR = "3",
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
            _patient3 = new ICHPatient
            {
                CPR = "3",
                Name = "Patient Three",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = "TriageBlack",
                Specialty = "Medicinal",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 21, 30, 00)
            };
            _listOfPatients.Add(_patient3);

            ICHPatient patient3UnknownTriage = new ICHPatient
            {
                CPR = "3",
                Name = "Patient Three",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = unknownTriageName,
                Specialty = "",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 21, 30, 00)
            };
            _sortedListOfPatients.Add(patient3UnknownTriage);
            _sortEta.SortListOnEta(_listOfPatients).ReturnsForAnyArgs(_sortedListOfPatients);

            _uut.SortForTriage(_listOfPatients);
            // Unknown triages are added last to the List of triages
            Assert.That(_detailViewModel.ListOfTriagePatientLists.Last().Exists(p => p.Name == "Patient Three"), Is.True);
        }

        // Test a list with patient without triage
        [Test]
        public void SortForSpecialty_ListWithPatientWithoutTriage_PatientEndsInUnknownTriageList()
        {
            _patient3 = new ICHPatient
            {
                CPR = "3",
                Name = "Patient Three",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = "",
                Specialty = "",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 21, 30, 00)
            };
            _listOfPatients.Add(_patient3);

            ICHPatient patient3Wut = new ICHPatient
            {
                CPR = "3",
                Name = "Patient Three",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = unknownTriageName,
                Specialty = "",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 21, 30, 00)
            };
            _sortedListOfPatients.Add(patient3Wut);
            _sortEta.SortListOnEta(_listOfPatients).ReturnsForAnyArgs(_sortedListOfPatients);

            _uut.SortForTriage(_listOfPatients);
            // Unknown triages are added last to the List of triages
            Assert.That(_detailViewModel.ListOfTriagePatientLists.Last().Exists(p => p.Name == "Patient Three"), Is.True);
        }

        // Test a list with patients without triages and one without ETA too
        [Test]
        public void SortForSpecialty_ListWithPatientWithoutTriageAndEta_PatientEndsLastInUnknownTriageList()
        {
            _patient1.Triage = _listOfTriages[3].Name;
            _patient3 = new ICHPatient
            {
                CPR = "3",
                Name = "Patient Three",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = unknownTriageName,
                Specialty = "",
                ToHospital = "AUH",
            };
            _patient4 = new ICHPatient
            {
                CPR = "4",
                Name = "Patient Four",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = "",
                Specialty = "",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 21, 30, 00)
            };
            _listOfPatients.Add(_patient3);
            _listOfPatients.Add(_patient4);

            List<ICHPatient> unknownTriagePatients = new List<ICHPatient>();
            
            ICHPatient patient3UnknownTriage = new ICHPatient
            {
                CPR = "3",
                Name = "Patient Three",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = unknownTriageName,
                Specialty = "",
                ToHospital = "AUH",
            };
            ICHPatient patient4UnknownTriage = new ICHPatient
            {
                CPR = "4",
                Name = "Patient Four",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = unknownTriageName,
                Specialty = "",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 21, 30, 00)
            };
            unknownTriagePatients.Add(patient3UnknownTriage);
            unknownTriagePatients.Add(patient4UnknownTriage);

            _sortedListOfPatients.Add(patient4UnknownTriage);
            _sortedListOfPatients.Add(patient3UnknownTriage);

            _sortEta.SortListOnEta(unknownTriagePatients).ReturnsForAnyArgs(_sortedListOfPatients);

            _uut.SortForTriage(_listOfPatients);
            // Unknown triages are added last to the List of triages
            Assert.That(_detailViewModel.ListOfTriagePatientLists.Last().IndexOf(patient3UnknownTriage), Is.EqualTo(_detailViewModel.ListOfTriagePatientLists.Last().Count-1));
        }

        #endregion






    }
}
