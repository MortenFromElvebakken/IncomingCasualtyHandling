using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomingCasualtyHandling.BL;
using IncomingCasualtyHandling.BL.Models;
using NSubstitute;
using NUnit.Framework;

namespace IncomingCasualtyHandling.Test.Unit.BL.Test.Unit
{
   
    [TestFixture]
    public class SortETATest
    {
        #region Arrange

        //private ISortETA _uut;
        private SortETA _uut;
        //private IOverviewViewModel _overviewViewModel;
        private OverviewView_Model _overviewViewModel;
        //private IDetailViewModel _detailViewModel;
        private DetailView_Model _detailViewModel;

        [SetUp]
        public void Setup()
        {
            _overviewViewModel = Substitute.For<OverviewView_Model>();
            _detailViewModel = Substitute.For<DetailView_Model>();
            //_uut = new SortETA(_overviewViewModel, _detailViewModel);
        }

        #endregion

        #region Act and Assert
        // Metode_Scenarie_Resultat

        // Test an empty list
        [Test]
        public void SortForETA_ListWithPatients_CreateETAObject()
        {

        }

        #endregion






    }
}
