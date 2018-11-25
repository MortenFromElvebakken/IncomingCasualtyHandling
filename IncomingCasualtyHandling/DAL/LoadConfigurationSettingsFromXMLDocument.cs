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
        public string ServerName { get; private set; }
        public string HospitalShortName { get; private set; }
        

        public LoadConfigurationSettingsFromXMLDocument()
        {
            XmlServerName = "http://localhost:8080/Conf.Fapi/Configuration.xml";
            configFile = new XmlDocument();
            configFile.Load(XmlServerName);
            GetHospitalShortName();
            GetServerName();
        }
        public LoadConfigurationSettingsFromXMLDocument(string _newXmlServerName)
        {
            XmlServerName = _newXmlServerName; 
            configFile = new XmlDocument();
            configFile.Load(XmlServerName);


            GetHospitalShortName();
            GetServerName();
            ReturnTriageList();
            ReturnSpecialtyList();

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

        public List<Triage> ReturnTriageList()
        {
            List<Triage> triageList = new List<Triage>();
            
            foreach (XmlNode c in configFile.LastChild.ChildNodes[2])
            {
                var _triage = new Triage();
                //
                _triage.Name = c.FirstChild.InnerText;
                _triage.Colour = c.LastChild.InnerText;
                _triage.Amount = 0;
                _triage.ShowAs = Visibility.Collapsed;
                triageList.Add(_triage);
            }

            return triageList;

        }
        

        public List<Specialty> ReturnSpecialtyList()
        {
            List<Specialty> specialtiesList = new List<Specialty>();
            foreach (XmlNode c in configFile.LastChild.ChildNodes[3])
            {
                var _specialty = new Specialty();
                _specialty.Name = c.FirstChild.InnerText;
                _specialty.Colour = "#af3205";//c.LastChild.InnerText; //Not yet implemented since colour isnt known on the specialties
                _specialty.Amount = 0;
                _specialty.ShowAs = Visibility.Collapsed;

                specialtiesList.Add(_specialty);
            }

            return specialtiesList;
        }

    }
}
