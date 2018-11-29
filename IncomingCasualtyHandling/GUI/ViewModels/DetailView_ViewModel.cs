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
        public string IconPath => _detailViewModel.IconPath;

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


        public ObservableCollection<TabControl> Tabs
        {
            get => _detailViewModel.ObservableCollectionTabs;
        }
        


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
            // Cast the source of the control that triggered the event
            var source = (System.Windows.Controls.GridViewColumnHeader) o.Source;
            // Find the name of the column
            var columnName = source.Content;
            // Use the column name as argument to the method
            _detailViewModel.GridViewColumnHeaderClicked(columnName.ToString());
            
                
        }
    }
}
