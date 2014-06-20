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
        public void TestForThreePaysAndFourWithdrawals()
        {
            //updateSavingsAccount has been called 3 times for 3 pays

            core.updateSavingsAccount(myBudget.SavingsAccount, myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear, myBudget.mainGoal, _amountForMainGoalPerPay, myBudget.ListOfWalletTableItem);
            core.updateSavingsAccount(myBudget.SavingsAccount, myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear, myBudget.mainGoal, _amountForMainGoalPerPay, myBudget.ListOfWalletTableItem);
            core.updateSavingsAccount(myBudget.SavingsAccount, myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear, myBudget.mainGoal, _amountForMainGoalPerPay, myBudget.ListOfWalletTableItem);

            //withdrawFromSavingsAccount has been called 4 times for 4 withdrawals from savings account
            core.withdrawFromSavingsAccount(myBudget.SavingsAccount, 500);
            core.withdrawFromSavingsAccount(myBudget.SavingsAccount, 50);
            core.withdrawFromSavingsAccount(myBudget.SavingsAccount, 125);
            core.withdrawFromSavingsAccount(myBudget.SavingsAccount, 100);

            //initialSavingsAccountBalance=500.00
            //AmountAvailableForGoalsPerPay=116700.0/26 = 4488.46
            //for 3 pays = 4488.46*3=13465.38
            
            //amountForMainGoalPerPay=150000/26 = 5769.23
            //amountForWalletTablePerPay=35.00

            //4488.46
            //totalAmountForWithdrawals=775.00
            //savingsAccount.AmountAvailable=13190.3
            Assert.AreEqual(13190.3, core.getCurrentSurplusInSavingsAccount(myBudget.SavingsAccount, myBudget.mainGoal, myBudget.ListOfWalletTableItem), 0.1);
        }
    }
}