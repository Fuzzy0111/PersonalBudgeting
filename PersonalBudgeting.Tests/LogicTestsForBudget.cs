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
        List<Income> _listofIncome;
        DAL myDAL;

        [TestFixtureSetUp]
        public void TestSetuptheEnvironment()
        {
            myDAL = new DAL();
            core = new Core();
            _listofIncome = myDAL.retrieveListOfIncome();
        }

        [TestFixtureTearDown]
        public void TestTearDownTheEnvironment()
        {
            core = null;
            _listofIncome = null;
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




    }
}
