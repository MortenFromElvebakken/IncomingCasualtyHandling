using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL;
using IncomingCasualtyHandling.DAL.Interface;
using NUnit.Framework;

namespace IncomingCasualtyHandling.Test.Unit.DAL.Test.Unit
{
    [TestFixture]
    public class LoadConfigurationSettingsTest
    {
        #region Arrange

        private ILoadConfigurationSettings _uut;

        private string _xmlDocumentPath;
        private string _xmlServerName = "http://localhost:8080/hapi-fhir-jpaserver-example/baseDstu3";
        private string _xmlHospitalShortName = "AUH";
        private List<Triage> _xmlTriageList;
        private List<Specialty> _xmlSpecialtyList;

        [SetUp]
        public void Setup()
        {
            _xmlTriageList = new List<Triage>
            {
                new Triage {Name = "TriageRed", Colour = "#FF0000", Amount = 0, ShowAs = Visibility.Collapsed},
                new Triage {Name = "TriageOrange", Colour = "#FFA500", Amount = 0, ShowAs = Visibility.Collapsed},
                new Triage {Name = "TriageYellow", Colour = "#FFFF00", Amount = 0, ShowAs = Visibility.Collapsed},
                new Triage {Name = "TriageGreen", Colour = "#008000", Amount = 0, ShowAs = Visibility.Collapsed},
                new Triage {Name = "TriageBlue", Colour = "#0000FF", Amount = 0, ShowAs = Visibility.Collapsed}
            };

            _xmlSpecialtyList = new List<Specialty>
            {
                new Specialty
                {
                    Name = "Diagnostic radiology",
                    Colour = "#007F7F",
                    Amount = 0,
                    ShowAs = Visibility.Collapsed
                },
                new Specialty
                {
                    Name = "Emergency medicine",
                    Colour = "#975EC8",
                    Amount = 0,
                    ShowAs = Visibility.Collapsed
                },
                new Specialty
                {
                    Name = "Internal medicine",
                    Colour = "#B8BC34",
                    Amount = 0,
                    ShowAs = Visibility.Collapsed
                },
                new Specialty {Name = "Neurology", Colour = "#47B72C", Amount = 0, ShowAs = Visibility.Collapsed},
                new Specialty
                {
                    Name = "Obstetrics and Gynecology",
                    Colour = "#FFB27F",
                    Amount = 0,
                    ShowAs = Visibility.Collapsed
                },
                new Specialty {Name = "Opthalmology", Colour = "#FF99E3", Amount = 0, ShowAs = Visibility.Collapsed},
                new Specialty
                {
                    Name = "Orthopaedic surgery",
                    Colour = "#BB1282",
                    Amount = 0,
                    ShowAs = Visibility.Collapsed
                },
                new Specialty {Name = "Otolaryngology", Colour = "#8200BF", Amount = 0, ShowAs = Visibility.Collapsed},
                
                new Specialty {Name = "Pediatrics", Colour = "#A3CC92", Amount = 0, ShowAs = Visibility.Collapsed},
                new Specialty {Name = "Plastic surgery", Colour = "#00137F", Amount = 0, ShowAs = Visibility.Collapsed},
                new Specialty {Name = "Psychiatry", Colour = "#0026D1", Amount = 0, ShowAs = Visibility.Collapsed},
                new Specialty {Name = "Surgery-general", Colour = "#2C9914", Amount = 0, ShowAs = Visibility.Collapsed},
                new Specialty {Name = "Thoracic sugery", Colour = "#EA7C00", Amount = 0, ShowAs = Visibility.Collapsed},
                new Specialty {Name = "Unknown", Colour = "#cecece", Amount = 0, ShowAs = Visibility.Collapsed},
                new Specialty {Name = "Urology", Colour = "#CCCE6B", Amount = 0, ShowAs = Visibility.Collapsed},
                new Specialty {Name = "Vascular surgery", Colour = "#DA1B21", Amount = 0, ShowAs = Visibility.Collapsed}
            };

            var currentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(
                TestContext.CurrentContext.TestDirectory));
            _xmlDocumentPath = currentDirectory + "\\Configuration.xml";
            _uut = new LoadConfigurationSettings(_xmlDocumentPath);
            
            
        }


        #endregion

        #region Act and Assert
        // Method_Scenario_Result

        //Get the server name correct
        [Test]
        public void LoadConfigurationSettingsConstructor_GetServerNameCalled_GotCorrectServerName()
        {
            Assert.That(_uut.ServerName, Is.EqualTo(_xmlServerName));
        }

        //Get the hospital name correct
        [Test]
        public void LoadConfigurationSettingsConstructor_GetHospitalShortNameCalled_GotCorrectHospitalShortName()
        {
            Assert.That(_uut.HospitalShortName, Is.EqualTo(_xmlHospitalShortName));
        }

        //Get the triages correct
        [Test]
        public void ReturnTriageListCalled_GetTriagesFromXML_GotCorrectAmountOfTriages()
        {
            List<Triage> triageList = _uut.ReturnTriageList();
            Assert.That(triageList.Count, Is.EqualTo(_xmlTriageList.Count));
        }

        //Get the triages correct
        [Test]
        public void ReturnTriageListCalled_GetTriagesFromXML_GotCorrectTriageName()
        {
            List<Triage> triageList = _uut.ReturnTriageList();
            Assert.That(triageList[0].Name, Is.EqualTo(_xmlTriageList[0].Name));
        }

        //Get the triages correct
        [Test]
        public void ReturnTriageListCalled_GetTriagesFromXML_GotCorrectTriageColour()
        {
            List<Triage> triageList = _uut.ReturnTriageList();
            Assert.That(triageList[1].Colour, Is.EqualTo(_xmlTriageList[1].Colour));
        }

        //Get the triages correct
        [Test]
        public void ReturnTriageListCalled_GetTriagesFromXML_HaveSetTriageAmountTo0()
        {
            List<Triage> triageList = _uut.ReturnTriageList();
            Assert.That(triageList[2].Amount, Is.EqualTo(0));
        }

        //Get the triages correct
        [Test]
        public void ReturnTriageListCalled_GetTriagesFromXML_HaveSetTriageVisibilityToCollapsed()
        {
            List<Triage> triageList = _uut.ReturnTriageList();
            Assert.That(triageList[3].ShowAs, Is.EqualTo(Visibility.Collapsed));
        }

        //Get the specialty correct
        [Test]
        public void ReturnSpecialtyListCalled_GetSpecialtiesFromXML_GotCorrectAmountOfSpecialties()
        {
            List<Specialty> specialtyList = _uut.ReturnSpecialtyList();
            Assert.That(specialtyList.Count, Is.EqualTo(_xmlSpecialtyList.Count));
        }

        //Get the specialty correct
        [Test]
        public void ReturnSpecialtyListCalled_GetSpecialtiesFromXML_GotCorrectSpecialtyName()
        {
            List<Specialty> specialtyList = _uut.ReturnSpecialtyList();
            Assert.That(specialtyList[0].Name, Is.EqualTo(_xmlSpecialtyList[0].Name));
        }

        //Get the specialty correct
        [Test]
        public void ReturnSpecialtyListCalled_GetSpecialtiesFromXML_HaveSetSpecialtyColourCorrect()
        {
            List<Specialty> specialtyList = _uut.ReturnSpecialtyList();
            Assert.That(specialtyList[4].Colour, Is.EqualTo(_xmlSpecialtyList[4].Colour));
        }

        //Get the specialty correct
        [Test]
        public void ReturnSpecialtyListCalled_GetSpecialtiesFromXML_HaveSetSpecialtyAmountTo0()
        {
            List<Specialty> specialtyList = _uut.ReturnSpecialtyList();
            Assert.That(specialtyList[8].Amount, Is.EqualTo(0));
        }

        //Get the specialty correct
        [Test]
        public void ReturnSpecialtyListCalled_GetSpecialtiesFromXML_HaveSetSpecialtyVisibilityToCollapsed()
        {
            List<Specialty> specialtyList = _uut.ReturnSpecialtyList();
            Assert.That(specialtyList[12].ShowAs, Is.EqualTo(Visibility.Collapsed));
        }
        #endregion
    }
}
