using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using Hl7.Fhir.Model;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.GUI.ViewModels
{

    //public class Patient
    //{
    //    public string Name { get; set; }
    //    public int Age { get; set; }
    //}

    public class TabElement
    {
        public string TabTitle { get; set; }

        public List<PatientModel> Data { get; set; } = new List<PatientModel>();

    }

    public class DetailViewViewModel : WorkspaceViewModel
    {
        List<Triage> _listOfTriages = new List<Triage>();

        private List<Triage> ListOfTriages
        {
            get => _detailViewModel.ListOfTriages;
            set
            {
                _listOfTriages = value;
                OnPropertyChanged();
            }
        }

        List<Specialty> _listOfSpecialty = new List<Specialty>();

        private ObservableCollection<PatientModel> _triage1Patients = new ObservableCollection<PatientModel>();
        private ObservableCollection<Patient> _triage2Patients = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> _triage3Patients = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> _triage4Patients = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> _triage5Patients = new ObservableCollection<Patient>();

        private ObservableCollection<Patient> _specialty1Patients = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> _specialty2Patients = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> _specialty3Patients = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> _specialty4Patients = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> _specialty5Patients = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> _specialty6Patients = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> _specialty7Patients = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> _specialty8Patients = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> _specialty9Patients = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> _specialty10Patients = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> _specialty11Patients = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> _specialty12Patients = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> _specialty13Patients = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> _specialty14Patients = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> _specialty15Patients = new ObservableCollection<Patient>();
        private ObservableCollection<Patient> _specialty16Patients = new ObservableCollection<Patient>();

        private ObservableCollection<Patient> _etaPatients = new ObservableCollection<Patient>();

        private TabElement _tabElement1 = new TabElement();

        private TabElement TabElement1
        {
            get
            {
                TabElement tabElement = new TabElement
                {
                    TabTitle = ListOfTriages[0].Name,
                    Data = _detailViewModel.Triage1Patients
                };
                return tabElement;
            }
            set => _tabElement1 = value;
        }

        private List<TabElement> _tabs = new List<TabElement>();

        public List<TabElement> Tabs => _tabs;

        //private ObservableCollection<TabElement> _tabs = new ObservableCollection<TabElement>();
        //public ObservableCollection<TabElement> Tabs => _tabs;


        public string TabTitle => "TabTitleText";

        public string Text => "TestText";
        private DetailViewModel _detailViewModel;
        public DetailViewViewModel()
        { }

        public DetailViewViewModel(DetailViewModel detailViewModel)
        {
            _detailViewModel = detailViewModel;
            _detailViewModel.PropertyChanged += DetailViewModelOnPropertyChanged;
            _tabElement1 = new TabElement
            {
                TabTitle = ListOfTriages[0].Name,
                Data = _detailViewModel.Triage1Patients
            };
            _tabs.Add(_tabElement1);
           

            //ObservableCollection<Patient> _patientList = new ObservableCollection<Patient>();
            //Patient patient1 = new Patient
            //{
            //    Name = "Patient1",
            //    Age = 5
            //};
            //Patient patient2 = new Patient
            //{
            //    Name = "Patient2",
            //    Age = 10
            //};
            //_patientList.Add(patient1);
            //_patientList.Add(patient2);
            //TabElement firstTab = new TabElement
            //{
            //    TabTitle = "FirstTab",
            //    Data = _patientList

            //};
            //TabElement secondTab = new TabElement
            //{
            //    TabTitle = "SecondTab"
            //};
            //TabElement thirdTab = new TabElement
            //{
            //    TabTitle = "ThirdTab"
            //};

            //_tabs.Add(firstTab);
            //_tabs.Add(secondTab);
            //string text = _tabs[1].ToString();

            //_tabs.Add(thirdTab);
        }

        private void DetailViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            OnPropertyChanged("TabElement1");
        }
    }
}
