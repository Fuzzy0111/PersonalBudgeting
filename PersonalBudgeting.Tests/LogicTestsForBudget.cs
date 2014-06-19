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
        double amountToWithdraw;
        

        [TestFixtureSetUp]
        public void TestSetuptheEnvironment()
        {
            myBudget = new Budget();
            core = new Core();

            _amountForMainGoalPerPay = core.getMinimumAmountRequiredPerPayToAccomplishGoalBeforeDeadline(myBudget.MainGoal.Cost, myBudget.MainGoal.DurationInNoOfPays);
        }

        [TestFixtureTearDown]
        public void TestTearDownTheEnvironment()
        {
            core = null;
            myBudget = null;
        }

        [Test]
        public void TestCalculateGrossIncomeWithEmptyIncomeList()
        {
            Assert.AreEqual(0, core.getGrossIncome(new List<Income>()));
        }

        #region getGrossIncomePerYear Tests
        [Test, ExpectedException(typeof(NullReferenceException))]
        public void TestgetGrossIncomePerYearWithNullList()
        {
            double result = core.getGrossIncomePerYear(null, 5);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetGrossIncomePerYearWithEmptyList()
        {
            double result = core.getGrossIncomePerYear(new List<Income>(), 5);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetGrossIncomePerYearFor0Period()
        {
            double result = core.getGrossIncomePerYear(myBudget.ListOfIncome, 0);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetGrossIncomePerYearForNegativePeriod()
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

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetNetIncomePerYearEmptyList()
        {
            core.getNetIncomePerYear(0.15F, 0.2F, new List<Income>(), 12);
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

        #region GetAmountAvailableForGoalsPerYear Test
        [Test]
        public void TestGetAmountAvailableForGoalsPerYear()
        {
            Assert.AreEqual(116700.0, core.getAmountAvailableForGoalsPerYear(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, 26), 0.1);
        }
        #endregion

        #region getNoOfPaysRequiredToAccomplishGoal Tests
        [Test, ExpectedException(typeof(DivideByZeroException))]

        public void TestgetNoOfPaysRequiredToAccomplishGoalDivisionbyZero()
        {
            int numPay = core.getNoOfPaysRequiredToAccomplishGoal(myBudget.MainGoal.Cost, 0);

        }
        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]

        public void TestgetNoOfPaysRequiredToAccomplishGoalAmountPerPayNegative()
        {
            int numPay = core.getNoOfPaysRequiredToAccomplishGoal(myBudget.MainGoal.Cost, -20);

        }
        #endregion
        #region goalPayableBeforeDeadline Tests
        [Test]
        public void TestgoalPayableBeforeDeadline_1()
        {
            Assert.AreEqual(true, core.goalPayableBeforeDeadline(myBudget.MainGoal.Cost, _amountForMainGoalPerPay, myBudget.NoOfPaysPerYear));
        }
        [Test]
        public void TestgoalPayableBeforeDeadline_2()
        {
            Assert.AreEqual(false, core.goalPayableBeforeDeadline(myBudget.MainGoal.Cost, _amountForMainGoalPerPay, 40));
        }
        [Test,ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestgoalPayableBeforeDeadlineDesiredAmountOutOfRange()
        {
            Boolean Payable = core.goalPayableBeforeDeadline(myBudget.MainGoal.Cost, _amountForMainGoalPerPay, -30);
        }
        #endregion

        #region getAmountAvailableForGoalsPerPay Test
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


        #region WithdrawFromSavingsAccount Test
        [Test,ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestWithdrawFromSavingsAccountGreaterThanAmountToRemove()
        {
            core.withdrawFromSavingsAccount(myBudget.SavingsAccount, 700);
        }

        [Test, ExpectedException(typeof(NullReferenceException))]
        public void TestNullSavingsAccount()
        {
            core.withdrawFromSavingsAccount(null, 300);
        }
        #endregion
    }
}
