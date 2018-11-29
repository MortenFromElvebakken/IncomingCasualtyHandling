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
        public string IconPath => _detailView_Model.IconPath;

        //To specify which tab is opened, and which is currently open
        public int SelectedTabIndex
        {
            get => _detailView_Model.SelectedTabIndex;
            set => _detailView_Model.SelectedTabIndex = value;

        }
        //To get which button is pressed from the overviewview, in order to open correct tab
        public string StringFromParameter
        {
            get => _detailView_Model.StringFromChangeViewCommandParameter;
            set => _detailView_Model.StringFromChangeViewCommandParameter = value;
        }


        public ObservableCollection<TabControl> Tabs
        {
            get => _detailView_Model.ObservableCollectionTabs;
        }
        


        private IDetailView_Model _detailView_Model;
        public DetailView_ViewModel()
        { }

        public DetailView_ViewModel(IDetailView_Model detailView_Model)
        {
            _detailView_Model = detailView_Model;
            _detailView_Model.PropertyChanged += DetailViewModelOnPropertyChanged;
            ChangeTabsCommand = new RelayCommand<object>(ChangeTabs);
            GridViewColumnHeaderClickedHandlerCommand = new RelayCommand<RoutedEventArgs>(GridViewColumnHeaderClickedHandler);
            
        }

        private void DetailViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }


        public ICommand ChangeTabsCommand { get; set; }
        public void ChangeTabs(object x)
        {
            _detailView_Model.ChangeTabsAllowed(x.ToString());
        }
        public bool ChangedFromMain {set => _detailView_Model.ChangedFromMain = true; }


        public ICommand GridViewColumnHeaderClickedHandlerCommand { get; set; }
        private void GridViewColumnHeaderClickedHandler(RoutedEventArgs o)
        {
            // Cast the source of the control that triggered the event
            var source = (System.Windows.Controls.GridViewColumnHeader) o.Source;
            // Find the name of the column
            var columnName = source.Content;
            // Use the column name as argument to the method
            _detailView_Model.GridViewColumnHeaderClicked(columnName.ToString());
            
                
        }
    }
}
