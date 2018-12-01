using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    public class CountTimeTest
    {
        #region Arrange

        private CountTime _uut;

        private IOverviewView_Model _overviewViewModel;
        private IMainView_Model _mainViewModel;

        private List<ICHPatient> _listOfPatients;
        private ICHPatient _patient1, _patient2;

        [SetUp]
        public void Setup()
        {
            _overviewViewModel = Substitute.For<IOverviewView_Model>();
            _mainViewModel = Substitute.For<IMainView_Model>();
            _uut = new CountTime(_mainViewModel);            

            // Create a list with patients
            _listOfPatients = new List<ICHPatient>();
            _patient1 = new ICHPatient

            {
                CPR = "1",
                Name = "Patient One",
                Age = "10",
                Gender = AdministrativeGender.Male,
                Triage = new Triage(){Name = "TriageRed", },
                Specialty = "Medicinal",
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

        // Find the next coming patient and calculate ETA, put in Main Model
        [Test]
        public void FindRelativeTime_RelativeTimeCalculated_MainModelUpdated()
        {
            string twoHoursInMinutes = "(-120 minutes)";
            _patient1.ETA = DateTime.Now.AddHours(2);
            _patient2.ETA = DateTime.Now.AddHours(3);
            _uut.FindRelativeTime(_listOfPatients);

            Assert.That(_mainViewModel.ETA.RelativeTime, Is.EqualTo(twoHoursInMinutes));
        }

        // Relative ETA is counted down, Main Model
        [Test]
        public void ETATimeTimerTick_RelativeTimeIsReducedWithAMinute_MainModelUpdated()
        {
            string afterAMinute = "(-119 minutes)";
            _patient1.ETA = DateTime.Now.AddHours(2);
            _patient2.ETA = DateTime.Now.AddHours(3);
            _uut.FindRelativeTime(_listOfPatients);

            Thread.Sleep(65000); //Wait at least a minute

            Assert.That(_mainViewModel.ETA.RelativeTime, Is.EqualTo(afterAMinute));
        }

        // Find the next coming patient and calculate ETA, put in Main Model
        [Test]
        public void ETATimeTimerTick_RelativeTimeIsReducedWithAMinute_OverviewViewModelUpdated()
        {
            string twoHoursInMinutes = "(-120 minutes)";
            _patient1.ETA = DateTime.Now.AddHours(2);
            _patient2.ETA = DateTime.Now.AddHours(3);
            _uut.FindRelativeTime(_listOfPatients);

            Assert.That(_mainViewModel.ETA.RelativeTime, Is.EqualTo(twoHoursInMinutes));
        }

        // Test that only future ETA's are used
        [Test]
        public void FindRelativeTime_FirstPatientETAIsBeforeNow_NextPatientsETAIsInModel()
        {
            string threeHoursInMinutes = "(-180 minutes)";
            _patient1.ETA = DateTime.Now.Subtract(new TimeSpan(0,2,0,0));
            _patient2.ETA = DateTime.Now.AddHours(3);
            _uut.FindRelativeTime(_listOfPatients);

            Assert.That(_mainViewModel.ETA.RelativeTime, Is.EqualTo(threeHoursInMinutes));
        }

        // Test reaction to no future ETAs
        [Test]
        public void FindRelativeTime_NoFutureETAs_ModelContainsRightUnknownAbsoluteTime()
        {
            _patient1.ETA = DateTime.Now.Subtract(new TimeSpan(0, 2, 0, 0));
            _patient2.ETA = DateTime.Now.Subtract(new TimeSpan(0, 2, 0, 0));
            _uut.FindRelativeTime(_listOfPatients);

            Assert.That(_mainViewModel.ETA.AbsoluteTime, Is.EqualTo("--:--"));
        }

        // Test reaction to no future ETAs
        [Test]
        public void FindRelativeTime_NoFutureETAs_ModelContainsRightUnknownRelativeTime()
        {
            _patient1.ETA = DateTime.Now.Subtract(new TimeSpan(0, 2, 0, 0));
            _patient2.ETA = DateTime.Now.Subtract(new TimeSpan(0, 2, 0, 0));
            _uut.FindRelativeTime(_listOfPatients);

            Assert.That(_mainViewModel.ETA.RelativeTime, Is.EqualTo(""));
        }

        // Test reaction to ETA further into the future than 999 minutes
        [Test]
        public void FindRelativeTime_ETAFurtherIntoTheFuture_ModelContainsRightAbsoluteTime()
        {
            _patient1.ETA = DateTime.Now.AddDays(1);
            _listOfPatients.Remove(_patient2);
            _uut.FindRelativeTime(_listOfPatients);

            var absoluteTime =_patient1.ETA.ToShortTimeString();

            Assert.That(_mainViewModel.ETA.AbsoluteTime, Is.EqualTo(absoluteTime));
        }

        // Test reaction to ETA further into the future than 999 minutes
        [Test]
        public void FindRelativeTime_ETAFurhterIntoTheFuture_ModelContainsRightRlativeTime()
        {
            _patient1.ETA = DateTime.Now.AddDays(1);
            _listOfPatients.Remove(_patient2);
            _uut.FindRelativeTime(_listOfPatients);

            Assert.That(_mainViewModel.ETA.RelativeTime, Is.EqualTo("(>999 minutes)"));
        }

        #endregion






    }
}
