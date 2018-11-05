using System.Collections.Generic;
using IncomingCasualtyHandling.BL;
using IncomingCasualtyHandling.DAL;
using OurPatient = IncomingCasualtyHandling.BL.Models.OurPatient;

namespace IncomingCasualtyHandling.GUI.ViewModels
{
    internal class ViewModel
    {
        private string _test;
        private List<OurPatient> _listOfPatients;

        public ViewModel()
        {
            initialize();
        }
        private void initialize()
        {
            //Data layer initialize
            LoadConfigurationSettings lcs = new LoadConfigurationSettings();
            GetPatientsFromFhir fhirCommands = new GetPatientsFromFhir(lcs);


            TestProperty = "test";
            SortPatients ctrlBL = new SortPatients();
            //_listOfPatients = ctrlBL.recievePatients();


            //Dette bliver til vores "Main", der initializer alt der skal initializes
        }
        public string TestProperty
        {
            get { return _test; }
            set
            {
                if (value == _test) return;
                _test = value;
            }
        }
        public List<OurPatient> listOfPatients
        {
            get { return _listOfPatients; }
            set
            {
                if (value == _listOfPatients) return;
                _listOfPatients = value;
            }
        }
    }
}
