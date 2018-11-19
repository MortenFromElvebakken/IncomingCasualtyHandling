﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Hl7.Fhir.Model;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;
using TabControl = IncomingCasualtyHandling.BL.Models.TabControl;

namespace IncomingCasualtyHandling.GUI.ViewModels
{
    public class DetailView_ViewModel : Workspace_ViewModel
    {
        //List<Triage> _listOfTriages = new List<Triage>();

        //private List<Triage> ListOfTriages
        //{
        //    get => _detailViewModel.ListOfTriages;
        //    set => _listOfTriages = value;
        //}


        //Opret  properties til brug af overviewcomponent

        //antal af triagerede i hver gruppe
        //Antal speciale i øverste speciale
        //Næste ETA
        
        //for homeicon
        public string IconPath
        {
            get => _detailViewModel.IconPath;
        }
        //To specify which tab is opened, and which is currently open
        public int SelectedIndex
        {
            get => _detailViewModel.SelectedTabIndex;
            set => _detailViewModel.SelectedTabIndex = value;

        }
        //To get which button is pressed from the overviewview, in order to open correct tab
        public string StringFromParameter
        {
            get => _detailViewModel.StringFromChangeViewCommandParameter;
            set => _detailViewModel.StringFromChangeViewCommandParameter = value;
        }


        //to set the counter of patients in bottom right corner
        public string PatientsInList
        {
            get => _detailViewModel.PatientsInList;
            set => _detailViewModel.PatientsInList = value; // På dem vi aldrig bruger en setter på, skal der så overhovedet laves en?
            //Herunder hvis der kommer en patient mere i den liste man sidder på, hvordan opdateres patientsinlist så?
        }

        //List of tabs in window, set upon pressing something in overview view
        public List<TabControl> Tabs
        {
            get => _detailViewModel.ListOfTabs;
            set => _detailViewModel.ListOfTabs = value;
        }


        private IDetailView_Model _detailViewModel;
        public DetailView_ViewModel()
        { }

        public DetailView_ViewModel(IDetailView_Model detailViewModel)
        {
            _detailViewModel = detailViewModel;
            _detailViewModel.PropertyChanged += DetailViewModelOnPropertyChanged;
            ChangeTabsCommand = new RelayCommand<object>(ChangeTabs);
            
           
        }

        private void DetailViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }
        public ICommand ChangeTabsCommand { get; set; }
        private void ChangeTabs(object x)
        {

            StringFromParameter = x.ToString();
            OnPropertyChanged("Tabs");
            OnPropertyChanged("SelectedIndex");

        }
    }
}