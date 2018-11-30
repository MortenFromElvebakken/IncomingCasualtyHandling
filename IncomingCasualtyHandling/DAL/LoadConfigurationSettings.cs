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
    public class LoadConfigurationSettings: ILoadConfigurationSettings
    {
        public string XmlServerName { get; set; }

        private string _server;
        private string _hospital;
        private readonly XmlDocument _configFile;
        public string ServerName { get; private set; }
        public string HospitalShortName { get; private set; }
        

        public LoadConfigurationSettings()
        {
            XmlServerName = "http://localhost:8080/Conf.Fapi/Configuration.xml";
            _configFile = new XmlDocument();
            _configFile.Load(XmlServerName);
            GetHospitalShortName();
            GetServerName();
        }

        public LoadConfigurationSettings(string _newXmlServerName)
        {
            XmlServerName = _newXmlServerName; 
            _configFile = new XmlDocument();
            _configFile.Load(XmlServerName);


            GetHospitalShortName();
            GetServerName();
            ReturnTriageList();
            ReturnSpecialtyList();

        }

        private void GetServerName()
        {
            _server = _configFile.LastChild.ChildNodes[0].InnerText; 
            ServerName = _server;
        }
        private void GetHospitalShortName()
        {
            _hospital = _configFile.LastChild.ChildNodes[1].InnerText;
            HospitalShortName = _hospital;
        }

        public List<Triage> ReturnTriageList()
        {
            List<Triage> triageList = new List<Triage>();
            
            foreach (XmlNode c in _configFile.LastChild.ChildNodes[2])
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
            foreach (XmlNode c in _configFile.LastChild.ChildNodes[3])
            {
                var _specialty = new Specialty();
                _specialty.Name = c.FirstChild.InnerText;
                if (c.LastChild.InnerText != "")
                {
                    _specialty.Colour = c.LastChild.InnerText;
                }
                else
                {
                    _specialty.Colour = "#808080";

                }
                _specialty.Amount = 0;
                _specialty.ShowAs = Visibility.Collapsed;

                specialtiesList.Add(_specialty);
            }

            return specialtiesList;
        }

    }
}
