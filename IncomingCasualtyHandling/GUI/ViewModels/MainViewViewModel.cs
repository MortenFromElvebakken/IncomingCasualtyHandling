using System.Collections.Generic;
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
