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

        private Patient _patient1;
        private PatientModel _patientModel1;

        [SetUp]
        public void Setup()
        {
            _uut = new SerialiseToPatient();

            _patient1 = new Patient()
            {
                Identifier = new System.Collections.Generic.List<Identifier>(),
                Name = new System.Collections.Generic.List<HumanName>(),
                Gender = new AdministrativeGender?(AdministrativeGender.Unknown),
                Extension = new List<Extension>()
            };

            _patient1.Identifier.Add(new Identifier("CPR", "201120001518"));
            HumanName patientName = new HumanName()
            {
                Family = "Testson",
                Text = "Test Testson"
            };
            _patient1.Name.Add(patientName);
            
            //Extension etaextension = new Extension("http://www.example.com/datetimeTest", DateTime.MinValue);
            _patient1.Extension.Add();

            _patientModel1 = new PatientModel()
            {
                ETA = DateTime.MinValue,
                Name = "Test Testson",
                Triage = "Unknown",
                Specialty = "Unknown",
                Gender = AdministrativeGender.Unknown,
                ToHospital = "To hospital went wrong"
            };

        }


        #endregion

        #region Act and Assert
        // Method_Scenario_Result
       
        [Test]
        public void ReturnPatient_ReceivedCompletePatient_CreatesPatient()
        {
            PatientModel patient = _uut.ReturnPatient(_patient1);
            Assert.That(patient, Is.EqualTo(_patientModel1));
        }

        #endregion






    }
}
