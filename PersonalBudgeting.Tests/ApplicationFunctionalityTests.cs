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
    class ApplicationFunctionalityTests
    {
        Core core;
        DAL myDAL;
        List<Income> _listofIncome;
        List<Expenditure> _listOfExpenditure;
        MainGoal _mainGoal;
        List<WalletTableItem> _listOfWalletTableItems;
        float _taxRate; //todo: investigate current tax rate in australia & mock accordingly
        float _superannuationRate;
        double amountForMainGoalPerPay;
        SavingsAccount mySavingsAccount;
        int noOfPayPerYear;

        [TestFixtureSetUp]
        public void TestSetuptheEnvironment()
        {
            myDAL = new DAL();
            core = new Core();
            _listofIncome = myDAL.retrieveListOfIncome();
            _listOfExpenditure = myDAL.retrieveListOfExpenditure();
            _mainGoal = myDAL.retrieveMainGoal();
            _listOfWalletTableItems = myDAL.retrieveListOfWishlistItem(); //todo: Refactor to proper naming
            _taxRate = myDAL.retrieveTaxRate();
            _superannuationRate = myDAL.retrieveSuperannuationRate();
            noOfPayPerYear = 26;
            amountForMainGoalPerPay = core.getMinimumAmountRequiredPerPayToAccomplishGoalBeforeDeadline(_mainGoal.Cost,52);
            mySavingsAccount = myDAL.retrieveSavingsAccount();
        }

        [TestFixtureTearDown]
        public void TestTearDownTheEnvironment()
        {
            core = null;
            _listofIncome = null;
            _listOfExpenditure = null;
            _mainGoal = null;
            _listOfWalletTableItems = null;
        }
        
        [Test]
        public void TestForThreePays()
        {
            core.updateSavingsAccount(mySavingsAccount, _taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear, _mainGoal, amountForMainGoalPerPay, _listOfWalletTableItems);
            core.updateSavingsAccount(mySavingsAccount, _taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear, _mainGoal, amountForMainGoalPerPay, _listOfWalletTableItems);
            core.updateSavingsAccount(mySavingsAccount, _taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear, _mainGoal, amountForMainGoalPerPay, _listOfWalletTableItems);

            core.withdrawFromSavingsAccount(mySavingsAccount,500);
            core.withdrawFromSavingsAccount(mySavingsAccount,50);
            core.withdrawFromSavingsAccount(mySavingsAccount,125);
            core.withdrawFromSavingsAccount(mySavingsAccount,100);

            Assert.AreEqual(4536.5, mySavingsAccount.AmountAvailable,0.1);
        }
    }
}

// refactor: remove mainGoalPercentage