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
        public void TestCalculateNoOfPaysRequiredToAccomplishGoal_NormalScenario_Successful()
        {

            Assert.AreEqual(5100, core.getGrossIncome(myBudget.ListOfIncome));
            Assert.AreEqual(132600, core.getGrossIncomePerYear(myBudget.ListOfIncome, myBudget.NoOfPaysPerYear));
            Assert.AreEqual(5720, core.getTotalExpenditurePerYear(myBudget.ListOfExpenditure, myBudget.NoOfPaysPerYear));
            Assert.AreEqual(106080, core.getNetIncomePerYear(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear), 0.1);
            Assert.AreEqual(100360, core.getAmountAvailableForGoalsPerYear(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear), 0.1);
            
            Assert.AreEqual(10,core.getNoOfPaysRequiredToAccomplishGoal(10000,1000));

        }

        [Test]
        public void TestCalculateBudget_NormalScenario_Successful()
        {

            core.getGrossIncome(myBudget.ListOfIncome);
            core.getGrossIncomePerYear(myBudget.ListOfIncome, myBudget.NoOfPaysPerYear);
            core.getTotalExpenditurePerYear(myBudget.ListOfExpenditure, myBudget.NoOfPaysPerYear);
            core.getNetIncomePerYear(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear);
            core.getAmountAvailableForGoalsPerYear(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear);
           
            WalletTableItem wti = new WalletTableItem("Bag", "Leather bag", 500.0, 100, 100);
            myBudget.ListOfWalletTableItem.Add(wti);
            core.updateBankAccount(myBudget.SavingsAccount,myBudget.TaxRate,myBudget.SuperannuationRate,myBudget.ListOfExpenditure,myBudget.ListOfIncome,myBudget.NoOfPaysPerYear,myBudget.mainGoal,_amountForMainGoalPerPay,myBudget.ListOfWalletTableItem);
            Assert.AreEqual(519.6, myBudget.SavingsAccount.SavingsForGoals, 0.1);
            Assert.AreEqual(220, myBudget.SavingsAccount.SavingsForExpenditures);
            Assert.AreEqual(3840.4, myBudget.SavingsAccount.SavingsForPersonalUse, 0.1);
        }

        [Test]
        public void TestAmountSavedForWalletItem_CorrectAmount()
        {
            
            WalletTableItem wti = new WalletTableItem("Bag", "Leather bag", 500.0, 0.0, 100);
            myBudget.ListOfWalletTableItem.Add(wti);
            core.tickWalletTableItem(wti);
            core.tickWalletTableItem(wti);
            core.tickWalletTableItem(wti);
            core.tickWalletTableItem(wti);
            core.tickWalletTableItem(wti);
            Assert.AreEqual(500,wti.AmountSaved);

        }
    }
}

