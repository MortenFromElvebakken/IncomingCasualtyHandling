using IncomingCasualtyHandling.BL.Object_classes;
using System.Collections.Generic;

namespace IncomingCasualtyHandling.BL.Models
{
    public class OverviewView_Model : ObservableObject
    {
        #region Triages

        private int _maximumTriages = 10;
        public int MaximumTriages => _maximumTriages;
        
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

        #endregion


        #region Specialties

        private List<Specialty> _listOfSpecialties;
        public List<Specialty> ListOfSpecialities
        {
            get => _listOfSpecialties;
            set
            {
                _listOfSpecialties = value;
                int counter = 1;
                foreach (var specialty in ListOfSpecialities)
                {
                    string propertyName = "Specialty" + counter;
                    OnPropertyChanged(propertyName);
                    counter++;
                }

            }
        }

        public Specialty Specialty1
        {
            get => ListOfSpecialities[0];
            set => ListOfSpecialities[0] = value;
        }

        public Specialty Specialty2
        {
            get => ListOfSpecialities[1];
            set => ListOfSpecialities[1] = value;
        }

        public Specialty Specialty3
        {
            get => ListOfSpecialities[2];
            set => ListOfSpecialities[2] = value;
        }

        public Specialty Specialty4
        {
            get => ListOfSpecialities[3];
            set => ListOfSpecialities[3] = value;
        }

        public Specialty Specialty5
        {
            get => ListOfSpecialities[4];
            set => ListOfSpecialities[4] = value;
        }

        public Specialty Specialty6
        {
            get => ListOfSpecialities[5];
            set => ListOfSpecialities[5] = value;
        }

        public Specialty Specialty7
        {
            get => ListOfSpecialities[6];
            set => ListOfSpecialities[6] = value;
        }

        public Specialty Specialty8
        {
            get => ListOfSpecialities[7];
            set => ListOfSpecialities[7] = value;
        }

        public Specialty Specialty9
        {
            get => ListOfSpecialities[8];
            set => ListOfSpecialities[8] = value;
        }

        public Specialty Specialty10
        {
            get => ListOfSpecialities[9];
            set => ListOfSpecialities[9] = value;
        }

        public Specialty Specialty11
        {
            get => ListOfSpecialities[10];
            set => ListOfSpecialities[10] = value;
        }

        public Specialty Specialty12
        {
            get => ListOfSpecialities[11];
            set => ListOfSpecialities[11] = value;
        }

        public Specialty Specialty13
        {
            get => ListOfSpecialities[12];
            set => ListOfSpecialities[11] = value;
        }

        public Specialty Specialty14
        {
            get => ListOfSpecialities[13];
            set => ListOfSpecialities[13] = value;
        }

        public Specialty Specialty15
        {
            get => ListOfSpecialities[14];
            set => ListOfSpecialities[14] = value;
        }

        public Specialty Specialty16
        {
            get => ListOfSpecialities[15];
            set => ListOfSpecialities[15] = value;
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
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor

        public OverviewView_Model()
        {
           // specialties.Add(new Specialty
           // {
           //     Name = "Orthopaedic",
           //     Colour = "#af3205",
           //     Amount = 5,
           //     ShowAs = Visibility.Visible
           // });
           // specialties.Add(new Specialty
           // {
           //     Name = "Medicinal",
           //     Colour = "#9400D3",
           //     Amount = 9,
           //     ShowAs = Visibility.Visible
           // });
           // specialties.Add(new Specialty
           // {
           //     Name = "Thoracic surgery",
           //     Colour = "#003865",
           //     Amount = 4,
           //     ShowAs = Visibility.Visible
           // });

           // var counter = 0;
           // Array.Clear(listOfSpecialities, 0, listOfSpecialities.Length);
           // specialties.Sort((x, y) => y.Amount.CompareTo(x.Amount));
           // foreach (var specialty in specialties)
           // {
           //     if (specialty != null)
           //     {
           //         listOfSpecialities[counter] = specialty;
           //         counter++;
           //     }
           // }

           // Specialty4 = new Specialty
           // {
           //     ShowAs = Visibility.Collapsed
           // };
           // Specialty5 = new Specialty
           // {
           //     ShowAs = Visibility.Collapsed
           // };
           // Specialty6 = new Specialty
           // {
           //     ShowAs = Visibility.Collapsed
           // };
           // Specialty7 = new Specialty
           // {
           //     ShowAs = Visibility.Collapsed
           // };
           // Specialty8 = new Specialty
           // {
           //     ShowAs = Visibility.Collapsed
           // };
           // Specialty9 = new Specialty
           // {
           //     ShowAs = Visibility.Collapsed
           // };
           // Specialty10 = new Specialty
           // {
           //     ShowAs = Visibility.Collapsed
           // };
           // Specialty11 = new Specialty
           // {
           //     ShowAs = Visibility.Collapsed
           // };
           // Specialty12 = new Specialty
           // {
           //     ShowAs = Visibility.Collapsed
           // };
           // Specialty13 = new Specialty
           // {
           //     ShowAs = Visibility.Collapsed
           // };
           // Specialty14 = new Specialty
           // {
           //     ShowAs = Visibility.Collapsed
           // };
           // Specialty15 = new Specialty
           // {
           //     ShowAs = Visibility.Collapsed
           // };
           // Specialty16 = new Specialty
           // {
           //     ShowAs = Visibility.Collapsed
           // };

           // Triage1 = new Triage
           // {
           //     Amount = 8,
           //     Colour = "#f60e0e",
           //     Name = "Red",
           //     ShowAs = Visibility.Visible
           // };

           // Triage2 = new Triage
           // {
           //     Amount = 0,
           //     Colour = "#f28d0e",
           //     Name = "Orange",
           //     ShowAs = Visibility.Collapsed
           // };

           // Triage3 = new Triage
           // {
           //     Amount = 5,
           //     Colour = "#ffe913",
           //     Name = "Yellow",
           //     ShowAs = Visibility.Visible
           // };
           //Triage4 = new Triage
           // {
           //     Amount = 2,
           //     Colour = "#0bdd2e",
           //     Name = "Green",
           //     ShowAs = Visibility.Visible
           // };
           // Triage5 = new Triage
           // {
           //     Amount = 0,
           //     Colour = "#1e38ff",
           //     Name = "Blue",
           //     ShowAs = Visibility.Collapsed
           // };

           // Eta = new ETA
           // {
           //     AbsoluteTime = "10:42",
           //     RelativeTime = "(-08:00)"
           // };
        }

#endregion
    }
}
