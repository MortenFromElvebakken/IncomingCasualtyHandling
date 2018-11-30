using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;
using IncomingCasualtyHandling.BL;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL.Interface;
using NSubstitute;
using NUnit.Framework;

namespace IncomingCasualtyHandling.Test.Unit.BL.Test.Unit
{
    
    // Arrange - Act - Assert

    [TestFixture]
    public class SortSpecialtyTest
    {
        #region Arrange

        private ISortSpecialty _uut;

        private ILoadConfigurationSettings _loadConfigSettings;
        private IOverviewView_Model _overviewViewModel;
        private IDetailView_Model _detailViewModel;
        private IMainView_Model _mainViewModel;

        private ISortETA _sortEta;


        private List<Specialty> _listOfSpecialties;
        private List<ICHPatient> _listOfPatients, _sortedListOfPatients;
        private ICHPatient _patient1, _patient2, _patient3;

        [SetUp]
        public void Setup()
        {
            _loadConfigSettings = Substitute.For<ILoadConfigurationSettings>();
            _overviewViewModel = Substitute.For<IOverviewView_Model>();
            _detailViewModel = Substitute.For<IDetailView_Model>();
            _mainViewModel = Substitute.For<IMainView_Model>();
            _sortEta = Substitute.For<ISortETA>();
            

            _listOfSpecialties = new List<Specialty>();
            _listOfSpecialties.Add(new Specialty{Name = "Diagnostic radiology"});
            _listOfSpecialties.Add(new Specialty { Name = "Emergency medicine" });
            _listOfSpecialties.Add(new Specialty { Name = "Neurology" });
            _listOfSpecialties.Add(new Specialty { Name = "Obstetrics and Gynecology" });
            _listOfSpecialties.Add(new Specialty { Name = "Opthalmology" });
            _listOfSpecialties.Add(new Specialty { Name = "Orthopaedic surgery" });
            _listOfSpecialties.Add(new Specialty { Name = "Otolaryngology" });
            _listOfSpecialties.Add(new Specialty { Name = "Pathology-Anatomic and Clinical" });
            _listOfSpecialties.Add(new Specialty { Name = "Pediatrics" });
            _listOfSpecialties.Add(new Specialty { Name = "Plastic surgery" });
            _listOfSpecialties.Add(new Specialty { Name = "Psychiatry" });
            _listOfSpecialties.Add(new Specialty { Name = "Surgery-general" });
            _listOfSpecialties.Add(new Specialty { Name = "Thoracic sugery" });
            _listOfSpecialties.Add(new Specialty { Name = "Unknown" });
            _listOfSpecialties.Add(new Specialty { Name = "Urology" });
            _listOfSpecialties.Add(new Specialty { Name = "Vascular surgery" });


            _loadConfigSettings.ReturnSpecialtyList().Returns(_listOfSpecialties);

            _uut = new SortSpecialty(_loadConfigSettings, _overviewViewModel, _detailViewModel, _mainViewModel, _sortEta);

            // Create a list with patients
            _listOfPatients = new List<ICHPatient>();
            _sortedListOfPatients = new List<ICHPatient>();
            _patient1 = new ICHPatient

            {
                CPR = "1",
                Name = "Patient One",
                Age = "10",
                Gender = AdministrativeGender.Male,
                Triage = new Triage() { Name = "TriageRed" },
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
                Triage = new Triage() { Name = "TriageYellow" },
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

        // Test an empty list; no amounts in specialties
        [Test]
        public void SortForSpecialty_ListWithoutPatients_NoAmountInSpecialties()
        {
            _listOfPatients.Clear();
            _uut.SortForSpecialty(_listOfPatients);
            Assert.That(_detailViewModel.ListOfSpecialties[0].Amount, Is.EqualTo(0));
        }

        // Test an empty list; no specialty patients
        [Test]
        public void SortForSpecialty_ListWithoutPatients_NoSpecialtiesPatientsSendToModel()
        {
            _listOfPatients.Clear();
            _uut.SortForSpecialty(_listOfPatients);
            Assert.That(_detailViewModel.ListOfSpecialtiesPatientLists, Is.Empty);
        }

        // Test a list with 2 patients with different specialties
        [Test]
        public void SortForSpecialty_ListWith2DifferentSpecialties_AddSortedListWith2ListsToModel()
        {
            _uut.SortForSpecialty(_listOfPatients);
            Assert.That(_detailViewModel.ListOfSpecialtiesPatientLists.Count, Is.EqualTo(2));
        }

        // Test a list with 2 patients with different specialties, place them alphabetically correct
        [Test]
        public void SortForSpecialty_ListWith2DifferentSpecialties_CorrectSpecialtyOnFirstSpotInListWith2ListsInModel()
        {
            List<ICHPatient> unknownList = new List<ICHPatient>();
            unknownList.Add(_patient2);
            _sortEta.SortListOnEta(new List<ICHPatient>()).ReturnsForAnyArgs(unknownList);
            _uut.SortForSpecialty(_listOfPatients);
            Assert.IsTrue(_detailViewModel.ListOfSpecialtiesPatientLists[0].Exists(p => p.Specialty=="Neurology"));
        }

        // Test a list with 3 patients with different specialties
        [Test]
        public void SortForSpecialty_ListWith3DifferentSpecialties_AddSortedListWith3ListsToModel()
        {
            _patient3 = new ICHPatient
            {
                CPR = "3",
                Name = "Patient Three",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = new Triage() { Name = "TriageYellow" },
                Specialty = _listOfSpecialties[1].Name,
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 21, 30, 00)
            };
            _listOfPatients.Add(_patient3);
            _uut.SortForSpecialty(_listOfPatients);
            Assert.That(_detailViewModel.ListOfSpecialtiesPatientLists.Count, Is.EqualTo(3));
        }

        // Test a list with 3 patients with 2 different specialties, test that the one with greatest amount is placed first
        [Test]
        public void SortForSpecialty_ListWith3DifferentSpecialties_CorrectSpecialtyOnFirstSpotInListWith2ListsInModel()
        {
            _patient3 = new ICHPatient
            {
                CPR = "3",
                Name = "Patient Three",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = new Triage() { Name = "TriageYellow" },
                Specialty = _patient1.Specialty,
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 21, 30, 00)
            };
            _listOfPatients.Add(_patient3);
            _uut.SortForSpecialty(_listOfPatients);
            Assert.IsTrue(_detailViewModel.ListOfSpecialtiesPatientLists[0].Exists(p => p.Specialty == "Neurology"));
        }

        // Test a list with patients with specialties unknown to system
        [Test]
        public void SortForSpecialty_ListWith2UnknownSpecialties_AddSortedListWith2ListsToModel()
        {
            _patient3 = new ICHPatient
            {
                CPR = "3",
                Name = "Patient Three",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = new Triage() { Name = "TriageYellow" },
                Specialty = "Test specialty",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 21, 30, 00)
            };
            _listOfPatients.Add(_patient3);

            _sortedListOfPatients.Add(_patient3);
            _sortedListOfPatients.Add(_patient2); // The 2 patients have same ETA, but patient2's name comes later in the alphabet than patient 3
            _sortEta.SortListOnEta(_listOfPatients).ReturnsForAnyArgs(_sortedListOfPatients);

            _uut.SortForSpecialty(_listOfPatients);
            // Unknown specialties are added last to the List of Specialties
            Assert.That(_detailViewModel.ListOfSpecialtiesPatientLists.Last().Count, Is.EqualTo(2));
        }

        // Test a list with patient without specialty
        [Test]
        public void SortForSpecialty_ListWithPatientWithoutSpecialty_PatientEndsInUnknownSpecialtyList()
        {
            _patient3 = new ICHPatient
            {
                CPR = "3",
                Name = "Patient Three",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = new Triage() { Name = "TriageYellow" },
                Specialty = "",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 21, 30, 00)
            };
            _listOfPatients.Add(_patient3);

            _sortedListOfPatients.Add(_patient3);
            _sortedListOfPatients.Add(_patient2); // The 2 patients have same ETA, but patient2's name ("Two") comes later in the alphabet than patient 3 ("Three")
            _sortEta.SortListOnEta(_listOfPatients).ReturnsForAnyArgs(_sortedListOfPatients);

            _uut.SortForSpecialty(_listOfPatients);
            // Unknown specialties are added last to the List of Specialties
            Assert.That(_detailViewModel.ListOfSpecialtiesPatientLists.Last().Exists(p => p.Name == "Patient Three"), Is.True);
        }

        // Test that specialty with most patients is set in MainView Model
        [Test]
        public void SortForSpecialty_MostNeurologyPatients_Specialty1IsNeurology()
        {
            _patient3 = new ICHPatient
            {
                CPR = "3",
                Name = "Patient Three",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = new Triage() { Name = "TriageYellow" },
                Specialty = "Neurology",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 21, 30, 00)
            };
            _listOfPatients.Add(_patient3);

            _uut.SortForSpecialty(_listOfPatients);

            // Unknown specialties are added last to the List of Specialties
            Assert.That(_mainViewModel.Specialty1.Name, Is.SameAs(_listOfSpecialties.Find(s => s.Name == "Neurology").Name));
        }

        #endregion






    }
}

