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
    class IT1_LoadData_LoadConfigurationSettigs
    {
        // Fakes
        private IConvertToICHPatient _serialise;

        // System under test
        private LoadConfigurationSettings _loadConfig;

        // Drivers
        private LoadData _loadData;

        // Included 

        // Data
        private string _xmlDocumentPath;
        private string _xmlServerName = "http://localhost:8080/hapi-fhir-jpaserver-example/baseDstu3";


        [SetUp]
        public void SetUp()
        {
            _serialise = Substitute.For<IConvertToICHPatient>();

            var currentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(
                TestContext.CurrentContext.TestDirectory));
            _xmlDocumentPath = currentDirectory + "\\Configuration.xml";
            _loadConfig = new LoadConfigurationSettings(_xmlDocumentPath);

            _loadData = new LoadData(_loadConfig, _serialise);

        }

        [Test]
        public void LoadDataConstructor_GetServerUrl_ServerUrlReturnedFromLoad()
        {
            var url = _loadData.FhirServerUrl;
            Assert.That(url, Is.EqualTo(_xmlServerName));
            
        }
    }

}