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
        Budget myBudget;
        double _amountForMainGoalPerPay;


        [TestFixtureSetUp]
        public void TestSetuptheEnvironment()
        {
            myBudget = new Budget();
            core = new Core();
            _amountForMainGoalPerPay = core.getMinimumAmountRequiredPerPayToAccomplishGoalBeforeDeadline(myBudget.mainGoal.Cost, myBudget.mainGoal.DurationInNoOfPays);
        }

        [Test]
        public void TestgetGrossIncomePerYearFor12Months()
        {
            Assert.AreEqual(5100 * 12, core.getGrossIncomePerYear(myBudget.ListOfIncome, 12));
        }

        [Test]
        public void TestVerifyTotalIncome()
        {
            Assert.AreEqual(5100, core.getGrossIncome(myBudget.ListOfIncome));
        } 
        
        [Test]
        public void TestgetNetIncomePerYear()
        {
            string msg = String.Format("Tax: {0}, Sup: {1}, TotalGrossIncome: {2}, PaysPerYear: {3}",
                myBudget.TaxRate, myBudget.SuperannuationRate, core.getGrossIncomePerYear(myBudget.ListOfIncome, 12), 12);
            Assert.AreEqual(55080, core.getNetIncomePerYear(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfIncome, 12), 0.1, msg);
        }

        [Test]
        public void TestGetTotalExpenditure()
        {
            Assert.AreEqual(220, core.getTotalExpenditure(myBudget.ListOfExpenditure));
        }

        [Test]
        public void TestGetTotalExpenditurePerYear()
        {
            Assert.AreEqual(220.0 * 12, core.getTotalExpenditurePerYear(myBudget.ListOfExpenditure));
        }

        [Test]
        public void TestGetAmountAvailableForGoalsPerYear()
        {
            Assert.AreEqual(116700.0, core.getAmountAvailableForGoalsPerYear(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, 26), 0.1);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestTickAllWalletTableItemsEmptyList()
        {

            core.tickAllWalletTableItems(myBudget.SavingsAccount,null, _amountForMainGoalPerPay, myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, 26);
        }

        [TestFixtureTearDown]
        public void TestTearDownTheEnvironment()
        {
            core = null;
            myBudget = null; 
        }

    }
}
