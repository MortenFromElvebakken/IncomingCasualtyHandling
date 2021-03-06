﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL;
using IncomingCasualtyHandling.DAL.Interface;
using NSubstitute;
using NUnit.Framework;

namespace IncomingCasualtyHandling.Test.Unit.DAL.Test.Unit
{
    [TestFixture]
    public class ConvertToICHPatientTest
    {
        #region Arrange

        private IConvertToICHPatient _uut;
        private ILoadConfigurationSettings _loadConfig;
        public Patient Patient1;

        string triage = "TriageRed";
        string specialty = "Emergency medicine";
        DateTime eta = new DateTime(2018, 11, 22, 12, 00, 00, DateTimeKind.Local);
        string cpr = "201120001518";
        string givenName = "Test";
        string familyName = "Testson";
        string wholeName;
        private AdministrativeGender gender = AdministrativeGender.Unknown;
        private string toHospital = "Unknown";
        private DateTimeOffset lastUpdated = new DateTimeOffset(2018,11,22,8,0,0, new TimeSpan(0,0,0,0));

        private List<Triage> _triageList = new List<Triage>()
        {
            new Triage(){Name = "TriageRed"}
        };
        [SetUp]
        public void Setup()
        {
            _loadConfig = Substitute.For<ILoadConfigurationSettings>();
            _uut = new ConvertToICHPatient(_loadConfig);
            _loadConfig.ReturnTriageList().Returns(_triageList);
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
            

        }


        #endregion

        #region Act and Assert
        // Method_Scenario_Result

        //Correct convertion of ETA
        [Test]
        public void ReturnPatient_ReceivedCompletePatient_CreatesPatientEta()
        {
            ICHPatient ichPatient = _uut.ReturnPatient(Patient1);
            Assert.That(ichPatient.ETA, Is.EqualTo(eta));
        }

        //Correct convertion of Gender
        [Test]
        public void ReturnPatient_ReceivedCompletePatient_CreatesPatientGender()
        {
            ICHPatient ichPatient = _uut.ReturnPatient(Patient1);
            Assert.That(ichPatient.Gender, Is.EqualTo(gender));
        }

        //Correct convertion of Name
        [Test]
        public void ReturnPatient_ReceivedCompletePatient_CreatesPatientName()
        {
            ICHPatient ichPatient = _uut.ReturnPatient(Patient1);
            Assert.That(ichPatient.Name, Is.EqualTo(wholeName));
        }

        // Correct convertion of CPR
        [Test]
        public void ReturnPatient_ReceivedCompletePatient_CreatesPatientId()
        {
            ICHPatient ichPatient = _uut.ReturnPatient(Patient1);
            Assert.That(ichPatient.CPR, Is.EqualTo(cpr));
        }

        //Correct convertion of Specialty
        [Test]
        public void ReturnPatient_ReceivedCompletePatient_CreatesPatientSpecialty()
        {
            ICHPatient ichPatient = _uut.ReturnPatient(Patient1);
            Assert.That(ichPatient.Specialty, Is.EqualTo(specialty));
        }

        //Correct convertion of Triage
        [Test]
        public void ReturnPatient_ReceivedCompletePatient_CreatesPatientTriage()
        {
            ICHPatient ichPatient = _uut.ReturnPatient(Patient1);
            Assert.That(ichPatient.Triage.Name, Is.EqualTo(triage));
        }

        //Correct convertion of ToHospital
        [Test]
        public void ReturnPatient_ReceivedPatientWithoutToHospital_CreatesPatientToHospitalCorrect()
        {
            ICHPatient ichPatient = _uut.ReturnPatient(Patient1);
            Assert.That(ichPatient.ToHospital, Is.EqualTo(toHospital));
        }

        #endregion






    }
}
