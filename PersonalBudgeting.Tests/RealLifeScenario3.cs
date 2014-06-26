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
    class RealLifeScenario3
    {
        Core core;
        Budget myBudget;
        double _amountForMainGoalPerPay;

        [TestFixtureSetUp]
        public void TestSetuptheEnvironment()
        {
            core = new Core();
            myBudget = new Budget();
            _amountForMainGoalPerPay = core.getMinimumAmountRequiredPerPayToAccomplishGoalBeforeDeadline(myBudget.mainGoal.Cost, myBudget.mainGoal.DurationInNoOfPays);
        }
        [TestFixtureTearDown]
        public void TestTearDownTheEnvironment()
        {
            core = null;
            myBudget = null;
        }
        #region TICK OFF WALLET TABLE ITEM
        [Test]
        public void TesttickOffWalletTable_TickOffAnItem()
        {
            myBudget.ListOfWalletTableItem.First().Cost = 20;
            core.updateBankAccount(myBudget.SavingsAccount,
                                   myBudget.TaxRate,
                                   myBudget.SuperannuationRate,
                                   myBudget.ListOfExpenditure,
                                   myBudget.ListOfIncome,
                                   myBudget.NoOfPaysPerYear,
                                   myBudget.mainGoal,
                                   _amountForMainGoalPerPay,
                                   myBudget.ListOfWalletTableItem);
            core.tickWalletTableItem(myBudget.ListOfWalletTableItem.First());
            core.tickOffWalletTableItem(myBudget, myBudget.ListOfWalletTableItem.First());
            Assert.AreEqual(399.6, myBudget.SavingsAccount.SavingsForGoals, 0.1);

        }
        #endregion
    }
}
