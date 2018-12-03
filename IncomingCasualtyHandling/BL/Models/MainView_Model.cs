using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL;
using IncomingCasualtyHandling.DAL.Interface;

namespace IncomingCasualtyHandling.BL.Models
{
    public class MainView_Model : ObservableObject, IMainView_Model
    {


        private ILoadData _iLoadData;
        public MainView_Model(ILoadData iLoadData)
        {
            _iLoadData = iLoadData;
            _iLoadData.NoInternet += SetInternetProperty;
        }

        

        private string _currentDateTime;
        public string CurrentDateTime
        {
            get => _currentDateTime;
            set
            {
                if (_currentDateTime != value)
                {
                    _currentDateTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _serverName;
        public string ServerName
        {
            get => _serverName;
            set
            {
                _serverName = value;
                _iLoadData.SetFhirClientURL(_serverName);
            }
        }



        #region Triage, ETA and specialty

        private List<Triage> _listOfTriages;
        public List<Triage> ListOfTriages
        {
            get => _listOfTriages;
            set
            {
                _listOfTriages = value;
                int counter = 1;
                foreach (var triage in _listOfTriages)
                {
                    string propertyName = "Triage" + counter;
                    OnPropertyChanged(propertyName);
                    counter++;
                }

            }
        }

        public Triage Triage1
        {
            get => ListOfTriages[0];
            set => ListOfTriages[0] = value;
        }

        public Triage Triage2
        {
            get => ListOfTriages[1];
            set => ListOfTriages[1] = value;
        }

        public Triage Triage3
        {
            get => ListOfTriages[2];
            set => ListOfTriages[2] = value;
        }

        public Triage Triage4
        {
            get => ListOfTriages[3];
            set => ListOfTriages[3] = value;
        }

        public Triage Triage5
        {
            get => ListOfTriages[4];
            set => ListOfTriages[4] = value;
        }

        public Triage Triage6
        {
            get => ListOfTriages[5];
            set => ListOfTriages[5] = value;
        }

        private ETA _ETA;
        public ETA ETA
        {
            get => _ETA;
            set
            {
                if (value != null)
                {
                    _ETA = value;
                    OnPropertyChanged();
                }
                
            }
        }

        public Specialty Specialty1 { get; set; }
        #endregion
        
        

        #region InternetConnection

        
        private Visibility _connectionToInternet = Visibility.Collapsed;
        public Visibility ConnectionToInternet
        {
            get => _connectionToInternet;
            set
            {
                if (_connectionToInternet == value) return;
                _connectionToInternet = value;
                OnPropertyChanged();
            }
        }
        private static DateTime _whenServerStoppedResponding = default(DateTime);

        public void SetInternetProperty(bool b)
        {
            if (b == false)
            {
                _whenServerStoppedResponding = DateTime.Now;
                ConnectionToInternet = Visibility.Visible;
                OnPropertyChanged(NoConnectionString);
            }
            else
            {
                
                ConnectionToInternet = Visibility.Collapsed;
            }
        }
        public string NoConnectionString { get; } = string.Format("Could not contact server, no updates are made. Last update: " + _whenServerStoppedResponding);
        #endregion

    }
}
