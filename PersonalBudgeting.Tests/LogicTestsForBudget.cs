using NUnit.Framework;
using PersonalBudgeting.BLL;
using PersonalBudgeting.MOCK_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudgeting.Tests
{
    [TestFixture]
    public class TestsLogicOfBudget
    {

        Core core;
        DAL myDAL;
        List<Income> _listofIncome;
        List<Expenditure> _listOfExpenditure;
        MainGoal _mainGoal;
        List<WishlistItem> _listOfWishlistItem;
        float _taxRate;
        float _superannuationRate;
        float _safetyMargin;
        float _mainGoalPercentage;

        [TestFixtureSetUp]
        public void TestSetuptheEnvironment()
        {
            myDAL = new DAL();
            core = new Core();
            _listofIncome = myDAL.retrieveListOfIncome();
            _listOfExpenditure = myDAL.retrieveListOfExpenditure();
            _mainGoal = myDAL.retrieveMainGoal();
            _listOfWishlistItem = myDAL.retrieveListOfWishlistItem();
            _taxRate = myDAL.retrieveTaxRate();
            _superannuationRate = myDAL.retrieveSuperannuationRate();
            _safetyMargin = myDAL.retrieveSafetyMargin();
            _mainGoalPercentage = myDAL.retrieveMainGoalPercentage();
        }

        [TestFixtureTearDown]
        public void TestTearDownTheEnvironment()
        {
            core = null;
            _listofIncome = null;
            _listOfExpenditure = null;
            _mainGoal = null;
            _listOfWishlistItem = null;

        }

        [Test,ExpectedException]
        public void TestCalculateGrossIncomeWithEmptyIncomeList()
        {
            Assert.AreEqual(5100, core.getGrossIncome(new List<Income>()));
        }

        #region getGrossIncomePerYear Tests
        [Test, ExpectedException(typeof(NullReferenceException))]
        public void TestgetGrossIncomePerYearWithNullList()
        {
            double result = core.getGrossIncomePerYear(null, 5);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetGrossIncomePerYearWithEmptyList()
        {
            double result = core.getGrossIncomePerYear(new List<Income>(), 5);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetGrossIncomePerYearFor0Period()
        {
            double result = core.getGrossIncomePerYear(_listofIncome, 0);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetGrossIncomePerYearForNegativePeriod()
        {
            double result = core.getGrossIncomePerYear(_listofIncome, -5);
        }
        #endregion


        [Test]
        public void TestGetTotalExpenditurePerYearForEmptyList()
        {
            Assert.AreEqual(0, core.getTotalExpenditurePerYear(new List<Expenditure>()));
        }

        [Test]
        public void TestGetTotalExpenditurePerYear()
        {
            Assert.AreEqual(220.0 * 12, core.getTotalExpenditurePerYear(_listOfExpenditure));
        }

        [Test, ExpectedException(typeof(NullReferenceException))]
        public void TestGetTotalExpenditurePerYearWithNullList()
        {
            double result = core.getTotalExpenditurePerYear(null);
        }

        [Test]
        public void TestGetTotalExpenditurePerYearForEmptyListUsingNoOfPays()
        {
            Assert.AreEqual(0, core.getTotalExpenditurePerYear(new List<Expenditure>(), 26));
        }

        [Test]
        public void TestGetTotalExpenditurePerYearUsingNoOfPays()
        {
            Assert.AreEqual(220.0 * 26, core.getTotalExpenditurePerYear(_listOfExpenditure, 26));
        }

        [Test, ExpectedException(typeof(NullReferenceException))]
        public void TestGetTotalExpenditurePerYearWithNullListUsingNoOfPays()
        {
            double result = core.getTotalExpenditurePerYear(null, 26);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestGetTotalExpenditurePerYearWithNoOfPaysEqualsZero()
        {
            double result = core.getTotalExpenditurePerYear(_listOfExpenditure, 0);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestGetTotalExpenditurePerYearWithNegativeNoOfPays()
        {
            double result = core.getTotalExpenditurePerYear(_listOfExpenditure, -5);
        }

        [Test]
        public void TestGetAmountAvailableForGoalsPerYear()
        {
            Assert.AreEqual(116700.0, core.getAmountAvailableForGoalsPerYear(_taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, 26));
        }

    }
}
