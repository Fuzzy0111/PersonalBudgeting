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
        public void AddIncomeForTwoCasualWorkers()
        {
            Assert.AreEqual(0,myBudget.SavingsAccount.SavingsForGoals);
            Assert.AreEqual(500,myBudget.SavingsAccount.SavingsForPersonalUse);
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
            Assert.AreEqual(220, myBudget.SavingsAccount.SavingsForExpenditures);
            Assert.AreEqual(3940.4,myBudget.SavingsAccount.SavingsForPersonalUse,0.1);
            core.getAmountAvailableForGoalsPerPay(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear);
            core.addIncomeForCasualWorker(myBudget, "salary", new Participant("Quannah", "Parker"), 300);
            Assert.AreEqual(4240.4,myBudget.SavingsAccount.SavingsForPersonalUse,0.1);
          

        }
    }
}
