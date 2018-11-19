using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    [TestFixture]
    public class SortingListOnETATest
    {

        #region Arrange

        private ISortingListOnETA _uut;
        private List<PatientModel> _listOfPatients, _sortedListOfPatients;
        private PatientModel _patient1, _patient2, _patient3, _patient4, _patient5;

        private int _nEventsRaised;
        private PatientListEventArgs _receivedArgs;

        [SetUp]
        public void Setup()
        {
            _uut = new SortingListOnETA();

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
                ToHospital = "AUH"
            };
            _patient3 = new PatientModel
            {
                PatientId = "3",
                Name = "Patient Three",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = "TriageYellow",
                Specialty = "Psychology",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 21, 30, 00)
            };

            _listOfPatients.Add(_patient1);
            _listOfPatients.Add(_patient2);
            _listOfPatients.Add(_patient3);

            _nEventsRaised = 0;

            // Assign to events
            _uut.SortedListReady += (o, args) => {
                ++_nEventsRaised;
                _receivedArgs = args;

            };
        }

        #endregion

        #region Act and Assert
        // Metode_Scenarie_Resultat

        // Test that event is raised
        [Test]
        public void SortListOnETA_SortAList_RaiseEvent()
        {
            _uut.SortListOnETA(_listOfPatients);
            Assert.That(_nEventsRaised, Is.EqualTo(1));
        }

        // Test that empty ETA is put at end
        [Test]
        public void SortListOnETA_PatientWithoutETA_PatientPlacedLast()
        {
            _sortedListOfPatients.Add(_patient3); // ETA 18-11-18, 21:30
            _sortedListOfPatients.Add(_patient1); // ETA 18-11-18, 22:30
            _sortedListOfPatients.Add(_patient2); // ETA null
            _uut.SortListOnETA(_listOfPatients);
            Assert.That(_receivedArgs.SortedList, Is.EqualTo(_sortedListOfPatients));
        }

        // Test that empty ETAs are put at end in alphabetical order
        [Test]
        public void SortListOnETA_PatientsWithoutETA_PatientsPlacedLastAlphabetically()
        {
            _patient4 = new PatientModel
            {
                PatientId = "4",
                Name = "Alma",
                Age = "30",
                Gender = AdministrativeGender.Female,
                Triage = "TriageYellow",
                Specialty = "Psychology",
                ToHospital = "AUH"
            };

            _listOfPatients.Add(_patient4);
            _sortedListOfPatients.Add(_patient3); // ETA 18-11-18, 21:30
            _sortedListOfPatients.Add(_patient1); // ETA 18-11-18, 22:30
            _sortedListOfPatients.Add(_patient4); // ETA null, name Alma
            _sortedListOfPatients.Add(_patient2); // ETA null, name Patient Two
            _uut.SortListOnETA(_listOfPatients);
            Assert.That(_receivedArgs.SortedList, Is.EqualTo(_sortedListOfPatients));
        }


        


        #endregion






    }
}
