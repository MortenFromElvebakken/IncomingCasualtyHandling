using System;
using System.Collections.Generic;
using System.Windows;
using IncomingCasualtyHandling.BL.Models;

namespace IncomingCasualtyHandling.GUI.ViewModels
{
    public class OverviewViewViewModel : WorkspaceViewModel
    {

        #region Triages

        private int _maximumTriages = 10;
        public int MaximumTriages => _maximumTriages;

        private Triage _triage1;
        private Triage _triage2;
        private Triage _triage3;
        private Triage _triage4;
        private Triage _triage5;

        public Triage Triage1
        {
            get => _triage1;
            set
            {
                _triage1 = value;
                OnPropertyChanged("Triage1");
            }
        }

        public Triage Triage2
        {
            get => _triage2;
            set
            {
                _triage2 = value;
                OnPropertyChanged("Triage2");
            }
        }

        public Triage Triage3
        {
            get => _triage3;
            set
            {
                _triage3 = value;
                OnPropertyChanged("Triage3");
            }
        }

        public Triage Triage4
        {
            get => _triage4;
            set
            {
                _triage4 = value;
                OnPropertyChanged("Triage4");
            }
        }

        public Triage Triage5
        {
            get => _triage5;
            set
            {
                _triage5 = value;
                OnPropertyChanged("Triage5");
            }
        }

        #endregion


        #region Specialties

        private Specialty[] listOfSpecialities = new Specialty[16];

        public Specialty Specialty1
        {
            get
            {
                return listOfSpecialities[0];
            }
            set
            {
                listOfSpecialities[0] = value;
                OnPropertyChanged("Specialty1");
                
            }
        }

        public Specialty Specialty2
        {
            get
            {
                return listOfSpecialities[1];
            }
            set
            {
                listOfSpecialities[1] = value; 
                OnPropertyChanged("Specialty2");
            }
        }

        public Specialty Specialty3
        {
            get
            {
                return listOfSpecialities[2];
            }
            set
            {
                listOfSpecialities[1] = value;
                OnPropertyChanged("Specialty3");
            }
        }

        private List<Specialty> specialties = new List<Specialty>();

        #endregion


        #region ETA

        private ETA _eta;

        public ETA Eta
        {
            get => _eta;
            set
            {
                _eta = value;
                OnPropertyChanged("ETA");
            }
        }

        #endregion


        #region Constructor
        public OverviewViewViewModel()
        {
            specialties.Add(new Specialty
            {
                Name = "Orthopaedic",
                Colour = "#af3205",
                Amount = 5,
                ShowAs = Visibility.Visible
            });
            specialties.Add(new Specialty
            {
                Name = "Medicinal",
                Colour = "#9400D3",
                Amount = 9,
                ShowAs = Visibility.Visible
            });
            specialties.Add(new Specialty
            {
                Name = "Thoracic surgery",
                Colour = "#003865",
                Amount = 4,
                ShowAs = Visibility.Visible
            });

            var counter = 0;
            Array.Clear(listOfSpecialities, 0, listOfSpecialities.Length);
            specialties.Sort((x, y) => y.Amount.CompareTo(x.Amount));
            foreach (var specialty in specialties)
            {
                if (specialty != null)
                {
                    listOfSpecialities[counter] = specialty;
                    counter++;
                }
            }

            _triage1 = new Triage
            {
                Amount = 8,
                Colour = "#f60e0e",
                Name = "Red",
                ShowAs = Visibility.Visible
            };

            _triage2 = new Triage
            {
                Amount = 0,
                Colour = "#f28d0e",
                Name = "Orange",
                ShowAs = Visibility.Collapsed
            };

            _triage3 = new Triage
            {
                Amount = 5,
                Colour = "#ffe913",
                Name = "Yellow",
                ShowAs = Visibility.Visible
            };
            _triage4 = new Triage
            {
                Amount = 2,
                Colour = "#0bdd2e",
                Name = "Green",
                ShowAs = Visibility.Visible
            };
            _triage5 = new Triage
            {
                Amount = 0,
                Colour = "#1e38ff",
                Name = "Blue",
                ShowAs = Visibility.Collapsed
            };

            _eta = new ETA
            {
                AbsoluteTime = "10:42",
                RelativeTime = "(-08:00)"
            };
        }

        #endregion
    }
}
