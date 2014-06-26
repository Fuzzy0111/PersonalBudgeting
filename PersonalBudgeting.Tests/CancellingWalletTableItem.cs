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
    class CancellingWalletTableItemTest
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

        [TestFixtureTearDown]
        public void TestTearDownTheEnvironment()
        {
            core = null;
            myBudget = null;
        }
        #region Wallet Table Items test
        [Test]
        public void TestCancelWalletTableItem_CancelanItem_SavingsForPersonalUse_SavingsForPersonalUseUpdated()
        {
            WalletTableItem wti = new WalletTableItem("Bag", "Leather bag", 500.0, 0.0, 100);
            myBudget.SavingsAccount.SavingsForPersonalUse = 500;
            myBudget.ListOfWalletTableItem.Add(wti);
            core.updateBankAccount(myBudget.SavingsAccount,
                                   myBudget.TaxRate,
                                   myBudget.SuperannuationRate,
                                   myBudget.ListOfExpenditure,
                                   myBudget.ListOfIncome,
                                   myBudget.NoOfPaysPerYear,
                                   myBudget.mainGoal,
                                   _amountForMainGoalPerPay,
                                   myBudget.ListOfWalletTableItem
                                   );
            //Assert.AreEqual(3840.4,myBudget.SavingsAccount.SavingsForPersonalUse);
            core.CancelWalletTableItem(myBudget, wti);
            Assert.AreEqual(3940.4, myBudget.SavingsAccount.SavingsForPersonalUse, 0.1);
        }

        [Test]
        public void TestCancelWalletTableItem_CancelanItem_SavingsForGoals_SavingsForGoalsUpdated()
        {

            WalletTableItem wti = new WalletTableItem("Bag", "Leather bag", 500.0, 0.0, 100);
            myBudget.ListOfWalletTableItem.Add(wti);
            
            core.updateBankAccount(myBudget.SavingsAccount,
                                   myBudget.TaxRate,
                                   myBudget.SuperannuationRate,
                                   myBudget.ListOfExpenditure,
                                   myBudget.ListOfIncome,
                                   myBudget.NoOfPaysPerYear,
                                   myBudget.mainGoal,
                                   _amountForMainGoalPerPay,
                                   myBudget.ListOfWalletTableItem
                                   );
            //Assert.AreEqual(519.4, myBudget.SavingsAccount.SavingsForGoals);
            core.CancelWalletTableItem(myBudget, wti);
            Assert.AreEqual(419.6, myBudget.SavingsAccount.SavingsForGoals, 0.1);
        }
        #endregion
    }
}
