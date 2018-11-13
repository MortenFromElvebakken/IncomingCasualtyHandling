using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.BL.Models
{
    public class DetailViewModel : ObservableObject
    {
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

    }
}
