using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using Hl7.Fhir.Model;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.GUI.ViewModels
{
    public class DetailViewViewModel : WorkspaceViewModel
    {
        List<Triage> _listOfTriages = new List<Triage>();

        private List<Triage> ListOfTriages
        {
            get => _detailViewModel.ListOfTriages;
            set
            {
                _listOfTriages = value;
                OnPropertyChanged();
            }
        }

        //List<Specialty> _listOfSpecialty = new List<Specialty>();

        //private ObservableCollection<PatientModel> _triage1Patients = new ObservableCollection<PatientModel>();
        //private ObservableCollection<Patient> _triage2Patients = new ObservableCollection<Patient>();
        //private ObservableCollection<Patient> _triage3Patients = new ObservableCollection<Patient>();
        //private ObservableCollection<Patient> _triage4Patients = new ObservableCollection<Patient>();
        //private ObservableCollection<Patient> _triage5Patients = new ObservableCollection<Patient>();

        //private ObservableCollection<Patient> _specialty1Patients = new ObservableCollection<Patient>();
        //private ObservableCollection<Patient> _specialty2Patients = new ObservableCollection<Patient>();
        //private ObservableCollection<Patient> _specialty3Patients = new ObservableCollection<Patient>();
        //private ObservableCollection<Patient> _specialty4Patients = new ObservableCollection<Patient>();
        //private ObservableCollection<Patient> _specialty5Patients = new ObservableCollection<Patient>();
        //private ObservableCollection<Patient> _specialty6Patients = new ObservableCollection<Patient>();
        //private ObservableCollection<Patient> _specialty7Patients = new ObservableCollection<Patient>();
        //private ObservableCollection<Patient> _specialty8Patients = new ObservableCollection<Patient>();
        //private ObservableCollection<Patient> _specialty9Patients = new ObservableCollection<Patient>();
        //private ObservableCollection<Patient> _specialty10Patients = new ObservableCollection<Patient>();
        //private ObservableCollection<Patient> _specialty11Patients = new ObservableCollection<Patient>();
        //private ObservableCollection<Patient> _specialty12Patients = new ObservableCollection<Patient>();
        //private ObservableCollection<Patient> _specialty13Patients = new ObservableCollection<Patient>();
        //private ObservableCollection<Patient> _specialty14Patients = new ObservableCollection<Patient>();
        //private ObservableCollection<Patient> _specialty15Patients = new ObservableCollection<Patient>();
        //private ObservableCollection<Patient> _specialty16Patients = new ObservableCollection<Patient>();

        //private ObservableCollection<Patient> _etaPatients = new ObservableCollection<Patient>();


        //This property is specified, based on which usercontrol is pressed in the mainview.
        //This means that it is now possible to create tabs based on this

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _detailViewModel.SelectedTabIndex;
            set => _selectedIndex = value;
        }

        public string StringFromParameter
        {
            get => _detailViewModel.testForStringFromClickOnModel;
            set => _detailViewModel.testForStringFromClickOnModel = value;
        }

        private List<Tabs> _tabs;

        public List<Tabs> Tabs
        {
            get => _detailViewModel.ListOfTabs;
            set => _detailViewModel.ListOfTabs = value;
        }


        private DetailViewModel _detailViewModel;
        public DetailViewViewModel()
        { }

        public DetailViewViewModel(DetailViewModel detailViewModel)
        {
            _detailViewModel = detailViewModel;
            _detailViewModel.PropertyChanged += DetailViewModelOnPropertyChanged;
            
           
        }

        private void DetailViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }
    }
}
