using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using IncomingCasualtyHandling.BL;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL;

namespace IncomingCasualtyHandling.GUI.ViewModels
{
    // the class is an ObservableObject in order to be able to call OnPropertyChanged on properties
    internal class MainViewViewModel : ObservableObject
    {
        private string _test;
        private List<PatientModel> _listOfPatients;

        OverviewViewModel _overviewModel;

        // Objects for the two subviews
        OverviewViewViewModel _overviewViewViewModel; 
        DetailViewViewModel _detailViewViewModel; 

        // Model for ViewModel
        private MainViewModel _mainModel;

        public MainViewViewModel()
        {

        }

        public MainViewViewModel(MainViewModel mainModel, OverviewViewViewModel overviewViewViewModel, DetailViewViewModel detailViewViewModel)
        {
            
            _mainModel = mainModel;
            _overviewViewViewModel = overviewViewViewModel;
            _detailViewViewModel = detailViewViewModel;
            
            _mainModel.PropertyChanged += MainModelOnPropertyChanged;

            //Setup the current workspace aka the view to be shown
            CurrentWorkspace = _overviewViewViewModel;
            ChangeViewCommand = new RelayCommand(ChangeView);
            
        }

        private void MainModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
            OnPropertyChanged(e.PropertyName);
        }

        

        // Property for databinding in MainView
        // This property decides the current view/viewmodel
        private WorkspaceViewModel _currentWorkspace;
        public WorkspaceViewModel CurrentWorkspace
        {
            get => _currentWorkspace;
            set
            {
                if (_currentWorkspace != value)
                {
                    _currentWorkspace = value;
                    OnPropertyChanged();
                }
            }

        }

        // Command for command-binding to change view
        public ICommand ChangeViewCommand { get; set; }

        public void ChangeView()
        {
            if (CurrentWorkspace == _overviewViewViewModel)
            {
                CurrentWorkspace = _detailViewViewModel;
            }
            else
            {
                CurrentWorkspace = _overviewViewViewModel;
            }

        }

        // Property for binding for TopComponent Timer
        public string CurrentDateTime
        {
            get
            {
                return _mainModel.CurrentDateTime;
            }
        }


        public string TestProperty
        {
            get { return _test; }
            set
            {
                if (value == _test) return;
                _test = value;
            }
        }
        public List<PatientModel> ListOfPatients
        {
            get { return _listOfPatients; }
            set
            {
                if (value == _listOfPatients) return;
                _listOfPatients = value;
            }
        }
    }
}
