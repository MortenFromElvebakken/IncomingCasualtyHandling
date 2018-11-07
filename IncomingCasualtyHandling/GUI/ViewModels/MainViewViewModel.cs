using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Input;
using IncomingCasualtyHandling.BL;
using IncomingCasualtyHandling.DAL;
using OurPatient = IncomingCasualtyHandling.BL.Models.OurPatient;

namespace IncomingCasualtyHandling.GUI.ViewModels
{
    // the class is an ObservableObject in order to be able to call OnPropertyChanged on properties
    internal class MainViewViewModel : ObservableObject
    {
        private string _test;
        private List<OurPatient> _listOfPatients;

        // Objects for the two subviews
        OverviewViewViewModel _overviewViewViewModel = new OverviewViewViewModel();
        DetailViewViewModel _detailViewViewModel = new DetailViewViewModel();

        public MainViewViewModel()
        {
            // Udkommenteret under View-Viewmodel test
            //Initialize();

            //Setup the current workspace aka the view to be shown
            CurrentWorkspace = _overviewViewViewModel;
            ChangeViewCommand = new RelayCommand(ChangeView);

            ////DateTime d;
            ////d = DateTime.Now;
            ////string day = d.Day.ToString().PadLeft(2, '0');
            ////string month = d.ToString("MMM", _culture);
            ////string year = d.Year.ToString();
            ////string hour = d.Hour.ToString().PadLeft(2, '0');
            ////string minute = d.Minute.ToString().PadLeft(2, '0');

            ////CurrentDateTime = day + ". " + month + ". " + year + "\t" + hour + ":" + minute;
            //_timer.Tick += new EventHandler(Timer_Click);
            ////_timer.Interval = new TimeSpan(0, 0, 1);
            //_timer.Interval = TimeSpan.FromSeconds(1);
            //_timer.Start();
        }
        
        private void Initialize()
        {
            //Data layer Initialize
            LoadConfigurationSettings lcs = new LoadConfigurationSettings();
            GetPatientsFromFhir fhirCommands = new GetPatientsFromFhir(lcs);


            TestProperty = "test";
            SortPatients ctrlBL = new SortPatients();
            //_listOfPatients = ctrlBL.recievePatients();


            //Dette bliver til vores "Main", der initializer alt der skal initializes
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

        //// Timer made with inspiration from:
        //// https://stackoverflow.com/a/5410783

        //readonly System.Windows.Threading.DispatcherTimer _timer = new System.Windows.Threading.DispatcherTimer();

        // Model for ViewModel
        private MainViewModel mainModel = new MainViewModel();
        // Property for binding 
        public string CurrentDateTime { get mainModel.CurrentDateTime; set; }

        //private readonly CultureInfo _culture = CultureInfo.CurrentCulture;

        //private void Timer_Click(object sender, EventArgs e)
        //{
        //    DateTime d;

        //    d = DateTime.Now;
        //    string day = d.Day.ToString().PadLeft(2, '0');
        //    string month = d.ToString("MMM", _culture);
        //    string year = d.Year.ToString();
        //    string hour = d.Hour.ToString().PadLeft(2, '0');
        //    string minute = d.Minute.ToString().PadLeft(2, '0');

        //    CurrentDateTime = day + ". " + month + ". " + year + "\t" + hour + ":" + minute;
        //    OnPropertyChanged("CurrentDateTime");
        //}


        public string TestProperty
        {
            get { return _test; }
            set
            {
                if (value == _test) return;
                _test = value;
            }
        }
        public List<OurPatient> listOfPatients
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
