using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.GUI.ViewModels
{
    public class OverviewView_ViewModel : Workspace_ViewModel
    {

        // Set Model for ViewModel
        OverviewView_Model _overviewModel;

        #region Triages

        public int MaximumTriages => _overviewModel.MaximumTriages;

        public Triage Triage1 => _overviewModel.Triage1;

        public Triage Triage2 => _overviewModel.Triage2;

        public Triage Triage3 => _overviewModel.Triage3;

        public Triage Triage4 => _overviewModel.Triage4;

        public Triage Triage5 => _overviewModel.Triage5;

        #endregion


        #region Specialties

        //private Specialty[] listOfSpecialities = new Specialty[16];
        //private List<Specialty> specialties = new List<Specialty>();

        public Specialty Specialty1 => _overviewModel.Specialty1;

        public Specialty Specialty2 => _overviewModel.Specialty2;

        public Specialty Specialty3 => _overviewModel.Specialty3;

        public Specialty Specialty4 => _overviewModel.Specialty4;

        public Specialty Specialty5 => _overviewModel.Specialty5;

        public Specialty Specialty6 => _overviewModel.Specialty6;

        public Specialty Specialty7 => _overviewModel.Specialty7;

        public Specialty Specialty8 => _overviewModel.Specialty8;

        public Specialty Specialty9 => _overviewModel.Specialty9;

        public Specialty Specialty10 => _overviewModel.Specialty10;

        public Specialty Specialty11 => _overviewModel.Specialty11;

        public Specialty Specialty12 => _overviewModel.Specialty12;

        public Specialty Specialty13 => _overviewModel.Specialty13;

        public Specialty Specialty14 => _overviewModel.Specialty14;

        public Specialty Specialty15 => _overviewModel.Specialty15;

        public Specialty Specialty16 => _overviewModel.Specialty16;

        #endregion


        #region ETA

        public ETA Eta => _overviewModel.Eta;

        #endregion


        #region Constructor

        public OverviewView_ViewModel()
        {

        }
        public OverviewView_ViewModel(OverviewView_Model overviewViewModel)
        {
            _overviewModel = overviewViewModel;
            _overviewModel.PropertyChanged += OverviewModelOnPropertyChanged;
            //specialties.Add(new Specialty
            //{
            //    Name = "Orthopaedic",
            //    Colour = "#af3205",
            //    Amount = 5,
            //    ShowAs = Visibility.Visible
            //});
            //specialties.Add(new Specialty
            //{
            //    Name = "Medicinal",
            //    Colour = "#9400D3",
            //    Amount = 9,
            //    ShowAs = Visibility.Visible
            //});
            //specialties.Add(new Specialty
            //{
            //    Name = "Thoracic surgery",
            //    Colour = "#003865",
            //    Amount = 4,
            //    ShowAs = Visibility.Visible
            //});

            //var counter = 0;
            //Array.Clear(listOfSpecialities, 0, listOfSpecialities.Length);
            //specialties.Sort((x, y) => y.Amount.CompareTo(x.Amount));
            //foreach (var specialty in specialties)
            //{
            //    if (specialty != null)
            //    {
            //        listOfSpecialities[counter] = specialty;
            //        counter++;
            //    }
            //}

            //_triage1 = new Triage
            //{
            //    Amount = 8,
            //    Colour = "#f60e0e",
            //    Name = "Red",
            //    ShowAs = Visibility.Visible
            //};

            //_triage2 = new Triage
            //{
            //    Amount = 0,
            //    Colour = "#f28d0e",
            //    Name = "Orange",
            //    ShowAs = Visibility.Collapsed
            //};

            //_triage3 = new Triage
            //{
            //    Amount = 5,
            //    Colour = "#ffe913",
            //    Name = "Yellow",
            //    ShowAs = Visibility.Visible
            //};
            //_triage4 = new Triage
            //{
            //    Amount = 2,
            //    Colour = "#0bdd2e",
            //    Name = "Green",
            //    ShowAs = Visibility.Visible
            //};
            //_triage5 = new Triage
            //{
            //    Amount = 0,
            //    Colour = "#1e38ff",
            //    Name = "Blue",
            //    ShowAs = Visibility.Collapsed
            //};

            //_eta = new ETA
            //{
            //    AbsoluteTime = "10:42",
            //    RelativeTime = "(-08:00)"
            //};
        }

        private void OverviewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        #endregion
    }
}
