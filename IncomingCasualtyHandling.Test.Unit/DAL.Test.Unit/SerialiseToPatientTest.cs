using System;
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
    public class SerialiseToPatientTest
    {
        #region Arrange

        private ISerializeToPatient _uut;

        public Patient Patient1;

        string triage = "TriageRed";
        string specialty = "Emergency medicine";
        DateTime eta = new DateTime(2018, 11, 22, 12, 00, 00, DateTimeKind.Local);
        string cpr = "201120001518";
        string givenName = "Test";
        string familyName = "Testson";
        string wholeName;
        private AdministrativeGender gender = AdministrativeGender.Unknown;
        private string toHospital = "To hospital went wrong";

        [SetUp]
        public void Setup()
        {
            _uut = new SerialiseToPatient();

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

        }


        #endregion

        #region Act and Assert
        // Method_Scenario_Result

        //Correct serialisation of ETA
        [Test]
        public void ReturnPatient_ReceivedCompletePatient_CreatesPatientEta()
        {
            PatientModel patient = _uut.ReturnPatient(Patient1);
            Assert.That(patient.ETA, Is.EqualTo(eta));
        }

        //Correct serialisation of Gender
        [Test]
        public void ReturnPatient_ReceivedCompletePatient_CreatesPatientGender()
        {
            PatientModel patient = _uut.ReturnPatient(Patient1);
            Assert.That(patient.Gender, Is.EqualTo(gender));
        }

        //Correct serialisation of Name
        [Test]
        public void ReturnPatient_ReceivedCompletePatient_CreatesPatientName()
        {
            PatientModel patient = _uut.ReturnPatient(Patient1);
            Assert.That(patient.Name, Is.EqualTo(wholeName));
        }

        // Correct serialisation of CPR
        [Test]
        public void ReturnPatient_ReceivedCompletePatient_CreatesPatientId()
        {
            PatientModel patient = _uut.ReturnPatient(Patient1);
            Assert.That(patient.PatientId, Is.EqualTo(cpr));
        }

        //Correct serialisation of Specialty
        [Test]
        public void ReturnPatient_ReceivedCompletePatient_CreatesPatientSpecialty()
        {
            PatientModel patient = _uut.ReturnPatient(Patient1);
            Assert.That(patient.Specialty, Is.EqualTo(specialty));
        }

        //Correct serialisation of Triage
        [Test]
        public void ReturnPatient_ReceivedCompletePatient_CreatesPatientTriage()
        {
            PatientModel patient = _uut.ReturnPatient(Patient1);
            Assert.That(patient.Triage, Is.EqualTo(triage));
        }

        //Correct serialisation of ToHospital
        [Test]
        public void ReturnPatient_ReceivedPatientWithoutToHospital_CreatesPatientToHospitalCorrect()
        {
            PatientModel patient = _uut.ReturnPatient(Patient1);
            Assert.That(patient.ToHospital, Is.EqualTo(toHospital));
        }

        #endregion






    }
}
