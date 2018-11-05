using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using IncomingCasualtyHandling.BL;
using IncomingCasualtyHandling.DAL;
using IncomingCasualtyHandling.Models;
using OurPatient = IncomingCasualtyHandling.Models.OurPatient;

namespace IncomingCasualtyHandling
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
