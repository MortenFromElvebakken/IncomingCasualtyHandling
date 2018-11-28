using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.GUI.ViewModels
{
    public class OverviewView_ViewModel : Workspace_ViewModel
    {
    #region Triages

        public int MaximumTriagePatients => _overviewModel.MaximumTriagePatients;

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

        public ETA ETA => _overviewModel.ETA;

        #endregion


        #region Constructor
        // Set Model for ViewModel
        IOverviewView_Model _overviewModel;

        public OverviewView_ViewModel()
        {

        }
        public OverviewView_ViewModel(IOverviewView_Model overviewViewModel)
        {
            _overviewModel = overviewViewModel;
            _overviewModel.PropertyChanged += OverviewModelOnPropertyChanged;
            
        }

        private void OverviewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        #endregion
    }
}
