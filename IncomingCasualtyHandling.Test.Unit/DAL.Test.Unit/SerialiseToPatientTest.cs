using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL.Models;
using IncomingCasualtyHandling.DAL;
using IncomingCasualtyHandling.DAL.Interface;
using NSubstitute;
using NUnit.Framework;

namespace IncomingCasualtyHandling.Test.Unit.DAL.Test.Unit
{
    [TestFixture]
    public class SerialiseToPatientTest
    {
        #region Arrange

        private ISerializeToPatient _uut;
        

        [SetUp]
        public void Setup()
        {
            _uut = new SerialiseToPatient();
        }

        #endregion

        #region Act and Assert
        // Method_Scenario_Result
       
        [Test]
        public void ReturnPatient_ReceivedCompletePatient_CreatesPatient()
        {

        }

        #endregion






    }
}
