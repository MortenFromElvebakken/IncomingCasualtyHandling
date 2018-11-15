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
    internal class MainView_ViewModel : ObservableObject
    {
        private string _test;
        private List<PatientModel> _listOfPatients;

        OverviewView_Model _overviewModel;

        // Objects for the two subviews
        OverviewView_ViewModel _overviewViewViewModel; 
        DetailView_ViewModel _detailViewViewModel; 

        // Model for ViewModel
        private MainView_Model _mainModel;

        public MainView_ViewModel()
        {

        }

        public MainView_ViewModel(MainView_Model mainModel, OverviewView_ViewModel overviewViewViewModel, DetailView_ViewModel detailViewViewModel)
        {
            
            _mainModel = mainModel;
            _overviewViewViewModel = overviewViewViewModel;
            _detailViewViewModel = detailViewViewModel;
            
            _mainModel.PropertyChanged += MainModelOnPropertyChanged;

            //Setup the current workspace aka the view to be shown
            CurrentWorkspace = _overviewViewViewModel;
            ChangeViewCommand = new RelayCommand(ChangeView);
            ChangeViewCommandWithProperty = new RelayCommand<object>(ChangeViewWithParameter);
        }

        

        private void MainModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
            OnPropertyChanged(e.PropertyName);
        }

        

        // Property for databinding in MainView
        // This property decides the current view/viewmodel
        private Workspace_ViewModel _currentWorkspace;
        public Workspace_ViewModel CurrentWorkspace
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

        //Test for new command with command-binding to change view depending on property specified in command
        public ICommand ChangeViewCommandWithProperty { get; set; }
        private void ChangeViewWithParameter(object x)
        {

            //string[] parameters = x.ToString().Split(' ');
            //Nu har vi splittet strengen fra parametret, ind i den første ie. "Triage" og den anden "1"
            //_detailViewViewModel.SelectedIndex = parameters[1];
            _detailViewViewModel.StringFromParameter = x.ToString();
            CurrentWorkspace = _detailViewViewModel;
            


            //Denne metode skal kaldes med property på, alt afhængig af hvilken komponent der trykkes på


            //Lav index på tabs, så den kan starte på den rigtige tab også.
            //Bind selected index på tabcontrol til denne værdi
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
