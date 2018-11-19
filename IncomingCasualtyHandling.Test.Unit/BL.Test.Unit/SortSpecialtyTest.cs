﻿using System;
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
        //private IOverviewView_Model _overviewViewModel;
        private OverviewView_Model _overviewViewModel;
        private IDetailView_Model _detailViewModel;
        //private DetailView_Model _detailViewModel;
        //private IMainView_Model _mainViewModel;
        private MainView_Model _mainViewModel;

        private IGetPatientsFromFHIR _getPatientsFromFHIR;


        private List<Specialty> _listOfSpecialties;
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


            _loadConfigSettings.SpecialtiesList = _listOfSpecialties;

            _uut = new SortSpecialty(_loadConfigSettings, _overviewViewModel, _detailViewModel, _mainViewModel, _getPatientsFromFHIR);

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

        // Test an empty list; no specialties
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

        // Test a list with 3 patients with different specialties
        [Test]
        public void SortForSpecialty_ListWith3DifferentSpecialties_AddSortedListWith3ListsToModel()
        {
            _patient3 = new PatientModel
            {
                PatientId = "3",
                Name = "Patient Three",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = "TriageYellow",
                Specialty = _listOfSpecialties[1].Name,
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 21, 30, 00)
            };
            _listOfPatients.Add(_patient3);
            _uut.SortForSpecialty(_listOfPatients);
            Assert.That(_detailViewModel.ListOfSpecialtiesPatientLists.Count, Is.EqualTo(3));
        }

        // Test a list with patients with specialties unknown to system
        [Test]
        public void SortForSpecialty_ListWith2UnknownSpecialties_AddSortedListWith3ListsToModel()
        {
            _patient3 = new PatientModel
            {
                PatientId = "3",
                Name = "Patient Three",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = "TriageYellow",
                Specialty = "Test specialty",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 21, 30, 00)
            };
            _listOfPatients.Add(_patient3);
            _uut.SortForSpecialty(_listOfPatients);
            // Unknown specialties are added last to the List of Specialties
            Assert.That(_detailViewModel.ListOfSpecialtiesPatientLists.Last().Count, Is.EqualTo(2));
        }

        // Test a list with patient without specialty
        [Test]
        public void SortForSpecialty_ListWithPatientWithoutSpecialty_PatientEndsInUnknownSpecialtyList()
        {
            _patient3 = new PatientModel
            {
                PatientId = "3",
                Name = "Patient Three",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = "TriageYellow",
                Specialty = "",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 21, 30, 00)
            };
            _listOfPatients.Add(_patient3);
            _uut.SortForSpecialty(_listOfPatients);
            // Unknown specialties are added last to the List of Specialties
            Assert.That(_detailViewModel.ListOfSpecialtiesPatientLists.Last().Exists(p => p.Name == "Patient Three"), Is.True);
        }

        // Test a list with patients 2 patients with same Specialty and ETA
        // Check that they are sorted alphabetically as second sorting
        [Test]
        public void SortForSpecialty_ListWithEqualSpecialties_SecondlySortedAlphabetically()
        {
            _patient3 = new PatientModel
            {
                PatientId = "3",
                Name = "Alma",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = "TriageYellow",
                Specialty = _listOfPatients[0].Specialty,
                ToHospital = "AUH",
                ETA = _listOfPatients[0].ETA
            };
            _listOfPatients.Add(_patient3);

            // Create the test sorted list that the detail view models list should match
            _sortedListOfPatients.Add(_patient3); // "Alma" comes first in the alphabet
            _sortedListOfPatients.Add(_patient1); // compared to "Patient One"


            _uut.SortForSpecialty(_listOfPatients);

            Assert.That(_detailViewModel.ListOfSpecialtiesPatientLists.Find(s => s[0].Specialty == _listOfPatients[0].Specialty), Is.EqualTo(_sortedListOfPatients));
        }

        #endregion






    }
}

