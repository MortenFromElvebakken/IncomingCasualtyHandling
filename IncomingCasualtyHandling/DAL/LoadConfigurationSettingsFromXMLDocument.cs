using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using Hl7.Fhir.ElementModel;

using IncomingCasualtyHandling.BL.Object_classes;

namespace IncomingCasualtyHandling.DAL
{
    public class LoadConfigurationSettingsFromXMLDocument
    {
        private readonly string xmlserver;
        private string _server;
        private string _hospital;
        private XmlDocument configFile;

        public List<Triage> TriageList { get; set; }
        public List<Specialty> SpecialtiesList { get; set; }
        public string ServerName { get; set; }
        public string HospitalShortName { get; set; }

        public LoadConfigurationSettingsFromXMLDocument()
        {
            xmlserver = "http://localhost:8080/Conf.Fapi/Configuration.xml"; //Muligt at sætte denne ved opstart af program?
            configFile = new XmlDocument();
            try
            {
                configFile.Load(xmlserver);
            }
            catch (System.IO.FileNotFoundException)
            {
                //Fejlmeddelelse
                Debug.WriteLine("Configuration file could not be found on server");
            }


            GetHospitalShortName();
            GetServerName();
            CreateTriageList();
            CreateSpecialtyList();

        }

        private void GetServerName()
        {
            _server = configFile.LastChild.ChildNodes[0].InnerText; //Ikke pænt, lav noget med indexering og den leder efter navn
            ServerName = _server;
        }
        private void GetHospitalShortName()
        {
            _hospital = configFile.LastChild.ChildNodes[1].InnerText;
            HospitalShortName = _hospital;
        }

        private void CreateTriageList()
        {
            List<Triage> _triageList = new List<Triage>();
            
            foreach (XmlNode c in configFile.LastChild.ChildNodes[2])
            {
                var _triage = new Triage();
                //
                _triage.Name = c.FirstChild.InnerText;
                _triage.Colour = c.LastChild.InnerText;
                _triage.Amount = 0;
                //_triage.Height = 200;
                _triage.ShowAs = Visibility.Collapsed;

                _triageList.Add(_triage);
            }

            TriageList = _triageList;

        }
        

        private void CreateSpecialtyList()
        {
            List<Specialty> _specialtiesList = new List<Specialty>();
            foreach (XmlNode c in configFile.LastChild.ChildNodes[3])
            {
                var _specialty = new Specialty();
                _specialty.Name = c.FirstChild.InnerText;
                _specialty.Colour = "#af3205";//c.LastChild.InnerText; //Not yet implemented since colour isnt known on the specialties
                _specialty.Amount = 0;
                _specialty.ShowAs = Visibility.Collapsed;

                _specialtiesList.Add(_specialty);
            }

            SpecialtiesList = _specialtiesList;
        }

    }
}
