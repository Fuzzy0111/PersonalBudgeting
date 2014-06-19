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
        List<Expenditure> _listOfExpenditure;
        float _taxRate, _superannuationRate;
        double amountForMainGoalPerPay;


        [TestFixtureSetUp]
        public void TestSetuptheEnvironment()
        {
            myDAL = new DAL();
            core = new Core();
            _listofIncome = myDAL.retrieveListOfIncome();
            _listOfExpenditure = myDAL.retrieveListOfExpenditure();
            _taxRate = myDAL.retrieveTaxRate(core.getGrossIncome(_listofIncome));
            _superannuationRate = myDAL.retrieveSuperannuationRate();
            amountForMainGoalPerPay = 2000;
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

        [Test]
        public void TestGetTotalExpenditure()
        {
            Assert.AreEqual(220, core.getTotalExpenditure(_listOfExpenditure));
        }

        [Test]
        public void TestGetTotalExpenditurePerYear()
        {
            Assert.AreEqual(220.0 * 12, core.getTotalExpenditurePerYear(_listOfExpenditure));
        }

        [Test]
        public void TestGetAmountAvailableForGoalsPerYear()
        {
            Assert.AreEqual(116700.0, core.getAmountAvailableForGoalsPerYear(_taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, 26), 0.1);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestTickOffAllWalletTableItemsEmptyList()
        {

            core.tickOffAllWalletTableItems(null, amountForMainGoalPerPay, _taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, 26);
        }

        [TestFixtureTearDown]
        public void TestTearDownTheEnvironment()
        {
            core = null;
            _listofIncome = null;

        }

    }
}
