using System;

namespace IncomingCasualtyHandling.BL.Models
{
    public class OurPatient
    {
        private string _patientID;

        private string _givenName;
        private string _familyName;
        private string _gender;

        private string _triage;
        private string _specialty;
        private string _toHospital;

        private DateTime _eta;

        public OurPatient()
        {
            //lav get/set på alle properties
        }

        public string PatientId
        {
            get { return _patientID; }
            internal set
            {
                if (value == _patientID) return;
                _patientID = value;
            }
        }
        public string GivenName
        {
            get { return _givenName; }
            internal set
            {
                if (value == _givenName) return;
                _givenName = value;
            }
        }
        public string FamilyName
        {
            get { return _familyName; }
            internal set
            {
                if (value == _familyName) return;
                _familyName = value;
            }
        }
        public string Gender
        {
            get { return _gender; }
            internal set
            {
                if (value == _gender) return;
                _gender = value;
            }
        }
        public string Triage
        {
            get { return _triage; }
            internal set
            {
                if (value == _triage) return;
                _triage = value;
            }
        }
        public string Specialty
        {
            get { return _specialty; }
            internal set
            {
                if (value == _specialty) return;
                _specialty = value;
            }
        }
        public DateTime ETA
        {
            get { return _eta; }
            internal set
            {
                if (value == _eta) return;
                _eta = value;
            }
        }

        public string toHospital
        {
            get { return _toHospital; }
            internal set
            {
                if (value == _toHospital) return;
                _toHospital = value;
            }
        }
    }
}
