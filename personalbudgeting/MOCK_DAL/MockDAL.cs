using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PersonalBudgeting.BLL;

namespace PersonalBudgeting.MOCK_DAL
{
    public class DAL // Mock DAL
    {
        List<Income> listOfIncome;
        List<Expenditure> listOfExpenditure;
        MainGoal mainGoal;
        List<WalletTableItem> listOfWalletTableItem;
        float _taxRate;
        float _superannuationRate;
        float _safetyMargin;
        float _mainGoalPercentage;
        SavingsAccount _savingsAccount;
        int _noOfPaysPerYear;

        public DAL()
        {
            listOfIncome = new List<Income>();
            listOfIncome.Add(new Income("salary", "wife", 2500.0));
            listOfIncome.Add(new Income("salary", "husband", 2600.0));

            listOfExpenditure = new List<Expenditure>();
            listOfExpenditure.Add(new Expenditure("Car Insurance", 200.0, "Living Expense"));
            listOfExpenditure.Add(new Expenditure("Electricity", 20.0, "Living Expense"));

            mainGoal = new MainGoal("Loan", "house loan from MCB", 150000.0, 0.0, 26);

            listOfWalletTableItem = new List<WalletTableItem>();
            listOfWalletTableItem = new List<WalletTableItem>();
            listOfWalletTableItem.Add(new WalletTableItem("Camera", "Canon", 150.0, 0.0, 10.0));
            listOfWalletTableItem.Add(new WalletTableItem("Phone", "IPhone", 200.0, 20.0, 10.0));
            listOfWalletTableItem.Add(new WalletTableItem("Washing Machine", "Samsung", 250.0, 75.0, 15.0));

            _noOfPaysPerYear = 26;
            _taxRate = 0.15F; //todo: calculate
            _superannuationRate = 0.05F; //todo: take into consideration that Super is calculated as a minimum of 9% or higher. sometimes it can be part of the pay packet & sometimes it can be over and aabove the pay packet. eg. you could get an annual pay of "60k incl. Super" & your friend could get an annual pay of "60k + Super".
            _safetyMargin = 50; //todo: ????  To clarify what this one is with Gerald.

            _savingsAccount = new SavingsAccount(500);

        }

        public int retrieveNoOfPaysPerYear()
        {
            return _noOfPaysPerYear;
        }

        public List<Income> retrieveListOfIncome()
        {
            return listOfIncome;
        }
        /*
        public void addIncome(string name, string source, double amount)
        {
            listOfIncome.Add(new Income(name, source, amount));
        }
        */
        public List<Expenditure> retrieveListOfExpenditure()
        {
            return listOfExpenditure;
        }
        /*
        public void addExpenditure(string name, double amount, string type)
        {
            listOfExpenditure.Add(new Expenditure(name, amount, type));
        }
        */

        public void setMainGoal(string name, string description, double cost, double amountSaved, int durationInNoOfPays)
        {
            mainGoal = new MainGoal(name, description, cost, amountSaved, durationInNoOfPays);
        }
        
        public MainGoal retrieveMainGoal()
        {
            return mainGoal;
        }

        public List<WalletTableItem> retrieveListOfWalletTableItem()
        {
            
            return listOfWalletTableItem;
        }

        public float retrieveTaxRate()
        {
            return _taxRate;
        }

        public float retrieveTaxRate(double income)
        {
            return calculateTaxRate(income);
        }

        public void setTaxRate(float tr)
        {
            _taxRate = tr;
        }

        public float retrieveSuperannuationRate()
        {
            return _superannuationRate;
        }

        public void setSuperannuationRate(double desiredAmount,double totalIncome,Boolean payPacketInclusive)
        {
            _superannuationRate = calculateSuperannuationRate(desiredAmount,totalIncome,payPacketInclusive);
        }

        public float retrieveSafetyMargin()
        {
            return _safetyMargin;
        }

        public void setSafetyMargin(float sm)
        {
            _safetyMargin = sm;
        }

        public float retrieveMainGoalPercentage()
        {
            return _mainGoalPercentage;
        }

        public void setMainGoalPercentage(float mgp)
        {
            _mainGoalPercentage = mgp;
        }
        //todo: MOVE??
        public float calculateTaxRate(double totalIncome)
        {
            if (totalIncome < 0)
                throw new ArgumentException();
            if (totalIncome < 18201)
                return 0;
            if (totalIncome < 37001)
                return (float)((totalIncome - 18200) * 0.19) / 100;
            if (totalIncome < 80001)
                return (float)(3572 + ((totalIncome - 37000) * 0.325)) / 100;
            if (totalIncome < 180001)
                return (float)(17547 + ((totalIncome - 80000) * 0.37)) / 100;
            else //if (totalIncome > 180000)
                return (float)(54547 + ((totalIncome - 180000) * 0.45)) / 100;
        }
        //todo: MOVE??
        public float calculateSuperannuationRate(double desiredAmount,double totalIncome,Boolean payPacketInclusive)
        {
            if (payPacketInclusive)
            {
                return 0;
            }
            if ((desiredAmount / totalIncome) < 0.0925)
            {
                return 0.0925F;
            }
            else
            {
                return (float)(desiredAmount / totalIncome);

            }
        }

        public SavingsAccount retrieveSavingsAccount()
        {
            return _savingsAccount;
        }
    }
}

// todo: rename Mock_dal folder and class
