using System;
using System.Collections.Generic;
using System.Windows;
using IncomingCasualtyHandling.BL.Models;

namespace IncomingCasualtyHandling.GUI.ViewModels
{
    public class OverviewViewViewModel : WorkspaceViewModel
    {
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
                OnPropertyChanged();
                
            }
        }

        public Specialty Specialty2
        {
            get
            {
                return listOfSpecialities[1];
            }
            set => listOfSpecialities[1] = value;
        }

        private List<Specialty> specialties = new List<Specialty>();

        private int counter;
        private double barHeight;

        private int _maximumTriages = 10;
        public int MaximumTriages => _maximumTriages;

        private Triage _redTriage;
        private Triage _orangeTriage;
        private Triage _yellowTriage;
        private Triage _greenTriage;
        private Triage _blueTriage;

        public Triage RedTriage
        {
            get => _redTriage;
            set => _redTriage = value;
        }

        public Triage OrangeTriage
        {
            get => _orangeTriage;
            set => _orangeTriage = value;
        }

        public Triage YellowTriage
        {
            get => _yellowTriage;
            set => _yellowTriage = value;
        }

        public Triage GreenTriage
        {
            get => _greenTriage;
            set => _greenTriage = value;
        }

        public Triage BlueTriage
        {
            get => _blueTriage;
            set => _blueTriage = value;
        }

        public OverviewViewViewModel()
        {
            barHeight = 200 / 10;
            listOfSpecialities[0] = new Specialty
            {
                Name = "Orthopaedic",
                Colour = "#4e7454",
                Amount = 5,
                ShowAs = Visibility.Collapsed
            };

            listOfSpecialities[1] = new Specialty
            {
                Name = "Medicinal",
                Colour = "#9400D3",
                Amount = 9,
                ShowAs = Visibility.Visible
            };

            specialties.Add(new Specialty
            {
                Name = "Orthopaedic",
                Colour = "#FFFFFF",
                Amount = 5,
                ShowAs = Visibility.Collapsed
            });
            specialties.Add(new Specialty
            {
                Name = "Medicinal",
                Colour = "#9400D3",
                Amount = 9,
                ShowAs = Visibility.Visible
            });

            counter = 0;
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

            _redTriage = new Triage
            {
                Amount = 8,
                Colour = "#f60e0e",
                Name = "Red",
                ShowAs = Visibility.Visible
            };

            _orangeTriage = new Triage
            {
                Amount = 0,
                Colour = "#f28d0e",
                Name = "Orange",
                ShowAs = Visibility.Collapsed
            };

            _yellowTriage = new Triage
            {
                Amount = 5,
                Colour = "#ffe913",
                Name = "Yellow",
                ShowAs = Visibility.Visible
            };
            _greenTriage = new Triage
            {
                Amount = 2,
                Colour = "#0bdd2e",
                Name = "Green",
                ShowAs = Visibility.Visible
            };
            _blueTriage = new Triage
            {
                Amount = 0,
                Colour = "#1e38ff",
                Name = "Blue",
                ShowAs = Visibility.Collapsed
            };

            _redTriage.Height = _redTriage.Amount * barHeight;
            _orangeTriage.Height = _orangeTriage.Amount * barHeight;
            _yellowTriage.Height = _yellowTriage.Amount * barHeight;
            _greenTriage.Height = _greenTriage.Amount * barHeight;
            _blueTriage.Height = _blueTriage.Amount * barHeight;

        }
    }
}
