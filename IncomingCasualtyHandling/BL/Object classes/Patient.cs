using System;

namespace IncomingCasualtyHandling.BL.Models
{
    public class OurPatient
    {
        private string _patientId;

        private string _name;
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
            get => _patientId;
            internal set
            {
                if (value == _patientId) return;
                _patientId = value;
            }
        }
        public string Name
        {
            get => _name;
            internal set
            {
                if (value == _name) return;
                _name = value;
            }
        }
        public string Gender
        {
            get => _gender;
            internal set
            {
                if (value == _gender) return;
                _gender = value;
            }
        }
        public string Triage
        {
            get => _triage;
            internal set
            {
                if (value == _triage) return;
                _triage = value;
            }
        }
        public string Specialty
        {
            get => _specialty;
            internal set
            {
                if (value == _specialty) return;
                _specialty = value;
            }
        }
        public DateTime ETA
        {
            get => _eta;
            internal set
            {
                if (value == _eta) return;
                _eta = value;
            }
        }

        public string toHospital
        {
            get => _toHospital;
            internal set
            {
                if (value == _toHospital) return;
                _toHospital = value;
            }
        }
    }
}
