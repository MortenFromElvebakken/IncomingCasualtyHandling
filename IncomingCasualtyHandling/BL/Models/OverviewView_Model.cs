using IncomingCasualtyHandling.BL.Object_classes;
using System.Collections.Generic;
using IncomingCasualtyHandling.BL.Interfaces;

namespace IncomingCasualtyHandling.BL.Models
{
    public class OverviewView_Model : ObservableObject, IOverviewView_Model
    {
        #region Triages

        private int _maximumTriagePatients = 10;
        public int MaximumTriagePatients => _maximumTriagePatients;
        
        

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
           
        }

#endregion
    }
}
