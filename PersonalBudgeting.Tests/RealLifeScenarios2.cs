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
        public void TestScenario2_ExpenditureHigherThanIncome()
        {
            //Add Expenditure
            myBudget.addExpenditure("Electric Guitar",4000,"Pocket Expenses",myBudget.ListOfParticipant);
            //Assert.AreEqual(4220,core.getTotalExpenditure(myBudget.ListOfExpenditure));
            //Assert.AreEqual(0,core.getAmountAvailableForGoalsPerPay(    myBudget.TaxRate,
            //                                                            myBudget.SuperannuationRate,
            //                                                            myBudget.ListOfExpenditure,
            //                                                            myBudget.ListOfIncome,
            //                                                            myBudget.NoOfPaysPerYear
            //                                                        )
            //               );
            core.updateBankAccount( myBudget.SavingsAccount,
                                    myBudget.TaxRate,
                                    myBudget.SuperannuationRate,
                                    myBudget.ListOfExpenditure,
                                    myBudget.ListOfIncome,
                                    myBudget.NoOfPaysPerYear,
                                    myBudget.mainGoal,
                                    _amountForMainGoalPerPay,
                                    myBudget.ListOfWalletTableItem
                                   );
            //Assert.AreEqual(0,myBudget.SavingsAccount.SavingsForGoals);
            Assert.AreEqual(4220,myBudget.SavingsAccount.SavingsForExpenditures);
            //Assert.AreEqual(500,myBudget.SavingsAccount.SavingsForPersonalUse);//todo:Add a fucntion in core to handle situation where Expenditure is higer than income
            //core.getAmountAvailableForGoalsPerPay(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear);

        }
    }
}
