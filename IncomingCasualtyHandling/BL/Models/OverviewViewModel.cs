using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        private List<Specialty> specialties = new List<Specialty>();

        #endregion


        #region ETA

        public ETA Eta { get; set; }

        #endregion
    }
}
