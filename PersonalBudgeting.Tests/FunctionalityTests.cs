using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalBudgeting.BLL;
using PersonalBudgeting.MOCK_DAL;
using NUnit.Framework;

namespace PersonalBudgeting.Tests
{
    [TestFixture]
    class FunctionalityTests
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
        public void TestForThreePays()
        {
            core.updateSavingsAccount(myBudget.SavingsAccount, myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear, myBudget.mainGoal, _amountForMainGoalPerPay, myBudget.ListOfWalletTableItem);
            core.updateSavingsAccount(myBudget.SavingsAccount, myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear, myBudget.mainGoal, _amountForMainGoalPerPay, myBudget.ListOfWalletTableItem);
            core.updateSavingsAccount(myBudget.SavingsAccount, myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear, myBudget.mainGoal, _amountForMainGoalPerPay, myBudget.ListOfWalletTableItem);

            core.withdrawFromSavingsAccount(myBudget.SavingsAccount, 500);
            core.withdrawFromSavingsAccount(myBudget.SavingsAccount, 50);
            core.withdrawFromSavingsAccount(myBudget.SavingsAccount, 125);
            core.withdrawFromSavingsAccount(myBudget.SavingsAccount, 100);

            Assert.AreEqual(13085.3, myBudget.SavingsAccount.AmountAvailable, 0.1);
        }
    }
}