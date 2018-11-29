using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Hl7.Fhir.Model;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;
using NUnit.Framework;

namespace IncomingCasualtyHandling.Test.Unit.BL.Test.Unit
{
    [TestFixture]
    class DetailView_ModelTest
    {
        #region Arrange

        private DetailView_Model _uut;

        ObservableCollection<TabControl> _tabs;
        private TabControl _tab1;
        private ObservableCollection<ICHPatient> _listOfPatients;
        private ObservableCollection<ICHPatient> _sortedListOfPatients;
        private ICHPatient _patient1, _patient2, _patient3;

        [SetUp]
        public void SetUp()
        {
            _uut = new DetailView_Model();

            // Create a list with patients
            _listOfPatients = new ObservableCollection<ICHPatient>();
            _sortedListOfPatients = new ObservableCollection<ICHPatient>();
            _patient1 = new ICHPatient

            {
                CPR = "1010101010",
                Name = "Peter Poulsen",
                Age = "50",
                Gender = AdministrativeGender.Male,
                Triage = "TriageRed",
                Specialty = "Neurology",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 22, 30, 00),
                FromDestination = "Accident",
                LastUpdated = new DateTimeOffset(2018,11,18,17,30,00, new TimeSpan(0,1,0,0))
            };
            _patient2 = new ICHPatient
            {
                CPR = "2020202020",
                Name = "Arne Jacobsen",
                Age = "20",
                Gender = AdministrativeGender.Female,
                Triage = "TriageGreen",
                Specialty = "Psychology",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 21, 30, 00),
                FromDestination = "Home",
                LastUpdated = new DateTimeOffset(2018, 11, 18, 18, 30, 00, new TimeSpan(0, 1, 0, 0))
            };
            _patient3 = new ICHPatient
            {
                CPR = "3030303030",
                Name = "Marcus Olson",
                Age = "60",
                Gender = AdministrativeGender.Other,
                Triage = "TriageYellow",
                Specialty = "Emergency Medicine",
                ToHospital = "AUH",
                ETA = new DateTime(2018, 11, 18, 18, 00, 00),
                FromDestination = "Other hospital",
                LastUpdated = new DateTimeOffset(2018, 11, 18, 08, 30, 00, new TimeSpan(0, 1, 0, 0))
            };

            _listOfPatients.Add(_patient1);
            _listOfPatients.Add(_patient2);
            _listOfPatients.Add(_patient3);

            _tabs = new ObservableCollection<TabControl>();
            _tab1 = new TabControl {Data = _listOfPatients, Name = "Tab1", isVisible = Visibility.Visible};
            _tabs.Add(_tab1);

            _uut.ObservableCollectionTabs = _tabs;
        }
        #endregion

        #region Act and Assert

        #region Name
        [Test]
        public void GridViewColunmHeaderClicked_NameHeaderClickedOnce_ListSortedByNameAscending()
        {
            string headerClicked = "Name";
            _uut.GridViewColumnHeaderClicked(headerClicked);

            _sortedListOfPatients.Add(_patient2); //Arne
            _sortedListOfPatients.Add(_patient3); //Marcus
            _sortedListOfPatients.Add(_patient1); //Peter

            Assert.That(_uut.ObservableCollectionTabs[0].Data, Is.EqualTo(_sortedListOfPatients));

        }

        [Test]
        public void GridViewColunmHeaderClicked_NameHeaderClickedTwice_ListSortedByNameDescending()
        {
            string headerClicked = "Name";
            _uut.GridViewColumnHeaderClicked(headerClicked);
            _uut.GridViewColumnHeaderClicked(headerClicked);

            _sortedListOfPatients.Add(_patient1); //Peter
            _sortedListOfPatients.Add(_patient3); //Marcus
            _sortedListOfPatients.Add(_patient2); //Arne

            Assert.That(_uut.ObservableCollectionTabs[0].Data, Is.EqualTo(_sortedListOfPatients));

        }
        #endregion

        #region CPR
        [Test]
        public void GridViewColunmHeaderClicked_CPRHeaderClickedOnce_ListSortedByCPRAscending()
        {
            string headerClicked = "CPR";
            _uut.GridViewColumnHeaderClicked(headerClicked);

            _sortedListOfPatients.Add(_patient1); //1010101010
            _sortedListOfPatients.Add(_patient2); //2020202020
            _sortedListOfPatients.Add(_patient3); //3030303030

            Assert.That(_uut.ObservableCollectionTabs[0].Data, Is.EqualTo(_sortedListOfPatients));

        }

        [Test]
        public void GridViewColunmHeaderClicked_CPRHeaderClickedTwice_ListSortedByCPRDescending()
        {
            string headerClicked = "CPR";
            _uut.GridViewColumnHeaderClicked(headerClicked);
            _uut.GridViewColumnHeaderClicked(headerClicked);

            _sortedListOfPatients.Add(_patient3); //3030303030
            _sortedListOfPatients.Add(_patient2); //2020202020
            _sortedListOfPatients.Add(_patient1); //1010101010

            Assert.That(_uut.ObservableCollectionTabs[0].Data, Is.EqualTo(_sortedListOfPatients));

        }
        #endregion

        #region Age
        [Test]
        public void GridViewColunmHeaderClicked_AgeHeaderClickedOnce_ListSortedByAgeAscending()
        {
            string headerClicked = "Age";
            _uut.GridViewColumnHeaderClicked(headerClicked);

            _sortedListOfPatients.Add(_patient2); //20
            _sortedListOfPatients.Add(_patient1); //50
            _sortedListOfPatients.Add(_patient3); //60

            Assert.That(_uut.ObservableCollectionTabs[0].Data, Is.EqualTo(_sortedListOfPatients));

        }

        [Test]
        public void GridViewColunmHeaderClicked_AgeHeaderClickedTwice_ListSortedByAgeDescending()
        {
            string headerClicked = "Age";
            _uut.GridViewColumnHeaderClicked(headerClicked);
            _uut.GridViewColumnHeaderClicked(headerClicked);

            _sortedListOfPatients.Add(_patient3); //60
            _sortedListOfPatients.Add(_patient1); //50
            _sortedListOfPatients.Add(_patient2); //20


            Assert.That(_uut.ObservableCollectionTabs[0].Data, Is.EqualTo(_sortedListOfPatients));

        }
        #endregion

        #region Gender
        [Test]
        public void GridViewColunmHeaderClicked_GenderHeaderClickedOnce_ListSortedByGenderAscending()
        {
            string headerClicked = "Gender";
            _uut.GridViewColumnHeaderClicked(headerClicked);

            _sortedListOfPatients.Add(_patient2); //Female
            _sortedListOfPatients.Add(_patient1); //Male
            _sortedListOfPatients.Add(_patient3); //Other

            Assert.That(_uut.ObservableCollectionTabs[0].Data, Is.EqualTo(_sortedListOfPatients));

        }

        [Test]
        public void GridViewColunmHeaderClicked_GenderHeaderClickedTwice_ListSortedByGenderDescending()
        {
            string headerClicked = "Gender";
            _uut.GridViewColumnHeaderClicked(headerClicked);
            _uut.GridViewColumnHeaderClicked(headerClicked);

            _sortedListOfPatients.Add(_patient3); //Other
            _sortedListOfPatients.Add(_patient1); //Male
            _sortedListOfPatients.Add(_patient2); //Female
            Assert.That(_uut.ObservableCollectionTabs[0].Data, Is.EqualTo(_sortedListOfPatients));

        }
        #endregion

        #region Triage
        [Test]
        public void GridViewColunmHeaderClicked_TriageHeaderClickedOnce_ListSortedByTriageAscending()
        {
            string headerClicked = "Triage";
            _uut.GridViewColumnHeaderClicked(headerClicked);

            _sortedListOfPatients.Add(_patient2); //Green
            _sortedListOfPatients.Add(_patient1); //Red
            _sortedListOfPatients.Add(_patient3); //Yellow

            Assert.That(_uut.ObservableCollectionTabs[0].Data, Is.EqualTo(_sortedListOfPatients));

        }

        [Test]
        public void GridViewColunmHeaderClicked_TriageHeaderClickedTwice_ListSortedByTriageDescending()
        {
            string headerClicked = "Triage";
            _uut.GridViewColumnHeaderClicked(headerClicked);
            _uut.GridViewColumnHeaderClicked(headerClicked);

            _sortedListOfPatients.Add(_patient3); //Yellow
            _sortedListOfPatients.Add(_patient1); //Red
            _sortedListOfPatients.Add(_patient2); //Green
            Assert.That(_uut.ObservableCollectionTabs[0].Data, Is.EqualTo(_sortedListOfPatients));

        }
        #endregion

        #region Specialty

        [Test]
        public void GridViewColunmHeaderClicked_SpecialtyHeaderClickedOnce_ListSortedBySpecialtyAscending()
        {
            string headerClicked = "Specialty";
            _uut.GridViewColumnHeaderClicked(headerClicked);

            _sortedListOfPatients.Add(_patient3); //Emergency medicine
            _sortedListOfPatients.Add(_patient1); //Neurology
            _sortedListOfPatients.Add(_patient2); //Psychology

            Assert.That(_uut.ObservableCollectionTabs[0].Data, Is.EqualTo(_sortedListOfPatients));

        }

        [Test]
        public void GridViewColunmHeaderClicked_SpecialtyHeaderClickedTwice_ListSortedBySpecialtyDescending()
        {
            string headerClicked = "Specialty";
            _uut.GridViewColumnHeaderClicked(headerClicked);
            _uut.GridViewColumnHeaderClicked(headerClicked);

            _sortedListOfPatients.Add(_patient2); //Psychology
            _sortedListOfPatients.Add(_patient1); //Neurology
            _sortedListOfPatients.Add(_patient3); //Emergency medicine
            
            Assert.That(_uut.ObservableCollectionTabs[0].Data, Is.EqualTo(_sortedListOfPatients));

        }
        #endregion

        #region ETA

        [Test]
        public void GridViewColunmHeaderClicked_ETAHeaderClickedOnce_ListSortedByETADescending()
        {
            string headerClicked = "ETA";
            _uut.GridViewColumnHeaderClicked(headerClicked);

            _sortedListOfPatients.Add(_patient1); //22:30
            _sortedListOfPatients.Add(_patient2); //21:30
            _sortedListOfPatients.Add(_patient3); //18:00

            Assert.That(_uut.ObservableCollectionTabs[0].Data, Is.EqualTo(_sortedListOfPatients));

        }

        [Test]
        public void GridViewColunmHeaderClicked_ETAHeaderClickedTwice_ListSortedByETAAscending()
        {
            string headerClicked = "ETA";
            _uut.GridViewColumnHeaderClicked(headerClicked);
            _uut.GridViewColumnHeaderClicked(headerClicked);

            _sortedListOfPatients.Add(_patient3); //18:00
            _sortedListOfPatients.Add(_patient2); //21:30
            _sortedListOfPatients.Add(_patient1); //22:30

            Assert.That(_uut.ObservableCollectionTabs[0].Data, Is.EqualTo(_sortedListOfPatients));

        }
        #endregion

        #region From destination

        [Test]
        public void GridViewColunmHeaderClicked_FromDestinationHeaderClickedOnce_ListSortedByFromDestinationAscending()
        {
            string headerClicked = "From destination";
            _uut.GridViewColumnHeaderClicked(headerClicked);

            _sortedListOfPatients.Add(_patient1); //Accident
            _sortedListOfPatients.Add(_patient2); //Home
            _sortedListOfPatients.Add(_patient3); //Other hospital

            Assert.That(_uut.ObservableCollectionTabs[0].Data, Is.EqualTo(_sortedListOfPatients));

        }

        [Test]
        public void GridViewColunmHeaderClicked_FromDestinationHeaderClickedTwice_ListSortedByFromDestinationDescending()
        {
            string headerClicked = "From destination";
            _uut.GridViewColumnHeaderClicked(headerClicked);
            _uut.GridViewColumnHeaderClicked(headerClicked);

            _sortedListOfPatients.Add(_patient3); //Other hospital
            _sortedListOfPatients.Add(_patient2); //Home
            _sortedListOfPatients.Add(_patient1); //Accident

            Assert.That(_uut.ObservableCollectionTabs[0].Data, Is.EqualTo(_sortedListOfPatients));

        }
        #endregion

        #region Last updated

        [Test]
        public void GridViewColunmHeaderClicked_LastUpdatedHeaderClickedOnce_ListSortedByLastUpdatedAscending()
        {
            string headerClicked = "Last updated";
            _uut.GridViewColumnHeaderClicked(headerClicked);

            _sortedListOfPatients.Add(_patient3); //08:30
            _sortedListOfPatients.Add(_patient1); //17:30
            _sortedListOfPatients.Add(_patient2); //18:30

            Assert.That(_uut.ObservableCollectionTabs[0].Data, Is.EqualTo(_sortedListOfPatients));

        }

        [Test]
        public void GridViewColunmHeaderClicked_LastUpdatedHeaderClickedTwice_ListSortedByLastUpdatedDescending()
        {
            string headerClicked = "Last updated";
            _uut.GridViewColumnHeaderClicked(headerClicked);
            _uut.GridViewColumnHeaderClicked(headerClicked);

            _sortedListOfPatients.Add(_patient2); //18:30
            _sortedListOfPatients.Add(_patient1); //17:30
            _sortedListOfPatients.Add(_patient3); //08:30

            Assert.That(_uut.ObservableCollectionTabs[0].Data, Is.EqualTo(_sortedListOfPatients));

        }
        #endregion


        #endregion
    }

}
