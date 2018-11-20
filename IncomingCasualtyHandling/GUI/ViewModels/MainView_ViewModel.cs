using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using IncomingCasualtyHandling.BL;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL;
using IncomingCasualtyHandling.GUI.View;

namespace IncomingCasualtyHandling.GUI.ViewModels
{
    // the class is an ObservableObject in order to be able to call OnPropertyChanged on properties
    internal class MainView_ViewModel : ObservableObject
    {
        private string _test;
        private List<PatientModel> _listOfPatients;

        IOverviewView_Model _overviewModel;

        // Objects for the two subviews
        OverviewView_ViewModel _overviewViewViewModel; 
        DetailView_ViewModel _detailViewViewModel; 

        // Model for ViewModel
        private IMainView_Model _mainModel;

        public MainView_ViewModel()
        {

        }

        public MainView_ViewModel(IMainView_Model mainModel, OverviewView_ViewModel overviewViewViewModel, DetailView_ViewModel detailViewViewModel)
        {
            
            _mainModel = mainModel;
            _overviewViewViewModel = overviewViewViewModel;
            _detailViewViewModel = detailViewViewModel;
            
            _mainModel.PropertyChanged += MainModelOnPropertyChanged;

            //Setup the current workspace aka the view to be shown
            CurrentWorkspace = _overviewViewViewModel;
            ChangeViewCommand = new RelayCommand(ChangeView);
            ChangeViewCommandWithProperty = new RelayCommand<object>(ChangeViewWithParameter);
            ChangeServerName = new RelayCommand(ChangeServer);
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

            if(CurrentWorkspace == _overviewViewViewModel)
            {
                
                _detailViewViewModel.StringFromParameter = x.ToString();
                CurrentWorkspace = _detailViewViewModel;
            }
            else
            {
                //En eller anden logic, der gør at den reloader tabs
                _detailViewViewModel.StringFromParameter = x.ToString();
                //var test = _detailViewViewModel.Tabs;
            }
        }
        public ICommand ChangeServerName { get; set; }

        public void ChangeServer()
        {
            ServerChangeWindow serverChangeWindow = new ServerChangeWindow();
            if (serverChangeWindow.ShowDialog() ==true)
            {
                
                //Sæt servernavn i mainmodel, lav en metode i set på denne property, der sætter servernavn
                //i dal, og hvis denne er sat, skal fhirclient.endpoint være lig denne, og getallpatients kaldes
                //igen
                //serverChangeWindow.ServerName
                //Set servername in 
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

        public Triage Triage1 => _mainModel.Triage1;

        public Triage Triage2 => _mainModel.Triage2;

        public Triage Triage3 => _mainModel.Triage3;

        public Triage Triage4 => _mainModel.Triage4;

        public Triage Triage5 => _mainModel.Triage5;

        public Triage Triage6 => _mainModel.Triage6;

        public Specialty Specialty1 => _mainModel.Specialty1;

        public ETA Eta => _mainModel.Eta;
    }
}
