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
        DAL myDAL;
        List<Income> _listofIncome;
        List<Expenditure> _listOfExpenditure;
        MainGoal _mainGoal;
        List<WalletTableItem> _listOfWishlistItem;
        float _taxRate;
        float _superannuationRate;
        float _safetyMargin;
        float _mainGoalPercentage;
        double goalCost;
        double amountPerPayForMainGoal;

        [TestFixtureSetUp]
        public void TestSetuptheEnvironment()
        {
            myDAL = new DAL();
            core = new Core();
            _listofIncome = myDAL.retrieveListOfIncome();
            _listOfExpenditure = myDAL.retrieveListOfExpenditure();
            _mainGoal = myDAL.retrieveMainGoal();
            _listOfWishlistItem = myDAL.retrieveListOfWishlistItem();
            _taxRate = myDAL.retrieveTaxRate();
            _superannuationRate = myDAL.retrieveSuperannuationRate();
            _safetyMargin = myDAL.retrieveSafetyMargin();
            _mainGoalPercentage = myDAL.retrieveMainGoalPercentage();
            goalCost = _mainGoal.Cost;
            amountPerPayForMainGoal = 2000;
        }

        [TestFixtureTearDown]
        public void TestTearDownTheEnvironment()
        {
            core = null;
            _listofIncome = null;
            _listOfExpenditure = null;
            _mainGoal = null;
            _listOfWishlistItem = null;

        }

        [Test,ExpectedException]
        public void TestCalculateGrossIncomeWithEmptyIncomeList()
        {
            Assert.AreEqual(5100, core.getGrossIncome(new List<Income>()));
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
            double result = core.getGrossIncomePerYear(_listofIncome, 0);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetGrossIncomePerYearForNegativePeriod()
        {
            double result = core.getGrossIncomePerYear(_listofIncome, -5);
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
            Assert.AreEqual(220.0 * 12, core.getTotalExpenditurePerYear(_listOfExpenditure));
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
            Assert.AreEqual(220.0 * 26, core.getTotalExpenditurePerYear(_listOfExpenditure, 26));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestGetTotalExpenditurePerYearWithNullListUsingNoOfPays()
        {
            double result = core.getTotalExpenditurePerYear(null, 26);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestGetTotalExpenditurePerYearWithNoOfPaysEqualsZero()
        {
            double result = core.getTotalExpenditurePerYear(_listOfExpenditure, 0);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestGetTotalExpenditurePerYearWithNegativeNoOfPays()
        {
            double result = core.getTotalExpenditurePerYear(_listOfExpenditure, -5);
        }
        #endregion

        #region getNetIncomePerYear Tests
        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetNetIncomePerYearNegativeTaxRate()
        {
            core.getNetIncomePerYear(-0.15F, 0.2F,  _listofIncome, 12);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetNetIncomePerYearNegativeSuperannuationRate()
        {
            core.getNetIncomePerYear(0.15F, -0.2F, _listofIncome, 12);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetNetIncomePerYearEmptyList()
        {
            core.getNetIncomePerYear(0.15F, 0.2F, new List<Income>(), 12);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestgetNetIncomePerYearNegativePayPerYear()
        {
            core.getNetIncomePerYear(-0.15F, 0.2F, _listofIncome, -5);
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
            Assert.AreEqual(116700.0, core.getAmountAvailableForGoalsPerYear(_taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, 26),0.1);
        }
        #endregion

        #region getNoOfPaysRequiredToAccomplishGoal Tests
        [Test, ExpectedException(typeof(DivideByZeroException))]

        public void TestgetNoOfPaysRequiredToAccomplishGoalDivisionbyZero()
        {
            int numPay = core.getNoOfPaysRequiredToAccomplishGoal(goalCost, 0);

        }
        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]

        public void TestgetNoOfPaysRequiredToAccomplishGoalAmountPerPayNegative()
        {
            int numPay = core.getNoOfPaysRequiredToAccomplishGoal(goalCost, -20);

        }
        #endregion

        #region goalPayableBeforeDeadline Tests
        [Test]
        public void TestgoalPayableBeforeDeadline_1()
        {
            Assert.AreEqual(true, core.goalPayableBeforeDeadline(goalCost, amountPerPayForMainGoal,80));
        }
        [Test]
        public void TestgoalPayableBeforeDeadline_2()
        {
            Assert.AreEqual(false, core.goalPayableBeforeDeadline(goalCost,amountPerPayForMainGoal, 40));
        }
        [Test,ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestgoalPayableBeforeDeadlineDesiredAmountOutOfRange()
        {
            Boolean Payable = core.goalPayableBeforeDeadline(goalCost, amountPerPayForMainGoal, -30);
        }
        #endregion

        #region getAmountAvailableForGoalsPerPay Test
        [Test, ExpectedException(typeof(DivideByZeroException))]
        public void TestgetAmountAvailableForGoalsPerPayNumberOFPayequalzero()
        {
            double AmountAvailableForGoalsPerPay =core.getAmountAvailableForGoalsPerPay(_taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, 0);
        }
        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestgetAmountAvailableForGoalsPerPayNumberOFPaylessthanzero()
        {
            double AmountAvailableForGoalsPerPay = core.getAmountAvailableForGoalsPerPay(_taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, -5);
        }
        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestgetAmountAvailableForGoalsPerPayListOfIncomeEmpty()
        {
            double AmountAvailableForGoalsPerPay = core.getAmountAvailableForGoalsPerPay(_taxRate, _superannuationRate, _listOfExpenditure, null, 75);
        }
        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestgetAmountAvailableForGoalsPerPayListOfExpenditureEmpty()
        {
            double AmountAvailableForGoalsPerPay = core.getAmountAvailableForGoalsPerPay(_taxRate, _superannuationRate, null, _listofIncome, 75);
        }
        #endregion

        #region  getRemainingAmountForSecondaryGoalsPerPay Tests
        [Test, ExpectedException(typeof(DivideByZeroException))]
        public void TestgetRemainingAmountForSecondaryGoalsPerPayNumberOFPayequalzero()
        {
            double RemainingAmountForSecondaryGoalsPerPay = core.getRemainingAmountForSecondaryGoalsPerPay(amountPerPayForMainGoal,_taxRate,_superannuationRate,_listOfExpenditure,_listofIncome,0);
        }
        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestgetRemainingAmountForSecondaryGoalsPerPayNumberOFPaylessthanzero()
        {
            double RemainingAmountForSecondaryGoalsPerPay = core.getRemainingAmountForSecondaryGoalsPerPay(amountPerPayForMainGoal, _taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, -5);
        }
        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestgetRemainingAmountForSecondaryGoalsPerPayListOfIncomeNull()
        {
            double RemainingAmountForSecondaryGoalsPerPay = core.getRemainingAmountForSecondaryGoalsPerPay(amountPerPayForMainGoal, _taxRate, _superannuationRate, _listOfExpenditure, null, 75);
        }
        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void TestgetRemainingAmountForSecondaryGoalsPerPayListOfExpenditureNull()
        {
            double RemainingAmountForSecondaryGoalsPerPay = core.getRemainingAmountForSecondaryGoalsPerPay(amountPerPayForMainGoal, _taxRate, _superannuationRate, null, _listofIncome, 75);
        }
        #endregion
    }
}
