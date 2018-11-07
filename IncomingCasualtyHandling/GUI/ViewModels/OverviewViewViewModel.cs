using System;
using System.Collections.Generic;
using System.Windows;
using IncomingCasualtyHandling.BL.Models;

namespace IncomingCasualtyHandling.GUI.ViewModels
{
    public class OverviewViewViewModel : WorkspaceViewModel
    {
        // Set Model for ViewModel
        OverviewViewModel overviewModel = new OverviewViewModel();

        #region Triages

        public int MaximumTriages => overviewModel.MaximumTriages;

        //private Triage _triage1;
        //private Triage _triage2;
        //private Triage _triage3;
        //private Triage _triage4;
        //private Triage _triage5;

        public Triage Triage1 => overviewModel.Triage1;

        public Triage Triage2 => overviewModel.Triage2;

        public Triage Triage3 => overviewModel.Triage3;

        public Triage Triage4 => overviewModel.Triage4;

        public Triage Triage5 => overviewModel.Triage5;

        #endregion


        #region Specialties

        //private Specialty[] listOfSpecialities = new Specialty[16];
        //private List<Specialty> specialties = new List<Specialty>();

        public Specialty Specialty1 => overviewModel.Specialty1;

        public Specialty Specialty2 => overviewModel.Specialty2;

        public Specialty Specialty3 => overviewModel.Specialty3;

        public Specialty Specialty4 => overviewModel.Specialty4;

        public Specialty Specialty5 => overviewModel.Specialty5;

        public Specialty Specialty6 => overviewModel.Specialty6;

        public Specialty Specialty7 => overviewModel.Specialty7;

        public Specialty Specialty8 => overviewModel.Specialty8;

        public Specialty Specialty9 => overviewModel.Specialty9;

        public Specialty Specialty10 => overviewModel.Specialty10;

        public Specialty Specialty11 => overviewModel.Specialty11;

        public Specialty Specialty12 => overviewModel.Specialty12;

        public Specialty Specialty13 => overviewModel.Specialty13;

        public Specialty Specialty14 => overviewModel.Specialty14;

        public Specialty Specialty15 => overviewModel.Specialty15;

        public Specialty Specialty16 => overviewModel.Specialty16;

        #endregion


        #region ETA

        private ETA _eta;

        public ETA Eta
        {
            get => _eta;
            set
            {
                _eta = value;
                OnPropertyChanged("Eta");
            }
        }

        #endregion


        #region Constructor
        public OverviewViewViewModel()
        {
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

        #endregion
    }
}
