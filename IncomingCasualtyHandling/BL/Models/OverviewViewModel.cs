using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IncomingCasualtyHandling.BL.Models
{
    class OverviewViewModel
    {
        #region Triages

        private int _maximumTriages = 10;
        public int MaximumTriages => _maximumTriages;

        //private Triage _triage1;
        //private Triage _triage2;
        //private Triage _triage3;
        //private Triage _triage4;
        //private Triage _triage5;

        public Triage Triage1 { get; set; }

        public Triage Triage2 { get; set; }

        public Triage Triage3 { get; set; }


        public Triage Triage4 { get; set; }

        public Triage Triage5 { get; set; }

        #endregion


        #region Specialties

        private Specialty[] listOfSpecialities = new Specialty[16];

        public Specialty Specialty1
        {
            get => listOfSpecialities[0];
            set => listOfSpecialities[0] = value;
        }

        public Specialty Specialty2
        {
            get => listOfSpecialities[1];
            set => listOfSpecialities[1] = value;
        }

        public Specialty Specialty3
        {
            get => listOfSpecialities[2];
            set => listOfSpecialities[2] = value;
        }

        public Specialty Specialty4
        {
            get => listOfSpecialities[3];
            set => listOfSpecialities[3] = value;
        }

        public Specialty Specialty5
        {
            get => listOfSpecialities[4];
            set => listOfSpecialities[4] = value;
        }

        public Specialty Specialty6
        {
            get => listOfSpecialities[5];
            set => listOfSpecialities[5] = value;
        }

        public Specialty Specialty7
        {
            get => listOfSpecialities[6];
            set => listOfSpecialities[6] = value;
        }

        public Specialty Specialty8
        {
            get => listOfSpecialities[7];
            set => listOfSpecialities[7] = value;
        }

        public Specialty Specialty9
        {
            get => listOfSpecialities[8];
            set => listOfSpecialities[8] = value;
        }

        public Specialty Specialty10
        {
            get => listOfSpecialities[9];
            set => listOfSpecialities[9] = value;
        }

        public Specialty Specialty11
        {
            get => listOfSpecialities[10];
            set => listOfSpecialities[10] = value;
        }

        public Specialty Specialty12
        {
            get => listOfSpecialities[11];
            set => listOfSpecialities[11] = value;
        }

        public Specialty Specialty13
        {
            get => listOfSpecialities[12];
            set => listOfSpecialities[11] = value;
        }

        public Specialty Specialty14
        {
            get => listOfSpecialities[13];
            set => listOfSpecialities[13] = value;
        }

        public Specialty Specialty15
        {
            get => listOfSpecialities[14];
            set => listOfSpecialities[14] = value;
        }

        public Specialty Specialty16
        {
            get => listOfSpecialities[15];
            set => listOfSpecialities[15] = value;
        }


        private List<Specialty> specialties = new List<Specialty>();

        #endregion


        #region ETA

        public ETA Eta { get; set; }

        #endregion

        #region Constructor

        public OverviewViewModel()
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

            Specialty4 = new Specialty
            {
                ShowAs = Visibility.Collapsed
            };
            Specialty5 = new Specialty
            {
                ShowAs = Visibility.Collapsed
            };
            Specialty6 = new Specialty
            {
                ShowAs = Visibility.Collapsed
            };
            Specialty7 = new Specialty
            {
                ShowAs = Visibility.Collapsed
            };
            Specialty8 = new Specialty
            {
                ShowAs = Visibility.Collapsed
            };
            Specialty9 = new Specialty
            {
                ShowAs = Visibility.Collapsed
            };
            Specialty10 = new Specialty
            {
                ShowAs = Visibility.Collapsed
            };
            Specialty11 = new Specialty
            {
                ShowAs = Visibility.Collapsed
            };
            Specialty12 = new Specialty
            {
                ShowAs = Visibility.Collapsed
            };
            Specialty13 = new Specialty
            {
                ShowAs = Visibility.Collapsed
            };
            Specialty14 = new Specialty
            {
                ShowAs = Visibility.Collapsed
            };
            Specialty15 = new Specialty
            {
                ShowAs = Visibility.Collapsed
            };
            Specialty16 = new Specialty
            {
                ShowAs = Visibility.Collapsed
            };

            Triage1 = new Triage
            {
                Amount = 8,
                Colour = "#f60e0e",
                Name = "Red",
                ShowAs = Visibility.Visible
            };

            Triage2 = new Triage
            {
                Amount = 0,
                Colour = "#f28d0e",
                Name = "Orange",
                ShowAs = Visibility.Collapsed
            };

            Triage3 = new Triage
            {
                Amount = 5,
                Colour = "#ffe913",
                Name = "Yellow",
                ShowAs = Visibility.Visible
            };
           Triage4 = new Triage
            {
                Amount = 2,
                Colour = "#0bdd2e",
                Name = "Green",
                ShowAs = Visibility.Visible
            };
            Triage5 = new Triage
            {
                Amount = 0,
                Colour = "#1e38ff",
                Name = "Blue",
                ShowAs = Visibility.Collapsed
            };

            Eta = new ETA
            {
                AbsoluteTime = "10:42",
                RelativeTime = "(-08:00)"
            };
        }

#endregion
    }
}
