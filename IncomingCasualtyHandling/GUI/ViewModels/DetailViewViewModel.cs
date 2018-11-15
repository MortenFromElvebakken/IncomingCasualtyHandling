using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using Hl7.Fhir.Model;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;
using TabControl = IncomingCasualtyHandling.BL.Models.TabControl;

namespace IncomingCasualtyHandling.GUI.ViewModels
{
    public class DetailViewViewModel : WorkspaceViewModel
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
            get => _detailViewModel.iconPath;
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
            get => _detailViewModel.testForStringFromClickOnModel;
            set => _detailViewModel.testForStringFromClickOnModel = value;
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
