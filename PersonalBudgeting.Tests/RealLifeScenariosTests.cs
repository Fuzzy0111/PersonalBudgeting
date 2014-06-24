﻿using NUnit.Framework;
using PersonalBudgeting.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudgeting.Tests
{
    [TestFixture]
    class RealLifeScenariosTests
    {
        Core core;
        Budget myBudget;
        double _amountForMainGoalPerPay;

        List<Income> listOfIncome;
        List<Expenditure> listOfExpenditure;
        MainGoal mainGoal;
        List<WalletTableItem> listOfWalletTableItem;
        float _taxRate;
        float _superannuationRate;
        float _safetyMargin;
        float _mainGoalPercentage;
        BankAccount _savingsAccount;
        int _noOfPaysPerYear;
        Participant partner1, partner2;
         List<Participant> participants;
        [TestFixtureSetUp]
        public void TestStepUp()
        {
            Core core = new Core();
            myBudget = new Budget();
            participants=new List<Participant>();
            partner1 = new Participant("Alan", "Turing");
            partner2 = new Participant("Louise", "Hay");
            participants.Add(partner1);
            participants.Add(partner2);

            mainGoal = new MainGoal("Loan", "house loan from MCB", 13000.0, 0.0, 26);
            listOfWalletTableItem.Add(new WalletTableItem("Camera", "Canon", 1300.0, 0.0, 50.0));
            listOfWalletTableItem.Add(new WalletTableItem("Phone", "IPhone", 2600.0, 0.0, 100.0));

            _noOfPaysPerYear = 26;
            _taxRate = 0.15F; //todo: calculate
            _superannuationRate = 0.05F; //todo: take into consideration that Super is calculated as a minimum of 9% or higher. sometimes it can be part of the pay packet & sometimes it can be over and aabove the pay packet. eg. you could get an annual pay of "60k incl. Super" & your friend could get an annual pay of "60k + Super".
            _safetyMargin = 50; //todo: ????  To clarify what this one is with Gerald.

            _savingsAccount = new BankAccount(500);

        }
        [TestFixtureTearDown]
        public void TestTeatDown()
        {
            core = null;
            myBudget = null;
        }
      
        public void TestCalculateBudget_NormalScenario_Successful()
        {
            myBudget.addIncome("salary", partner1, 1500.0);
            myBudget.addIncome("salary", partner2, 2000.0);
            myBudget.addExpenditure("Phone", 480.0, "Living Expense", participants);
            myBudget.addExpenditure("Car Maintenance", 300.0, "Living Expense", participants);
            myBudget.addExpenditure("Car Insurance", 170.0, "Living Expense", participants);
            myBudget.addExpenditure("Electricity", 560.0, "Living Expense", participants);
            myBudget.addWalletTableItem("Camera", "Canon", 1300.0, 0.0, 50.0);
            myBudget.addWalletTableItem("Phone", "IPhone", 2600.0, 0.0, 100.0);
            
            Assert.AreEqual(3500,core.getGrossIncome(myBudget.ListOfIncome));
            Assert.AreEqual(60000.2, core.getGrossIncomePerYear(myBudget.ListOfIncome, myBudget.NoOfPaysPerYear));
            Assert.AreEqual(48000.16, core.getNetIncomePerYear(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear));
            Assert.AreEqual(12120.0, core.getTotalExpenditurePerYear(myBudget.ListOfExpenditure));
            Assert.AreEqual(47880.2, core.getAmountAvailableForGoalsPerYear(myBudget.TaxRate, myBudget.SuperannuationRate, myBudget.ListOfExpenditure, myBudget.ListOfIncome, myBudget.NoOfPaysPerYear));
        }
    }
}