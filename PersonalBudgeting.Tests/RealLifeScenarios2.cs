using NUnit.Framework;
using PersonalBudgeting.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudgeting.Tests
{
    [TestFixture]
    class RealLifeScenarios2
    {
        Core core;
        Budget myBudget;
        double _amountForMainGoalPerPay;

        [TestFixtureSetUp]
        public void TestSetuptheEnvironment()
        {
            core = new Core();
            myBudget = new Budget();
            _amountForMainGoalPerPay = core.getMinimumAmountRequiredPerPayToAccomplishGoalBeforeDeadline(myBudget.mainGoal.Cost,myBudget.mainGoal.DurationInNoOfPays);
        }
        [TestFixtureTearDown]
        public void TestTearDownTheEnvironment()
        {
            core = null;
            myBudget = null;
        }
        [Test]
        public void TestScenario2_TotalExpenditureHigherThanNetIncome()
        {
            //Add an expenditure
            myBudget.addExpenditure("Electric Guitar", 4000, "Pocket Expenses", myBudget.ListOfParticipant);
            //Assert.AreEqual(500,myBudget.SavingsAccount.SavingsForPersonalUse);
            core.addToSavingsForPersonalUse(myBudget.SavingsAccount, 500);
            Assert.AreEqual(1000, myBudget.SavingsAccount.SavingsForPersonalUse);

            //Assert.AreEqual(4220,core.getTotalExpenditure(myBudget.ListOfExpenditure));
            //Assert.AreEqual(5100,core.getGrossIncome(myBudget.ListOfIncome));
           // core.updateBankAccount(myBudget.SavingsAccount, myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear, myBudget.mainGoal, _amountForMainGoalPerPay, myBudget.ListOfWalletTableItem);
            //Assert.AreEqual();
        }
    }
}
