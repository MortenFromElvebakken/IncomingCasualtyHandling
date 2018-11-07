using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using Hl7.Fhir.ElementModel;
using IncomingCasualtyHandling.BL.Models;

namespace IncomingCasualtyHandling.DAL
{
    public class LoadConfigurationSettingsFromXMLDocument
    {
        private string xmlserver;
        private string _server;
        private string _hospital;
        private XmlDocument configFile;

        public LoadConfigurationSettingsFromXMLDocument()
        {
            xmlserver = "http://localhost:8080/Conf.Fapi/Configuration.xml";
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
            //XmlNode configNode
            //Lav noget så det gemmer som en configurations.cs klasse, hvori variablerne gemmes og kan hentes andre steder i applikation
            ReturnTriageList();
            ReturnSpecialtyList();

        }

        public string ReturnServerName()
        {
            _server = configFile.LastChild.ChildNodes[0].InnerText; //Ikke pænt, lav noget med indexering og den leder efter navn
            return _server;
        }
        public string ReturnHospitalShortName()
        {
            _hospital = configFile.LastChild.ChildNodes[1].InnerText;
            return _hospital;
        }

        public void ReturnTriageList()
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
            
        }

        public void ReturnSpecialtyList()
        {
            List<Specialty> _specialtiesList = new List<Specialty>();
            foreach (XmlNode c in configFile.LastChild.ChildNodes[3])
            {
                var _specialty = new Specialty();
                _specialty.Name = c.FirstChild.InnerText;
                //_specialty.Colour = c.LastChild.InnerText; //Not yet implemented since colour isnt known on the specialties
                _specialty.Amount = 0;

                _specialtiesList.Add(_specialty);
            }

            //return _specialtiesList;
        }

    }
}
