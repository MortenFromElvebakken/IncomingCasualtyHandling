using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
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
        public int SelectedTabIndex
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


        public ObservableCollection<TabControl> Tabs2
        {
            get => _detailViewModel.ObservableCollectionTabs;
        }

        //List of tabs in window, set upon pressing something in overview view
        public List<TabControl> Tabs
        {
            get => _detailViewModel.ListOfTabs;
            set => _detailViewModel.ListOfTabs = value;
        }
        public TabControl test { get; set; }


        private IDetailView_Model _detailViewModel;
        public DetailView_ViewModel()
        { }

        public DetailView_ViewModel(IDetailView_Model detailViewModel)
        {
            _detailViewModel = detailViewModel;
            _detailViewModel.PropertyChanged += DetailViewModelOnPropertyChanged;
            ChangeTabsCommand = new RelayCommand<object>(ChangeTabs);
            GridViewColumnHeaderClickedHandlerCommand = new RelayCommand<RoutedEventArgs>(GridViewColumnHeaderClickedHandler);
            //SourceUpdatedTab = new RelayCommand(sourceUpdatedTabSet);


        }

        private void DetailViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }
        public ICommand ChangeTabsCommand { get; set; }
        public void ChangeTabs(object x)
        {
            _detailViewModel.ChangeTabsAllowed(x.ToString());
        }
        public bool ChangedFromMain {set => _detailViewModel.ChangedFromMain = true; }


        public ICommand GridViewColumnHeaderClickedHandlerCommand { get; set; }

        private void GridViewColumnHeaderClickedHandler(RoutedEventArgs o)
        {
            var source = (System.Windows.Controls.GridViewColumnHeader) o.Source;
            var columnName = source.Content;
            _detailViewModel.GridViewColumnHeaderClicked(columnName.ToString());
            

            MessageBox.Show("You clicked");
                
        }

        //public ICommand SourceUpdatedTab { get; set; }

        //private void sourceUpdatedTabSet()
        //{
        //    _detailViewModel.SetTabIndex();
        //}
    }
}
