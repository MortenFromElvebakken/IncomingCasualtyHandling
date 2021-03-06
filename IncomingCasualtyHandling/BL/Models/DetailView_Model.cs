﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Object_classes;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
// **********************************
// Group: 2018E73
// Anni Lykke Wilhelmsen, 201509504
// Morten From Elvebakken, 201509095
// **********************************

namespace IncomingCasualtyHandling.BL.Models
{
    public class DetailView_Model : ObservableObject, IDetailView_Model
    {

        public string IconPath => "/GUI/Icons/HomeIcon.png";

        #region Triages

        private List<Triage> _listOfTriages;

        public List<Triage> ListOfTriages
        {
            get => _listOfTriages;
            set
            {
                _listOfTriages = value;
                OnPropertyChanged("ListOfTriages");

            }
        }

        private List<List<ICHPatient>> _listOfTriagePatientLists;

        public List<List<ICHPatient>> ListOfTriagePatientLists
        {
            get => _listOfTriagePatientLists;
            set
            {
                _listOfTriagePatientLists = value;
                int counter = 1;
                foreach (var patientList in _listOfTriagePatientLists)
                {
                    string propertyName = "Triage" + counter + "Patients";
                    OnPropertyChanged(propertyName);
                    counter++;
                }

                if (SelectedOverview == "Triage")
                {
                    CreateTabs();
                }
            }
        }


        public List<ICHPatient> Triage1Patients
        {
            get => ListOfTriagePatientLists[0];
            set => ListOfTriagePatientLists[0] = value;
        }

        public List<ICHPatient> Triage2Patients
        {
            get => ListOfTriagePatientLists[1];
            set => ListOfTriagePatientLists[1] = value;
        }

        public List<ICHPatient> Triage3Patients
        {
            get => ListOfTriagePatientLists[2];
            set => ListOfTriagePatientLists[2] = value;
        }

        public List<ICHPatient> Triage4Patients
        {
            get => ListOfTriagePatientLists[3];
            set => ListOfTriagePatientLists[3] = value;
        }

        public List<ICHPatient> Triage5Patients
        {
            get => ListOfTriagePatientLists[4];
            set => ListOfTriagePatientLists[4] = value;
        }

        public List<ICHPatient> Triage6Patients
        {
            get => ListOfTriagePatientLists[5];
            set => ListOfTriagePatientLists[5] = value;
        }

        #endregion

        #region Specialties

        private List<Specialty> _listOfSpecialties;
        public List<Specialty> ListOfSpecialties
        {
            get => _listOfSpecialties;
            set
            {
                _listOfSpecialties = value;
                OnPropertyChanged("ListOfSpecialties");

            }
        }

        private List<List<ICHPatient>> _listOfSpecialtiesPatientLists;
        public List<List<ICHPatient>> ListOfSpecialtiesPatientLists
        {
            get => _listOfSpecialtiesPatientLists;
            set
            {
                _listOfSpecialtiesPatientLists = value;
                int counter = 1;
                foreach (var patientList in _listOfSpecialtiesPatientLists)
                {
                    string propertyName = "Specialty" + counter + "Patients";
                    //Hvis listerne i denne liste er ordnet efter hvor mange der kommer, hvad så hvis der ændres på nummer 2, så den nu har 5
                    //og derfor rykkes et andet sted hen, er det så hele rækkefølgen der opdateres?
                    OnPropertyChanged(propertyName);
                    counter++;
                }

                if (SelectedOverview == "Specialty")
                {
                 CreateTabs();   
                }
            }
        }

        #endregion

        #region SpecialtiesInList

        public List<ICHPatient> Specialty1Patients
        {
            get => ListOfSpecialtiesPatientLists[0];
            set => ListOfSpecialtiesPatientLists[0] = value;
        }

        public List<ICHPatient> Specialty2Patients
        {
            get => ListOfSpecialtiesPatientLists[1];
            set => ListOfSpecialtiesPatientLists[1] = value;
        }

        public List<ICHPatient> Specialty3Patients
        {
            get => ListOfSpecialtiesPatientLists[2];
            set => ListOfSpecialtiesPatientLists[2] = value;
        }

        public List<ICHPatient> Specialty4Patients
        {
            get => ListOfSpecialtiesPatientLists[3];
            set => ListOfSpecialtiesPatientLists[3] = value;
        }

        public List<ICHPatient> Specialty5Patients
        {
            get => ListOfSpecialtiesPatientLists[4];
            set => ListOfSpecialtiesPatientLists[4] = value;
        }

        public List<ICHPatient> Specialty6Patients
        {
            get => ListOfSpecialtiesPatientLists[5];
            set => ListOfSpecialtiesPatientLists[5] = value;
        }

        public List<ICHPatient> Specialty7Patients
        {
            get => ListOfSpecialtiesPatientLists[6];
            set => ListOfSpecialtiesPatientLists[6] = value;
        }

        public List<ICHPatient> Specialty8Patients
        {
            get => ListOfSpecialtiesPatientLists[7];
            set => ListOfSpecialtiesPatientLists[7] = value;
        }

        public List<ICHPatient> Specialty9Patients
        {
            get => ListOfSpecialtiesPatientLists[8];
            set => ListOfSpecialtiesPatientLists[8] = value;
        }

        public List<ICHPatient> Specialty10Patients
        {
            get => ListOfSpecialtiesPatientLists[9];
            set => ListOfSpecialtiesPatientLists[9] = value;
        }

        public List<ICHPatient> Specialty11Patients
        {
            get => ListOfSpecialtiesPatientLists[10];
            set => ListOfSpecialtiesPatientLists[10] = value;
        }

        public List<ICHPatient> Specialty12Patients
        {
            get => ListOfSpecialtiesPatientLists[11];
            set => ListOfSpecialtiesPatientLists[11] = value;
        }

        public List<ICHPatient> Specialty13Patients
        {
            get => ListOfSpecialtiesPatientLists[12];
            set => ListOfSpecialtiesPatientLists[12] = value;
        }

        public List<ICHPatient> Specialty14Patients
        {
            get => ListOfSpecialtiesPatientLists[13];
            set => ListOfSpecialtiesPatientLists[13] = value;
        }

        public List<ICHPatient> Specialty15Patients
        {
            get => ListOfSpecialtiesPatientLists[14];
            set => ListOfSpecialtiesPatientLists[14] = value;
        }

        public List<ICHPatient> Specialty16Patients
        {
            get => ListOfSpecialtiesPatientLists[15];
            set => ListOfSpecialtiesPatientLists[15] = value;
        }

        #endregion

        #region ETA

        private List<ICHPatient> _ETAPatients;
        public List<ICHPatient> ETAPatients
        {
            get => _ETAPatients;
            set
            {
                _ETAPatients = value;
                OnPropertyChanged("ETAPatients");
                if (SelectedOverview == "ETA")
                {
                    CreateTabs();
                }
            }
        }

        #endregion

        #region TabItems
        
        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set
            {
                if (value >= 0)
                {
                    _selectedTabIndex = value;
                    OldValue = value;
                    OnPropertyChanged();
                    OnPropertyChanged("Data");
                }
                else
                {
                    OldValue = _selectedTabIndex;
                    _selectedTabIndex = value;
                    Thread testThread = new Thread(SetTabIndexAgain);
                    testThread.Start();
                }
            }
        }
        private int OldValue { get; set; }
        public void SetTabIndexAgain()
        {
            Thread.Sleep(50);
            SelectedTabIndex = OldValue;
            
        }
        

        public string SelectedOverview { get; set; }

        private string _stringFromCommandParameter = "";
        public string StringFromChangeViewCommandParameter
        {
            get => _stringFromCommandParameter;
            set
            {
                _stringFromCommandParameter = value;
                OnPropertyChanged("StringFromChangeViewCommandParameter");
            }
        }
        

        private ObservableCollection<TabControl> _observableCollectionTabs;

        public ObservableCollection<TabControl> ObservableCollectionTabs
        {
            get => _observableCollectionTabs;
            set
            {
                _observableCollectionTabs = value;
                OnPropertyChanged("Tabs");
                OnPropertyChanged("SelectedTabIndex");
                OnPropertyChanged("Data");
            }
        }

        public void CreateTabs()
        {
            List<TabControl> _tempTabList = new List<TabControl>();

            //The parameters sent from the change view/tab command is split, so it can be used to
            //determine which tabs are to be made. 
            string[] parameters = StringFromChangeViewCommandParameter.Split(' ');
            SelectedOverview = parameters[0];
            

            switch (SelectedOverview)
            {
                case "Triage":
                    {
                        int counter = 0;
                        foreach (var triage in ListOfTriages)
                        {
                            if (triage.Amount != 0)
                            {
                                var _tab = new TabControl()
                                {
                                    Name = triage.Name,
                                    Data = new ObservableCollection<ICHPatient>(ListOfTriagePatientLists.Find(item => item[0].Triage.Name == triage.Name)),
                                    isVisible = Visibility.Visible
                                };
                                _tempTabList.Add(_tab);
                            }
                            else
                            {
                                var _tab = new TabControl()
                                {
                                    Name = triage.Name,
                                    isVisible = Visibility.Collapsed
                                };
                                _tempTabList.Add(_tab);
                            }
                            counter++;
                        }

                        //ObservableCollectionTabs = new ObservableCollection<TabControl>(_tempTabList);
                        //ListOfTabs = _tempTabList;
                        break;
                    }
                case "Specialty":
                    {
                        int counter = 0;
                        foreach (var specialty in ListOfSpecialties)
                        {
                            if (specialty.Amount != 0)
                            {
                                var _tab = new TabControl()
                                {
                                    Name = specialty.Name,
                                    Data = new ObservableCollection<ICHPatient>(ListOfSpecialtiesPatientLists.Find(item => item[0].Specialty == specialty.Name)),
                                    isVisible = Visibility.Visible
                                };
                                _tempTabList.Add(_tab);
                            }
                            else
                            {
                                var _tab = new TabControl()
                                {
                                    Name = specialty.Name,
                                    isVisible = Visibility.Collapsed
                                };
                                _tempTabList.Add(_tab);
                            }
                            counter++;
                        }
                        // Sort the list alphabetically
                        //ObservableCollectionTabs = new ObservableCollection<TabControl>(_tempTabList.OrderBy(x => x.Name).ToList());
                        
                        _tempTabList = _tempTabList.OrderBy(a => a.Name).ToList();
                        var test = _tempTabList;
                        //ObservableCollectionTabs = test;
                        //ListOfTabs = _tempTabList.OrderBy(t => t.Name).ToList();
                        break;
                    }
                default:
                {
                    var _tab = new TabControl()
                    {
                        Name = "ETA",
                        Data = new ObservableCollection<ICHPatient>(ETAPatients),
                        isVisible = Visibility.Visible
                    };
                    _tempTabList.Add(_tab);
                    //ObservableCollectionTabs = new ObservableCollection<TabControl>(_tempTabList);
                    //ListOfTabs = _tempTabList;
                    break;
                }

            }

            //Checks if tabname of selected index has changed, if it has it sets find the new index of the tab
            //If the tab is now not visible, it finds the first ocurrence of a visible tab and sets this as 
            //the new index in the end. Only checks if it has been run before
            

            int newTabIndex = SelectedTabIndex;
            if (ChangesFromUser == "No")
            {

                var oldTabName = ObservableCollectionTabs[SelectedTabIndex].Name;
                if (oldTabName == _tempTabList[SelectedTabIndex].Name)
                {
                    if (_tempTabList[SelectedTabIndex].isVisible != Visibility.Visible) //oldTabName !=
                    {
                        var findIndex = _tempTabList.FindIndex(a => a.Name == oldTabName);
                        if (_tempTabList[findIndex].isVisible == Visibility.Visible)
                        {
                            newTabIndex = findIndex;
                        }
                        else
                        {
                            newTabIndex = _tempTabList.FindIndex(a => a.isVisible == Visibility.Visible);
                        }
                    }
                }

                else
                {
                    var findIndex = _tempTabList.FindIndex(a => a.Name == oldTabName);
                    if (_tempTabList[findIndex].isVisible != Visibility.Visible) //oldTabName !=
                    {
                            newTabIndex = _tempTabList.FindIndex(a => a.isVisible == Visibility.Visible);
                    }
                    else
                    {
                        newTabIndex = findIndex;
                    }
                        
                }

                
            }

            if (_sortColumn != "ETA" | _sortDirection != ListSortDirection.Ascending)
            {
                //If _sortColumn wasnt ETA and _sortDirection wasnt ascending then it needs to sort the new data based on
                //last sorted specification. 
                if (_sortDirection != ListSortDirection.Ascending)
                {
                    _sortDirection = ListSortDirection.Ascending;
                }
                else
                {
                    _sortDirection = ListSortDirection.Descending;
                }
                ObservableCollectionTabs = new ObservableCollection<TabControl>(_tempTabList);
                GridViewColumnHeaderClicked(_sortColumn);
            }
            else
            {
                ObservableCollectionTabs = new ObservableCollection<TabControl>(_tempTabList);
            }

            if (newTabIndex != SelectedTabIndex)
            {
                SelectedTabIndex = newTabIndex;
            }

            
        }
        public bool ChangedFromMain { get; set; }
        public string ChangesFromUser = "";

        public void ChangeTabsAllowed(string s)
        {
            string[] parameters = s.Split(' ');
            var tryChangeTabs = parameters[0];
            var tryTabIndex = Convert.ToInt16(parameters[1]);
            //If the user is in detailview, and the pressed item is in the current tabs
            if (tryChangeTabs == SelectedOverview && !ChangedFromMain)
            {
                
                if (tryChangeTabs == "Specialty")
                {
                    var chosenSpecialtyName = ListOfSpecialties[tryTabIndex].Name;
                    var sortedSpecialtiesList = new List<Specialty>();
                    sortedSpecialtiesList = ListOfSpecialties.OrderBy(a => a.Name).ToList();
                    var newIndex = sortedSpecialtiesList.FindIndex(x => x.Name == chosenSpecialtyName);
                    if (_observableCollectionTabs[newIndex].isVisible == Visibility.Visible)
                    {
                        SelectedTabIndex = newIndex;
                    }
                    
                }
                else
                {
                    if (_observableCollectionTabs[tryTabIndex].isVisible == Visibility.Visible)    //_tabsList[tryTabIndex].isVisible == Visibility.Visible)
                    {
                        SelectedTabIndex = tryTabIndex;
                    }
                }
            }
            //If the user is in detail view and clicks something that needs new tabs
            else if (tryChangeTabs != SelectedOverview && !ChangedFromMain)
            {

                switch (tryChangeTabs)
                {
                    case "Triage":
                        if (ListOfTriages[tryTabIndex].ShowAs == Visibility.Visible)
                        {
                            _sortDirection = ListSortDirection.Ascending;
                            _sortColumn = "ETA";
                            ChangesFromUser = "Yes";
                            StringFromChangeViewCommandParameter = s;
                            CreateTabs();
                            SelectedTabIndex = tryTabIndex;
                            ChangesFromUser = "No";
                        }
                        break;
                    case "Specialty":
                            var chosenSpecialtyName = ListOfSpecialties[tryTabIndex].Name;
                            var sortedSpecialtiesList = new List<Specialty>();
                            sortedSpecialtiesList = ListOfSpecialties.OrderBy(a => a.Name).ToList();
                            var newIndex = sortedSpecialtiesList.FindIndex(x => x.Name == chosenSpecialtyName);
                            StringFromChangeViewCommandParameter = string.Format("Specialty " + newIndex);
                        if (sortedSpecialtiesList[newIndex].ShowAs == Visibility.Visible)
                        {
                            ChangesFromUser = "Yes";
                            CreateTabs();
                            
                            ChangesFromUser = "No";
                            SelectedTabIndex = newIndex;
                        }
                        
                        break;
                    default:
                        if (ETAPatients.Count > 0)
                        {
                            ChangesFromUser = "Yes";
                            StringFromChangeViewCommandParameter = s;
                            CreateTabs();
                            SelectedTabIndex = tryTabIndex;
                            ChangesFromUser = "No";
                        }
                        break;
                }
            }
            else
            {
                _sortDirection = ListSortDirection.Ascending;
                _sortColumn = "ETA";
                ChangesFromUser = "Yes";
                switch (tryChangeTabs)
                {
                        
                    case "Triage":
                        StringFromChangeViewCommandParameter = s;
                        CreateTabs();
                        SelectedTabIndex = tryTabIndex;
                        break;
                    case "Specialty": 
                        //If specialty is the case, it needs another index than the one sent from view, this is due to
                        //the alfabetic sort on tabs. 
                        var chosenSpecialtyName = ListOfSpecialties[tryTabIndex].Name;
                        var sortedSpecialtiesList = new List<Specialty>();
                        sortedSpecialtiesList = ListOfSpecialties.OrderBy(a => a.Name).ToList();
                        var newIndex = sortedSpecialtiesList.FindIndex(x => x.Name == chosenSpecialtyName);
                        StringFromChangeViewCommandParameter = string.Format("Specialty " + newIndex);
                        CreateTabs();
                        SelectedTabIndex = newIndex;
                        break;
                    default:
                        StringFromChangeViewCommandParameter = s;
                        CreateTabs();
                        SelectedTabIndex = tryTabIndex;
                        break;
                }

                ChangedFromMain = false;
                ChangesFromUser = "No";
            }

        }
        #endregion

        #region Sorting list

        // Made with inspiration from:
        // https://code.msdn.microsoft.com/windowsdesktop/Sorting-a-WPF-ListView-by-209a7d45?fbclid=IwAR0ZrZ0Ee4PDB0Z7TJmLKE55mN2b-GcRY_mho1NtjdALdM3w8vYpKRo0hko

        private string _sortColumn = "ETA";
        private ListSortDirection _sortDirection = ListSortDirection.Ascending;
        

        public void GridViewColumnHeaderClicked(string s)
        {
            // Check whether the header is the same as the one already sorting on
            if (_sortColumn == s)
            {
                // If it is the same header, then the sorting direction should be changed
                _sortDirection = _sortDirection == ListSortDirection.Ascending
                    ? ListSortDirection.Descending
                    : ListSortDirection.Ascending;
            }
            else
            {
                // If it's not the same header, then the sorting direction should be ascending
                _sortColumn = s;
                _sortDirection = ListSortDirection.Ascending;
            }

            // Check what to sort based upon
            switch (_sortColumn)
            {
                case "Name":
                    {
                        // Take each tab and sort it's data
                        foreach (var tab in _observableCollectionTabs)
                        {
                            if (tab.Data != null)
                            {
                                var listOfPatients = new List<ICHPatient>(tab.Data.ToList());
                                listOfPatients.Sort((p1, p2) => String.Compare(p1.Name, p2.Name, StringComparison.CurrentCulture));
                                // Check the sorting direction
                                if (_sortDirection == ListSortDirection.Descending)
                                {
                                    // The sort-method by default sorts ascending. Thus the sorted data must be reversed
                                    listOfPatients.Reverse();
                                    tab.Data = new ObservableCollection<ICHPatient>(listOfPatients);
                                }
                                else
                                {
                                    tab.Data = new ObservableCollection<ICHPatient>(listOfPatients);
                                }
                            }
                        }
                        break;
                    }
                case "CPR":
                    {
                        foreach (var tab in _observableCollectionTabs)
                        {
                            if (tab.Data != null)
                            {
                                var listOfPatients = new List<ICHPatient>(tab.Data.ToList());
                                listOfPatients.Sort((p1, p2) => String.Compare(p1.CPR, p2.CPR, StringComparison.CurrentCulture));
                                if (_sortDirection == ListSortDirection.Descending)
                                {
                                    listOfPatients.Reverse();
                                    tab.Data = new ObservableCollection<ICHPatient>(listOfPatients);
                                }
                                else
                                {
                                    tab.Data = new ObservableCollection<ICHPatient>(listOfPatients);
                                }
                            }
                        }
                        break;
                    }
                case "Age":
                    {
                        foreach (var tab in _observableCollectionTabs)
                        {
                            if (tab.Data != null)
                            {
                                var listOfPatients = new List<ICHPatient>(tab.Data.ToList());
                                //listOfPatients.Sort((p1, p2) => p1.Age.CompareTo(p2.Age));

                                listOfPatients.Sort((p1, p2) => int.Parse(p1.Age).CompareTo(int.Parse(p2.Age))); //New sort on age, since age is string

                                if (_sortDirection == ListSortDirection.Descending)
                                {
                                    listOfPatients.Reverse();
                                    tab.Data = new ObservableCollection<ICHPatient>(listOfPatients);
                                }
                                else
                                {
                                    tab.Data = new ObservableCollection<ICHPatient>(listOfPatients);
                                }
                            }
                        }
                        break;
                    }
                case "Gender":
                    {
                        foreach (var tab in _observableCollectionTabs)
                        {
                            if (tab.Data != null)
                            {
                                var listOfPatients = new List<ICHPatient>(tab.Data.ToList());
                                listOfPatients.Sort((p1, p2) => String.Compare(p1.Gender.ToString(), p2.Gender.ToString(), StringComparison.CurrentCulture));
                                if (_sortDirection == ListSortDirection.Descending)
                                {
                                    listOfPatients.Reverse();
                                    tab.Data = new ObservableCollection<ICHPatient>(listOfPatients);
                                }
                                else
                                {
                                    tab.Data = new ObservableCollection<ICHPatient>(listOfPatients);
                                }
                            }
                        }
                        break;
                    }
                case "Triage":
                    {
                        foreach (var tab in _observableCollectionTabs)
                        {
                            if (tab.Data != null)
                            {
                                var listOfPatients = new List<ICHPatient>(tab.Data.ToList());
                                listOfPatients.Sort((p1, p2) => String.Compare(p1.Triage.Name, p2.Triage.Name, StringComparison.CurrentCulture));
                                if (_sortDirection == ListSortDirection.Descending)
                                {
                                    listOfPatients.Reverse();
                                    tab.Data = new ObservableCollection<ICHPatient>(listOfPatients);
                                }
                                else
                                {
                                    tab.Data = new ObservableCollection<ICHPatient>(listOfPatients);
                                }
                            }
                        }
                        break;
                    }
                case "Specialty":
                    {
                        foreach (var tab in _observableCollectionTabs)
                        {
                            if (tab.Data != null)
                            {
                                var listOfPatients = new List<ICHPatient>(tab.Data.ToList());
                                listOfPatients.Sort((p1, p2) => String.Compare(p1.Specialty, p2.Specialty, StringComparison.CurrentCulture));
                                if (_sortDirection == ListSortDirection.Descending)
                                {
                                    listOfPatients.Reverse();
                                    tab.Data = new ObservableCollection<ICHPatient>(listOfPatients);
                                }
                                else
                                {
                                    tab.Data = new ObservableCollection<ICHPatient>(listOfPatients);
                                }
                            }
                        }
                        break;
                    }
                case "ETA":
                    {
                        foreach (var tab in _observableCollectionTabs)
                        {
                            if (tab.Data != null)
                            {
                                var listOfPatients = new List<ICHPatient>(tab.Data.ToList());
                                listOfPatients.Sort((p1, p2) => p1.ETA.CompareTo(p2.ETA));
                                if (_sortDirection == ListSortDirection.Descending)
                                {
                                    listOfPatients.Reverse();
                                    tab.Data = new ObservableCollection<ICHPatient>(listOfPatients);
                                }
                                else
                                {
                                    tab.Data = new ObservableCollection<ICHPatient>(listOfPatients);
                                }
                            }
                        }
                        break;
                    }
                case "From destination":
                    {
                        foreach (var tab in _observableCollectionTabs)
                        {
                            //tab.Data?.Sort((p1, p2) => String.Compare(p1.FromDestination, p2.FromDestination, StringComparison.CurrentCulture));
                            if (tab.Data != null)
                            {
                                var listOfPatients = new List<ICHPatient>(tab.Data.ToList());
                                listOfPatients.Sort((p1, p2) => String.Compare(p1.FromDestination, p2.FromDestination, StringComparison.CurrentCulture));
                                if (_sortDirection == ListSortDirection.Descending)
                                {
                                    listOfPatients.Reverse();
                                    tab.Data = new ObservableCollection<ICHPatient>(listOfPatients);
                                }
                                else
                                {
                                    tab.Data = new ObservableCollection<ICHPatient>(listOfPatients);
                                }
                            }
                        }
                        break;
                    }
                case "Last updated":
                    {
                        foreach (var tab in _observableCollectionTabs)
                        {
                            if (tab.Data != null)
                            {
                                var listOfPatients = new List<ICHPatient>(tab.Data.ToList());
                                listOfPatients.Sort((p1, p2) => p1.LastUpdated.CompareTo(p2.LastUpdated));
                                if (_sortDirection == ListSortDirection.Descending)
                                {
                                    listOfPatients.Reverse();
                                    tab.Data = new ObservableCollection<ICHPatient>(listOfPatients);
                                }
                                else
                                {
                                    tab.Data = new ObservableCollection<ICHPatient>(listOfPatients);
                                }
                            }
                        }
                        break;
                    }
            }
           // OnPropertyChanged("Data");
        }
        

        #endregion
    }
}