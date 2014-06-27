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
    class RealLifeScenario1
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
        public void TestCalculateBudget_NormalScenario()
        {

            core.getGrossIncome(myBudget.ListOfIncome);
            core.getGrossIncomePerYear(myBudget.ListOfIncome, myBudget.NoOfPaysPerYear);
            core.getTotalExpenditurePerYear(myBudget.ListOfExpenditure, myBudget.NoOfPaysPerYear);
            core.getNetIncomePerYear(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear);
            core.getAmountAvailableForGoalsPerYear(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear);

            WalletTableItem wti = new WalletTableItem("Bag", "Leather bag", 500.0, 100, 100);
            myBudget.ListOfWalletTableItem.Add(wti);
            core.updateBankAccount(myBudget.SavingsAccount, myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear, myBudget.mainGoal, _amountForMainGoalPerPay, myBudget.ListOfWalletTableItem);
            // Assert.AreEqual(519.6, myBudget.SavingsAccount.SavingsForGoals, 0.1);
            // Assert.AreEqual(220, myBudget.SavingsAccount.SavingsForExpenditures);
            Assert.AreEqual(3840.4, myBudget.SavingsAccount.SavingsForPersonalUse, 0.1);
        }
    }
}
