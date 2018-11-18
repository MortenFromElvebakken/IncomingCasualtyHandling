using IncomingCasualtyHandling.BL;
using NSubstitute;
using NUnit.Framework;

namespace IncomingCasualtyHandling.Test.Unit.BL.Test.Unit
{
    
    // Arrange - Act - Assert

    [TestFixture]
    public class SortSpecialtyTest
    {
        #region Arrange
        
        //private ISortETA sortEta;
        private SortETA _sortEta;
        //private ISortSpecialty sortSpecialty;
        private SortSpecialty _sortSpecialty;
        //private ISortTriage sortTriage;
        private SortTriage _sortTriage;


        [SetUp]
        public void Setup()
        {
            //sortEta = Substitute.For<ISortETA>();
            _sortEta = Substitute.For<SortETA>();
            //sortSpecialty = Substitute.For<ISortSpecialty>();
            _sortSpecialty = Substitute.For<SortSpecialty>();
            //sortTriage = Substitute.For<ISortTriage>();
            _sortTriage = Substitute.For<SortTriage>();
        }

        #endregion

        #region Act and Assert
        // Metode_Scenarie_Resultat

        // Test a list with patients
        [Test]
        public void Update_ListWithPatients_CallSortMethods()
        {

        }

        // Test an empty list
        [Test]
        public void Update_ListWithNoPatients_NoCallToSortMethods()
        {

        }

        #endregion






    }
}
