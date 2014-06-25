using NUnit.Framework;
using PersonalBudgeting.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalBudgeting.MOCK_DAL;
namespace PersonalBudgeting.Tests
{
    [TestFixture]
    class RealLifeScenariosTests
    {
        Core core;
        Budget myBudget;


        double _amountForMainGoalPerPay;

        [TestFixtureSetUp]
        public void TestSetUpEnvironment()
        {

            core = new Core();
            myBudget = new Budget();

            _amountForMainGoalPerPay = core.getMinimumAmountRequiredPerPayToAccomplishGoalBeforeDeadline(myBudget.mainGoal.Cost, myBudget.mainGoal.DurationInNoOfPays);

        }

        [TestFixtureTearDown]
        public void TestTearDown()
        {
            core = null;
            myBudget = null;
        }

        [Test]
        public void TestCalculateBudget_NormalScenario_Successful()
        {

            Assert.AreEqual(5100, core.getGrossIncome(myBudget.ListOfIncome));
            Assert.AreEqual(132600, core.getGrossIncomePerYear(myBudget.ListOfIncome, myBudget.NoOfPaysPerYear));
            Assert.AreEqual(5720, core.getTotalExpenditurePerYear(myBudget.ListOfExpenditure, myBudget.NoOfPaysPerYear));
            Assert.AreEqual(106080, core.getNetIncomePerYear(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear), 0.1);
            Assert.AreEqual(100360, core.getAmountAvailableForGoalsPerYear(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear), 0.1);
            //Assert.AreEqual(true,core.saveForMainGoal(myBudget.SavingsAccount,_amountForMainGoalPerPay,myBudget.mainGoal,myBudget.TaxRate,myBudget.SuperannuationRate,myBudget.ListOfExpenditure,myBudget.ListOfIncome,myBudget.NoOfPaysPerYear));
            Assert.AreEqual(100,core.getNoOfPaysRequiredToAccomplishGoal(10000,100));

        }

        [Test]
        public void Test()
        {

        }
    }
}

