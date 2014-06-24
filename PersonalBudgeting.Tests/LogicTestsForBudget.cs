using NUnit.Framework;
using PersonalBudgeting.BLL;
using PersonalBudgeting.MOCK_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudgeting.Tests
{
    [TestFixture]
    public class TestsLogicOfBudget
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

        [Test]
        public void TestCalculateGrossIncome_EmptyIncomeList_ReturnZero()
        {
            Assert.AreEqual(0, core.getGrossIncome(new List<Income>()));
        }

        #region getGrossIncomePerYear Tests
        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestgetGrossIncomePerYear_NullIncomeList_ReturnException()
        {
            double result = core.getGrossIncomePerYear(null, 5);
        }

        [Test]
        public void TestgetGrossIncomePerYear_EmptyIncomeList_ReturnZero()
        {
            double result = core.getGrossIncomePerYear(new List<Income>(), 5);
            Assert.AreEqual(0, result);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetGrossIncomePerYear_zeroPeriod_ReturnException()
        {
            double result = core.getGrossIncomePerYear(myBudget.ListOfIncome, 0);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetGrossIncomePerYear_NegativePeriod_ReturnException()
        {
            double result = core.getGrossIncomePerYear(myBudget.ListOfIncome, -5);
        }
        #endregion

        #region getTotalExpenditure Tests
        [Test]
        public void TestgetTotalExpenditureWithEmptyList()
        {
            core.getTotalExpenditure(new List<Expenditure>());
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestgetTotalExpenditureWithNullList()
        {
            core.getTotalExpenditure(null);
        }
        #endregion

        #region getTotalExpenditurePerYear Tests
        [Test]
        public void TestGetTotalExpenditurePerYearForEmptyList()
        {
            Assert.AreEqual(0, core.getTotalExpenditurePerYear(new List<Expenditure>()));
        }

        [Test]
        public void TestGetTotalExpenditurePerYear()
        {
            Assert.AreEqual(220.0 * 12, core.getTotalExpenditurePerYear(myBudget.ListOfExpenditure));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestGetTotalExpenditurePerYearWithNullList()
        {
            double result = core.getTotalExpenditurePerYear(null);
        }

        [Test]
        public void TestGetTotalExpenditurePerYearForEmptyListUsingNoOfPays()
        {
            Assert.AreEqual(0, core.getTotalExpenditurePerYear(new List<Expenditure>(), 26));
        }

        [Test]
        public void TestGetTotalExpenditurePerYearUsingNoOfPays()
        {
            Assert.AreEqual(220.0 * 26, core.getTotalExpenditurePerYear(myBudget.ListOfExpenditure, 26));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestGetTotalExpenditurePerYearWithNullListUsingNoOfPays()
        {
            double result = core.getTotalExpenditurePerYear(null, 26);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestGetTotalExpenditurePerYearWithNoOfPaysEqualsZero()
        {
            double result = core.getTotalExpenditurePerYear(myBudget.ListOfExpenditure, 0);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestGetTotalExpenditurePerYearWithNegativeNoOfPays()
        {
            double result = core.getTotalExpenditurePerYear(myBudget.ListOfExpenditure, -5);
        }
        #endregion

        #region getNetIncomePerYear Tests
        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetNetIncomePerYearNegativeTaxRate()
        {
            core.getNetIncomePerYear(-0.15F, 0.2F, myBudget.ListOfIncome, 12);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetNetIncomePerYearNegativeSuperannuationRate()
        {
            core.getNetIncomePerYear(0.15F, -0.2F, myBudget.ListOfIncome, 12);
        }

        [Test]
        public void TestgetNetIncomePerYearEmptyList()
        {
            Assert.AreEqual(0, core.getNetIncomePerYear(0.15F, 0.2F, new List<Income>(), 12));
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetNetIncomePerYearNegativePayPerYear()
        {
            core.getNetIncomePerYear(-0.15F, 0.2F, myBudget.ListOfIncome, -5);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestgetNetIncomePerYearNullIncomeList()
        {
            core.getNetIncomePerYear(0.15F, 0.2F, null, 12);
        }
        #endregion

        #region getAmountAvailableForGoalsPerYear Test
        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetAmountAvailableForGoalsPerYearInvalidTaxRate()
        {
            core.getAmountAvailableForGoalsPerYear(-5F, 0.2F, myBudget.ListOfExpenditure, myBudget.ListOfIncome, 26);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetAmountAvailableForGoalsPerYearInvalidSuperannuationRate()
        {
            core.getAmountAvailableForGoalsPerYear(myBudget.TaxRate, -0.2F, myBudget.ListOfExpenditure, myBudget.ListOfIncome, 26);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestgetAmountAvailableForGoalsPerYearNullExpenditureList()
        {
            core.getAmountAvailableForGoalsPerYear(myBudget.TaxRate, myBudget.SuperannuationRate, null, myBudget.ListOfIncome, 26);
        }

        [Test]
        public void TestgetAmountAvailableForGoalsPerYearEmptyExpenditureList()
        {
            core.getAmountAvailableForGoalsPerYear(myBudget.TaxRate, myBudget.SuperannuationRate, new List<Expenditure>(), myBudget.ListOfIncome, 26);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestgetAmountAvailableForGoalsPerYearNullIncomeList()
        {
            core.getAmountAvailableForGoalsPerYear(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, null, 26);
        }

        [Test]
        public void TestgetAmountAvailableForGoalsPerYearEmptyIncomeList()
        {
            core.getAmountAvailableForGoalsPerYear(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, new List<Income>(), 26);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetAmountAvailableForGoalsPerYearZeroPayPeriods()
        {
            core.getAmountAvailableForGoalsPerYear(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, 0);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetAmountAvailableForGoalsPerYearNegativePayPeriods()
        {
            core.getAmountAvailableForGoalsPerYear(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, -5);
        }
        #endregion

        #region getNoOfPaysRequiredToAccomplishGoal Tests
        [Test, ExpectedException(typeof(DivideByZeroException))]
        public void TestgetNoOfPaysRequiredToAccomplishGoalDivisionbyZero()
        {
            int numPay = core.getNoOfPaysRequiredToAccomplishGoal(myBudget.mainGoal.Cost, 0);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestgetNoOfPaysRequiredToAccomplishGoalAmountPerPayNegative()
        {
            int numPay = core.getNoOfPaysRequiredToAccomplishGoal(myBudget.mainGoal.Cost, -20);
        }
        #endregion

        #region goalPayableBeforeDeadline Tests
        [Test]
        public void TestgoalPayableBeforeDeadline_1()
        {
            Assert.AreEqual(true, core.goalPayableBeforeDeadline(myBudget.mainGoal.Cost, _amountForMainGoalPerPay, myBudget.NoOfPaysPerYear));
        }

        [Test]
        public void TestgoalPayableBeforeDeadline_2()
        {
            Assert.AreEqual(false, core.goalPayableBeforeDeadline(myBudget.mainGoal.Cost, _amountForMainGoalPerPay, 15));
        }

        [Test,ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestgoalPayableBeforeDeadlineDesiredAmountOutOfRange()
        {
            Boolean Payable = core.goalPayableBeforeDeadline(myBudget.mainGoal.Cost, _amountForMainGoalPerPay, -30);
        }
        #endregion

        #region getAmountAvailableForGoalsPerPay Tests
        [Test, ExpectedException(typeof(DivideByZeroException))]
        public void TestgetAmountAvailableForGoalsPerPayNumberOFPayequalzero()
        {
            double AmountAvailableForGoalsPerPay = core.getAmountAvailableForGoalsPerPay(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, 0);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestgetAmountAvailableForGoalsPerPayNumberOFPaylessthanzero()
        {
            double AmountAvailableForGoalsPerPay = core.getAmountAvailableForGoalsPerPay(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, -5);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestgetAmountAvailableForGoalsPerPayListOfIncomeEmpty()
        {
            double AmountAvailableForGoalsPerPay = core.getAmountAvailableForGoalsPerPay(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, null, myBudget.NoOfPaysPerYear);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestgetAmountAvailableForGoalsPerPayListOfExpenditureEmpty()
        {
            double AmountAvailableForGoalsPerPay = core.getAmountAvailableForGoalsPerPay(myBudget.TaxRate, myBudget.SuperannuationRate, null, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear);
        }
        #endregion

        #region  getRemainingAmountForSecondaryGoalsPerPay Tests
        [Test, ExpectedException(typeof(DivideByZeroException))]
        public void TestgetRemainingAmountForSecondaryGoalsPerPayNumberOFPayequalzero()
        {
            double RemainingAmountForSecondaryGoalsPerPay = core.getRemainingAmountForSecondaryGoalsPerPay(_amountForMainGoalPerPay, myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, 0);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestgetRemainingAmountForSecondaryGoalsPerPayNumberOFPaylessthanzero()
        {
            double RemainingAmountForSecondaryGoalsPerPay = core.getRemainingAmountForSecondaryGoalsPerPay(_amountForMainGoalPerPay, myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, -5);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestgetRemainingAmountForSecondaryGoalsPerPayListOfIncomeNull()
        {
            double RemainingAmountForSecondaryGoalsPerPay = core.getRemainingAmountForSecondaryGoalsPerPay(_amountForMainGoalPerPay, myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, null, myBudget.NoOfPaysPerYear);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestgetRemainingAmountForSecondaryGoalsPerPayListOfExpenditureNull()
        {
            double RemainingAmountForSecondaryGoalsPerPay = core.getRemainingAmountForSecondaryGoalsPerPay(_amountForMainGoalPerPay, myBudget.TaxRate, myBudget.SuperannuationRate, null, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear);
        }
        #endregion

        #region WithdrawFromSavingsAccount Tests
        [Test,ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestWithdrawFromSavingsAccountGreaterThanAmountToRemove()
        {
            core.withdrawFromSavingsAccount(myBudget.SavingsAccount, 700);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestNullSavingsAccount()
        {
            core.withdrawFromSavingsAccount(null, 300);
        }
        #endregion

        /*#region creditAmountAvailableForGoalsPerPayInSavingsAccount Tests
        [Test,ExpectedException(typeof(ArgumentNullException))]
        public void TestcreditAmountAvailableForGoalsPerPayInSavingsAccountListOfExpenditureNull()
        {
            core.creditAmountAvailableForGoalsPerPayInSavingsAccount(myBudget.SavingsAccount,myBudget.TaxRate,myBudget.SuperannuationRate, null, myBudget.ListOfIncome , myBudget.NoOfPaysPerYear);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestcreditAmountAvailableForGoalsPerPayInSavingsAccountListOfIncomeNull()
        {
            core.creditAmountAvailableForGoalsPerPayInSavingsAccount(myBudget.SavingsAccount, myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, null, myBudget.NoOfPaysPerYear);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestcreditAmountAvailableForGoalsPerPayInSavingsAccountnoOfPaysPerYearZero()
        {
            core.creditAmountAvailableForGoalsPerPayInSavingsAccount(myBudget.SavingsAccount, myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, 0);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestcreditAmountAvailableForGoalsPerPayInSavingsAccountnoOfPaysPerYearLessThanZero()
        {
            core.creditAmountAvailableForGoalsPerPayInSavingsAccount(myBudget.SavingsAccount, myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, -4);
        } 
        #endregion*/

        /* #region updateSavingsAccount Tests
        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateSavingsAccount_1()
        {
            core.updateSavingsAccount(myBudget.SavingsAccount, myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, null, myBudget.NoOfPaysPerYear, myBudget.mainGoal, _amountForMainGoalPerPay, myBudget.ListOfWalletTableItem);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateSavingsAccount_2()
        {
            core.updateSavingsAccount(myBudget.SavingsAccount, myBudget.TaxRate, myBudget.SuperannuationRate, null, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear, myBudget.mainGoal, _amountForMainGoalPerPay, myBudget.ListOfWalletTableItem);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestUpdateSavingsAccount_3()
        {
            core.updateSavingsAccount(myBudget.SavingsAccount, myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome,0, myBudget.mainGoal, _amountForMainGoalPerPay, myBudget.ListOfWalletTableItem);
        }
        #endregion*/
        /*
        #region getCurrentSurplusInSavingsAccount Tests
        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestgetCurrentSurplusInSavingsAccountWithNullSavingsAccount()
        {
            core.getCurrentSurplusInSavingsAccount(null, myBudget.mainGoal, myBudget.ListOfWalletTableItem);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestgetCurrentSurplusInSavingsAccountWithNullMainGoal()
        {
            core.getCurrentSurplusInSavingsAccount(myBudget.SavingsAccount, null, myBudget.ListOfWalletTableItem);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestgetCurrentSurplusInSavingsAccountWithNullListOfWalletTableItem()
        {
            core.getCurrentSurplusInSavingsAccount(myBudget.SavingsAccount, myBudget.mainGoal, null);
        }

        [Test]
        public void TestgetCurrentSurplusInSavingsAccountWithEmptyListOfWalletTableItem()
        {
            core.getCurrentSurplusInSavingsAccount(myBudget.SavingsAccount, myBudget.mainGoal, new List<WalletTableItem>());
        }
        #endregion*/

        #region updateBankAccountTests
        [Test]
        public void TestRemoveFromSavingsForExpenses_SavingsForExpensesIsZero_ReturnsFalse()
        {
            myBudget.SavingsAccount.SavingsForExpenditures = 0;
            Assert.AreEqual(false, core.removeFromSavingForExpenses(myBudget.SavingsAccount, 20));
        }
        [Test]
        public void TestaddToSavingsForExpenses_SaveForExpensesinListOfExpenditure_SavingsForExpenseCorrectValue()
        {
            core.addToSavingsForExpenses(myBudget.SavingsAccount,core.getTotalExpenditure(myBudget.ListOfExpenditure));
            Assert.AreEqual(220,myBudget.SavingsAccount.SavingsForExpenditures);
        }
        [Test]
        public void TestRemoveFromSavingsForExpenses_RemoveAnAmountFromSavingForExpenses_AmountHasbeenRemoved()
        {
            myBudget.SavingsAccount.SavingsForExpenditures = 0;
            core.addToSavingsForExpenses(myBudget.SavingsAccount,core.getTotalExpenditure(myBudget.ListOfExpenditure));
            core.removeFromSavingForExpenses(myBudget.SavingsAccount, 20);
            Assert.AreEqual(200, myBudget.SavingsAccount.SavingsForExpenditures);
        }
        [Test]
        public void TestSaveForMainGoal_AmountForMainGoalPerPayGreaterThanAmountAvailableFroGorals_ReturnsFalse()
        {
            Assert.AreEqual(false, core.saveForMainGoal(myBudget.SavingsAccount, 500000000, myBudget.mainGoal, myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear));
        }
        [Test]
        public void TestAddToSavingsForGoals_AddAnAmountToSavings_AmountisAdded()
        {
            myBudget.SavingsAccount.SavingsForGoals = 0;
            core.addToSavingsForGoals(myBudget.SavingsAccount,1000);
            Assert.AreEqual(1000,myBudget.SavingsAccount.SavingsForGoals);
        }
        [Test]
        public void TestRemoveFromSavingsForGoals_RemoveAnAmountToSavings_AmountRemoved()
        {
            myBudget.SavingsAccount.SavingsForGoals = 0;
            core.addToSavingsForGoals(myBudget.SavingsAccount, 1000);
            core.removeFromSavingForGoals(myBudget.SavingsAccount,500);
            Assert.AreEqual(500,myBudget.SavingsAccount.SavingsForGoals);
        }
        [Test]
        public void TestTickAllWaleltTableItems_TickAllItemsandCheckAmountTicked_CorrectAmountTicked()
        {
            Assert.AreEqual(35,core.tickAllWalletTableItems(myBudget.ListOfWalletTableItem,_amountForMainGoalPerPay,myBudget.TaxRate,myBudget.SuperannuationRate,myBudget.ListOfExpenditure,myBudget.ListOfIncome,myBudget.NoOfPaysPerYear));
        }
        #endregion
    }
}
