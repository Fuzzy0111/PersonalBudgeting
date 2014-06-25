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
        MockDALScenario1 mck;
        
        double _amountForMainGoalPerPay;

        [TestFixtureSetUp]
        public void TestSetUpEnvironment()
        {
            mck = new MockDALScenario1();
            Core core = new Core();
            myBudget = new Budget();

            _amountForMainGoalPerPay = 384.6;
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
              
            myBudget.ListOfIncome=mck.retrieveListOfIncome();
            myBudget.ListOfExpenditure=mck.retrieveListOfExpenditure();
            myBudget.ListOfWalletTableItem=mck.retrieveListOfWalletTableItem();
            myBudget.mainGoal = mck.retrieveMainGoal();

            myBudget.TaxRate = mck.retrieveTaxRate();
            myBudget.SuperannuationRate = mck.retrieveSuperannuationRate();

            myBudget.SavingsAccount = mck.retrieveSavingsAccount();
            myBudget.NoOfPaysPerYear = mck.retrieveNoOfPaysPerYear();

            core.updateBankAccount(myBudget.SavingsAccount,myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear, myBudget.mainGoal, _amountForMainGoalPerPay,myBudget.ListOfWalletTableItem);

            Assert.AreEqual(2307.7,core.getGrossIncome(myBudget.ListOfIncome));
           // Assert.AreEqual(60000.2, core.getGrossIncomePerYear(myBudget.ListOfIncome, myBudget.NoOfPaysPerYear));
            //Assert.AreEqual(12120.0, core.getTotalExpenditurePerYear(myBudget.ListOfExpenditure));
            //Assert.AreEqual(48000.16, core.getNetIncomePerYear(_taxRate,_superannuationRate,myBudget.ListOfIncome,myBudget.NoOfPaysPerYear));
            //Assert.AreEqual(47880.2, core.getAmountAvailableForGoalsPerYear(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear));
        }
    }
}
