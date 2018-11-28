using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Hl7.Fhir.Model;
using IncomingCasualtyHandling.DAL;
using IncomingCasualtyHandling.DAL.Interface;
using NSubstitute;
using NUnit.Framework;

namespace IncomingCasualtyHandling.Test.Integration
{
    [TestFixture]
    class IT1_GetPatients_LoadConfig
    {
        // Fakes
        private ISerializeToPatient _serialise;

        // System under test
        private ILoadConfigurationSettings _loadConfig;

        // Drivers
        private GetPatientsFromFhir _getPatients;

        // Included 

        // Data
        private string _xmlDocumentPath;
        private string _xmlServerName = "http://localhost:8080/hapi-fhir-jpaserver-example/baseDstu3";


        [SetUp]
        public void SetUp()
        {
            _serialise = Substitute.For<ISerializeToPatient>();

            _xmlDocumentPath =
                "E://Visual Studio 2017//BAC//IncomingCasualtyHandling.Test.Integration//Configuration.xml";
            _loadConfig = new LoadConfigurationSettingsFromXMLDocument(_xmlDocumentPath);

            _getPatients = new GetPatientsFromFhir(_loadConfig, _serialise);

        }

        [Test]
        public void GetPatientsFromFhirConstructor_GetServerUrl_ServerUrlReturnedFromLoad()
        {
            var url = _getPatients.GetServerUrl();
            Assert.That(url, Is.EqualTo(_xmlServerName));
            
        }
    }

}