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
    public class TestsAccuracyOfDALValues
    {
        Core core;
        DAL myDAL;
        List<Income> _listofIncome;
        float _taxRate, _superannuationRate;

        [TestFixtureSetUp]
        public void TestSetuptheEnvironment()
        {
            myDAL = new DAL();
            core = new Core();
            _listofIncome = myDAL.retrieveListOfIncome();
            _taxRate = myDAL.retrieveTaxRate();
            _superannuationRate = myDAL.retrieveSuperannuationRate();
        }

        [Test]
        public void TestgetGrossIncomePerYearFor12Months()
        {
            Assert.AreEqual(5100 * 12, core.getGrossIncomePerYear(_listofIncome, 12));
        }

        [Test]
        public void TestVerifyTotalIncome()
        {
            Assert.AreEqual(5100, core.getGrossIncome(_listofIncome));
        } 
        
        [Test]
        public void TestgetNetIncomePerYear()
        {
            string msg = String.Format("Tax: {0}, Sup: {1}, TotalGrossIncome: {2}, PaysPerYear: {3}",
                _taxRate, _superannuationRate, core.getGrossIncomePerYear(_listofIncome, 12), 12);
            Assert.AreEqual(55080, core.getNetIncomePerYear(_taxRate, _superannuationRate, _listofIncome, 12), 0.1, msg);
        }

        [TestFixtureTearDown]
        public void TestTearDownTheEnvironment()
        {
            core = null;
            _listofIncome = null;

        }

    }
}
