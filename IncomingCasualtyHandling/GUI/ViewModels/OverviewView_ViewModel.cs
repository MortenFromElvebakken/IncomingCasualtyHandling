using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;
// **********************************
// Group: 2018E73
// Anni Lykke Wilhelmsen, 201509504
// Morten From Elvebakken, 201509095
// **********************************
namespace IncomingCasualtyHandling.GUI.ViewModels
{
    public class OverviewView_ViewModel : Workspace_ViewModel
    {
        #region Triages

        public int MaximumTriagePatients => _overview_Model.MaximumTriagePatients;

        #endregion


        #region Specialties

        //private Specialty[] listOfSpecialities = new Specialty[16];
        //private List<Specialty> specialties = new List<Specialty>();

        public Specialty Specialty1 => _overview_Model.Specialty1;

        public Specialty Specialty2 => _overview_Model.Specialty2;

        public Specialty Specialty3 => _overview_Model.Specialty3;

        public Specialty Specialty4 => _overview_Model.Specialty4;

        public Specialty Specialty5 => _overview_Model.Specialty5;

        public Specialty Specialty6 => _overview_Model.Specialty6;

        public Specialty Specialty7 => _overview_Model.Specialty7;

        public Specialty Specialty8 => _overview_Model.Specialty8;

        public Specialty Specialty9 => _overview_Model.Specialty9;

        public Specialty Specialty10 => _overview_Model.Specialty10;

        public Specialty Specialty11 => _overview_Model.Specialty11;

        public Specialty Specialty12 => _overview_Model.Specialty12;

        public Specialty Specialty13 => _overview_Model.Specialty13;

        public Specialty Specialty14 => _overview_Model.Specialty14;

        public Specialty Specialty15 => _overview_Model.Specialty15;

        public Specialty Specialty16 => _overview_Model.Specialty16;

        #endregion


        #region ETA

        public ETA ETA => _overview_Model.ETA;

        #endregion


        #region Constructor
        // Set Model for ViewModel
        private IOverviewView_Model _overview_Model;

        public OverviewView_ViewModel()
        {

        }
        public OverviewView_ViewModel(IOverviewView_Model overviewview_Model)
        {
            _overview_Model = overviewview_Model;
            _overview_Model.PropertyChanged += OverviewModelOnPropertyChanged;
            
        }

        private void OverviewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        #endregion
    }
}
