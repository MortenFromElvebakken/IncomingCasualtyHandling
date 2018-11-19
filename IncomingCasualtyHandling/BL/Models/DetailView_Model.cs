using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Object_classes;

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

        private List<List<PatientModel>> _listOfTriagePatientLists;

        public List<List<PatientModel>> ListOfTriagePatientLists
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
            }
        }


        public List<PatientModel> Triage1Patients
        {
            get => ListOfTriagePatientLists[0];
            set => ListOfTriagePatientLists[0] = value;
        }

        public List<PatientModel> Triage2Patients
        {
            get => ListOfTriagePatientLists[1];
            set => ListOfTriagePatientLists[1] = value;
        }

        public List<PatientModel> Triage3Patients
        {
            get => ListOfTriagePatientLists[2];
            set => ListOfTriagePatientLists[2] = value;
        }

        public List<PatientModel> Triage4Patients
        {
            get => ListOfTriagePatientLists[3];
            set => ListOfTriagePatientLists[3] = value;
        }

        public List<PatientModel> Triage5Patients
        {
            get => ListOfTriagePatientLists[4];
            set => ListOfTriagePatientLists[4] = value;
        }

        public List<PatientModel> Triage6Patients
        {
            get => ListOfTriagePatientLists[5];
            set => ListOfTriagePatientLists[5] = value;
        }

        #endregion

        #region specialties

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

        private List<List<PatientModel>> _listOfSpecialtiesPatientLists;

        public List<List<PatientModel>> ListOfSpecialtiesPatientLists
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
            }
        }

#endregion

        #region specialtiesInList

        public List<PatientModel> Specialty1Patients
        {
            get => ListOfSpecialtiesPatientLists[0];
            set => ListOfSpecialtiesPatientLists[0] = value;
        }

        public List<PatientModel> Specialty2Patients
        {
            get => ListOfSpecialtiesPatientLists[1];
            set => ListOfSpecialtiesPatientLists[1] = value;
        }

        public List<PatientModel> Specialty3Patients
        {
            get => ListOfSpecialtiesPatientLists[2];
            set => ListOfSpecialtiesPatientLists[2] = value;
        }

        public List<PatientModel> Specialty4Patients
        {
            get => ListOfSpecialtiesPatientLists[3];
            set => ListOfSpecialtiesPatientLists[3] = value;
        }

        public List<PatientModel> Specialty5Patients
        {
            get => ListOfSpecialtiesPatientLists[4];
            set => ListOfSpecialtiesPatientLists[4] = value;
        }

        public List<PatientModel> Specialty6Patients
        {
            get => ListOfSpecialtiesPatientLists[5];
            set => ListOfSpecialtiesPatientLists[5] = value;
        }

        public List<PatientModel> Specialty7Patients
        {
            get => ListOfSpecialtiesPatientLists[6];
            set => ListOfSpecialtiesPatientLists[6] = value;
        }

        public List<PatientModel> Specialty8Patients
        {
            get => ListOfSpecialtiesPatientLists[7];
            set => ListOfSpecialtiesPatientLists[7] = value;
        }

        public List<PatientModel> Specialty9Patients
        {
            get => ListOfSpecialtiesPatientLists[8];
            set => ListOfSpecialtiesPatientLists[8] = value;
        }

        public List<PatientModel> Specialty10Patients
        {
            get => ListOfSpecialtiesPatientLists[9];
            set => ListOfSpecialtiesPatientLists[9] = value;
        }

        public List<PatientModel> Specialty11Patients
        {
            get => ListOfSpecialtiesPatientLists[10];
            set => ListOfSpecialtiesPatientLists[10] = value;
        }

        public List<PatientModel> Specialty12Patients
        {
            get => ListOfSpecialtiesPatientLists[11];
            set => ListOfSpecialtiesPatientLists[11] = value;
        }

        public List<PatientModel> Specialty13Patients
        {
            get => ListOfSpecialtiesPatientLists[12];
            set => ListOfSpecialtiesPatientLists[12] = value;
        }

        public List<PatientModel> Specialty14Patients
        {
            get => ListOfSpecialtiesPatientLists[13];
            set => ListOfSpecialtiesPatientLists[13] = value;
        }

        public List<PatientModel> Specialty15Patients
        {
            get => ListOfSpecialtiesPatientLists[14];
            set => ListOfSpecialtiesPatientLists[14] = value;
        }

        public List<PatientModel> Specialty16Patients
        {
            get => ListOfSpecialtiesPatientLists[15];
            set => ListOfSpecialtiesPatientLists[15] = value;
        }

        


        #endregion

        #region Eta

        private List<PatientModel> _ETAPatients;

        public List<PatientModel> ETAPatients
        {
            get => _ETAPatients;
            set
            {
                _ETAPatients = value;
                OnPropertyChanged("ETAPatients");
            }
        }

        #endregion

        #region tabItems

        private int _selectedTabIndex;

        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set
            {
                if (value >= 0)
                {
                    _selectedTabIndex = value;
                    OnPropertyChanged("PatientsInList");
                }
            }
        }

        public string SelectedOverview { get; set; }

        private string _stringFromCommandParameter = "";

        public string StringFromChangeViewCommandParameter
        {
            get { return _stringFromCommandParameter; }
            set
            {
                _stringFromCommandParameter = value;
                OnPropertyChanged("StringFromChangeViewCommandParameter");
                
            }
        }

        private string _patientsInList;

        public string PatientsInList
        {
            get => String.Format(_tabsList[SelectedTabIndex].Data.Count + " patient(s)");
            set
            {
                _patientsInList = value;
                OnPropertyChanged("PatientsInList");
            }
        }

        private List<TabControl> _tabsList = new List<TabControl>();

        public List<TabControl> ListOfTabs
        {
            get
            {

                List<TabControl> _tempTabList = new List<TabControl>();

                //finder parametre der skal bruges
                string[] parameters = StringFromChangeViewCommandParameter.ToString().Split(' ');
                SelectedOverview = parameters[0].ToString();
                SelectedTabIndex = Convert.ToInt16(parameters[1]);

                if (SelectedOverview == "Triage")
                {
                    int counter = 0;
                    foreach (var triage in ListOfTriages)
                    {
                        if (triage.Amount != 0)
                        {
                            var _tab = new TabControl()
                            {
                                Name = triage.Name,
                                Data = ListOfTriagePatientLists.Find(item => item[0].Triage == triage.Name)
                            };

                            _tempTabList.Add(_tab);
                        }

                        counter++;
                    }

                    _tabsList = _tempTabList;
                    return _tabsList;
                }

                if (SelectedOverview == "Specialty")
                {
                    int counter = 0;
                    foreach (var specialty in ListOfSpecialties)
                    {
                        if (specialty.Amount != 0)
                        {
                            var _tab = new TabControl()
                            {
                                Name = specialty.Name,
                                Data = ListOfSpecialtiesPatientLists.Find(item => item[0].Specialty == specialty.Name)

                            };
                            _tempTabList.Add(_tab);
                        }

                        counter++;
                    }

                    _tabsList = _tempTabList;
                    return _tabsList;
                }
                else
                {
                    var _tab = new TabControl()
                    {
                        Name = "ETA",
                        Data = ETAPatients

                    };
                    _tempTabList.Add(_tab);
                    _tabsList = _tempTabList;
                    return _tabsList;
                }
            }
            set
            {
                _tabsList = value;
                OnPropertyChanged("Tabs");

            }
        }
        #endregion
    }
}