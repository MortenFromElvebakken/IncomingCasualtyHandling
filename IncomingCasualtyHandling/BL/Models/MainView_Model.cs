using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Interfaces;
using IncomingCasualtyHandling.BL.Object_classes;
using IncomingCasualtyHandling.DAL;
using IncomingCasualtyHandling.DAL.Interface;

namespace IncomingCasualtyHandling.BL.Models
{
    public class MainView_Model : ObservableObject, IMainView_Model
    {
        private IGetPatientsFromFHIR iGetPatientsFromFhir;
        public MainView_Model(IGetPatientsFromFHIR _IGetPatientsFromFHIR)
        {
            iGetPatientsFromFhir = _IGetPatientsFromFHIR;
        }

        private string _currentDateTime;
        // Property for binding 
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

        public ETA Eta { get; set; }
        public Specialty Specialty1 { get; set; }


        //private readonly CultureInfo _culture = CultureInfo.CurrentCulture;
        private string _serverName;
        public string ServerName
        {
            get => _serverName;
            set
            {
                _serverName = value;
                //Check if servername is correct - må vi gøre det i codebehind i vinduet,
                //Og derfor kun gå ud af det(og godtage det, hvis det virker)
                iGetPatientsFromFhir.setFhirClientURL(_serverName);
            }
        }


    }
}
