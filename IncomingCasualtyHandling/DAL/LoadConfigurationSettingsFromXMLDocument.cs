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
using IncomingCasualtyHandling.DAL.Interface;

namespace IncomingCasualtyHandling.DAL
{
    public class LoadConfigurationSettingsFromXMLDocument: ILoadConfigurationSettings
    {
        public string XmlServerName { get; set; }

        private string _server;
        private string _hospital;
        private XmlDocument configFile;

        public List<Triage> TriageList { get; set; }
        public List<Specialty> SpecialtiesList { get; set; }
        public string ServerName { get; private set; }
        public string HospitalShortName { get; private set; }
        

        public LoadConfigurationSettingsFromXMLDocument()
        {
            XmlServerName = "http://localhost:8080/Conf.Fapi/Configuration.xml";
            configFile = new XmlDocument();
            configFile.Load(XmlServerName);


            GetHospitalShortName();
            GetServerName();
            CreateTriageList();
            CreateSpecialtyList();

        }
        public LoadConfigurationSettingsFromXMLDocument(string _newXmlServerName)
        {
            XmlServerName = _newXmlServerName; 
            configFile = new XmlDocument();
            configFile.Load(XmlServerName);


            GetHospitalShortName();
            GetServerName();
            CreateTriageList();
            CreateSpecialtyList();

        }

        private void GetServerName()
        {
            _server = configFile.LastChild.ChildNodes[0].InnerText; 
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
