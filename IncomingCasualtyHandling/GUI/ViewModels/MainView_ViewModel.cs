using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using IncomingCasualtyHandling.BL;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL;
using IncomingCasualtyHandling.GUI.View;
// **********************************
// Group: 2018E73
// Anni Lykke Wilhelmsen, 201509504
// Morten From Elvebakken, 201509095
// **********************************
namespace IncomingCasualtyHandling.GUI.ViewModels
{
    // the class is an ObservableObject in order to be able to call OnPropertyChanged on properties
    internal class MainView_ViewModel : ObservableObject
    {
        

        IOverviewView_Model _overviewModel;

        // Objects for the two subviews
        OverviewView_ViewModel _overviewView_ViewModel; 
        DetailView_ViewModel _detailView_ViewModel; 

        // Model for ViewModel
        private IMainView_Model _mainModel;

        public MainView_ViewModel()
        {

        }

        public MainView_ViewModel(IMainView_Model mainModel, OverviewView_ViewModel overviewViewViewModel, DetailView_ViewModel detailViewViewModel)
        {
            
            _mainModel = mainModel;
            _overviewView_ViewModel = overviewViewViewModel;
            _detailView_ViewModel = detailViewViewModel;
            
            _mainModel.PropertyChanged += MainModelOnPropertyChanged;

            //Setup the current workspace aka the view to be shown
            CurrentWorkspace = _overviewView_ViewModel;
            ReturnToMainCommand = new RelayCommand(ReturnToMain);
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

        // Command for command-binding to change view
        public ICommand ReturnToMainCommand { get; set; }

        public void ReturnToMain()
        {
                CurrentWorkspace = _overviewView_ViewModel;
        }

        //Test for new command with command-binding to change view depending on property specified in command
        public ICommand ChangeViewCommandWithProperty { get; set; }
        private void ChangeViewWithParameter(object x)
        {
            if(CurrentWorkspace == _overviewView_ViewModel)
            {
                _detailView_ViewModel.ChangedFromMain = true;
                _detailView_ViewModel.ChangeTabs(x);
                CurrentWorkspace = _detailView_ViewModel;
            }
            else
            {
                _detailView_ViewModel.ChangeTabs(x);
            }
        }
        public ICommand ChangeServerName { get; set; }
        //Changes servername if valid server input, and sets view to overview
        public void ChangeServer()
        {
            ServerChangeWindow serverChangeWindow = new ServerChangeWindow();
            if (serverChangeWindow.ShowDialog() ==true)
            {
                if (serverChangeWindow.ServerName.Text != "")
                {
                    _mainModel.ServerName = serverChangeWindow.ServerName.Text;
                    if (CurrentWorkspace != _detailView_ViewModel)
                    {
                        CurrentWorkspace = _overviewView_ViewModel;
                    }
                }
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

        public Visibility ConnectionToInternet => _mainModel.ConnectionToInternet;

        public string NoConnectionString => _mainModel.NoConnectionString;


        private List<ICHPatient> _listOfPatients;
        public List<ICHPatient> ListOfPatients
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

        public ETA ETA => _mainModel.ETA;
    }
}
