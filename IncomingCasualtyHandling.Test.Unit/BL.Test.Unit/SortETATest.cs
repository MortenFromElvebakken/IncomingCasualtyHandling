﻿using System;
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
    public class SortETATest
    {
        #region Arrange

        private ISortETA _uut;

        private IOverviewView_Model _overviewViewModel;
        private IDetailView_Model _detailViewModel;
        private IMainView_Model _mainViewModel;

        private ICountTime _countTime;
        private IGetPatientsFromFHIR _getPatientsFromFHIR;

        private List<PatientModel> _listOfPatients, _sortedListOfPatients;
        private PatientModel _patient1, _patient2, _patient3, _patient4, _patient5;

        [SetUp]
        public void Setup()
        {
            _overviewViewModel = Substitute.For<IOverviewView_Model>();
            _detailViewModel = Substitute.For<IDetailView_Model>();
            _mainViewModel = Substitute.For<IMainView_Model>();
            _countTime = Substitute.For<ICountTime>();
            _getPatientsFromFHIR = Substitute.For<IGetPatientsFromFHIR>();
            _uut = new SortETA(_overviewViewModel, _detailViewModel, _mainViewModel, _countTime, _getPatientsFromFHIR);

            // Create a list with patients
            _listOfPatients = new List<PatientModel>();
            _sortedListOfPatients = new List<PatientModel>();
            _patient1 = new PatientModel

            {
                CPR = "1",
                Name = "Patient One",
                Age = "10",
                Gender = AdministrativeGender.Male,
                Triage = "TriageRed",
                Specialty = "Medicinal",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 22, 30, 00)
            };
            _patient2 = new PatientModel
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

        // Test an empty list
        [Test]
        public void SortForETA_ListWithoutPatients_DoNothing()
        {
            _listOfPatients.Clear();
            _uut.SortForETA(_listOfPatients);
            Assert.That(_detailViewModel.ETAPatients, Is.Empty);
        }

        // Test a list with patients
        [Test]
        public void SortForETA_ListWithPatients_AddSortedListToModel()
        {
            _sortedListOfPatients.Add(_patient2);
            _sortedListOfPatients.Add(_patient1);
            _uut.SortForETA(_listOfPatients);
            Assert.That(_detailViewModel.ETAPatients, Is.EqualTo(_sortedListOfPatients));
        }

        // Test a list with 2 patients with same ETA
        // Check that they are sorted alphabetically as second sorting
        [Test]
        public void SortForETA_ListWithEqualETAs_SecondlySortedAlphabetically()
        {
            _patient3 = new PatientModel
            {
                CPR = "3",
                Name = "Patient Three",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = "TriageYellow",
                Specialty = "Psychology",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 21, 30, 00)
            };
            _listOfPatients.Add(_patient3);

            _uut.SortForETA(_listOfPatients);

            _sortedListOfPatients.Add(_patient3);
            _sortedListOfPatients.Add(_patient2);
            _sortedListOfPatients.Add(_patient1);

            Assert.That(_detailViewModel.ETAPatients, Is.EqualTo(_sortedListOfPatients));
        }

        // Test with a patient without ETA
        [Test]
        public void SortForETA_PatientWithoutETA_PlaceLastInList()
        {
            _patient3 = new PatientModel
            {
                CPR = "3",
                Name = "Patient Three",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = "TriageYellow",
                Specialty = "Psychology",
                ToHospital = "AUH"
            };
            _listOfPatients.Add(_patient3);
            _uut.SortForETA(_listOfPatients);
            Assert.That(_detailViewModel.ETAPatients.Last().Name, Is.EqualTo(_patient3.Name));
        }

        // Test with patients without ETA
        [Test]
        public void SortForETA_PatientsWithoutETA_PlaceAlphabeticallyLastInList()
        {
            _patient3 = new PatientModel
            {
                CPR = "3",
                Name = "Patient Three",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = "TriageYellow",
                Specialty = "Psychology",
                ToHospital = "AUH"
            };
            _patient4 = new PatientModel
            {
                CPR = "3",
                Name = "Alma",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = "TriageYellow",
                Specialty = "Psychology",
                ToHospital = "AUH"
            };
            _listOfPatients.Add(_patient3);
            _listOfPatients.Add(_patient4);
            _uut.SortForETA(_listOfPatients);
            Assert.That(_detailViewModel.ETAPatients.Last().Name, Is.EqualTo(_patient3.Name));
        }

        #endregion






    }
}
