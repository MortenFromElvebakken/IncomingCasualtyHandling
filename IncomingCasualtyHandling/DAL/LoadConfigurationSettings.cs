using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IncomingCasualtyHandling.DAL
{
    public class LoadConfigurationSettings
    {
        public string xmlserver;
        public string _server;
        public string _hospital;
        XmlDocument configFile;

        public LoadConfigurationSettings()
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
            }
            //XmlNode configNode
            //Lav noget så det gemmer som en configurations.cs klasse, hvori variablerne gemmes og kan hentes andre steder i applikation

        }

        public string ReturnServerName()
        {
            _server = configFile.LastChild.FirstChild.InnerText; //Ikke pænt, lav noget med indexering og den leder efter navn
            return _server;
        }
        public string ReturnHospitalShortName()
        {
            _hospital = configFile.LastChild.LastChild.InnerText;
            return _hospital;
        }
        //Metode til at indhente triage og speciale

    }
}
