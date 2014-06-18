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
        List<WalletTableItem> listOfWishlistItem;
        float _taxRate;
        float _superannuationRate;
        float _safetyMargin;
        float _mainGoalPercentage;

        public DAL()
        {
            listOfIncome = new List<Income>();
            listOfIncome.Add(new Income("salary", "wife", 2500.0));
            listOfIncome.Add(new Income("salary", "husband", 2600.0));

            listOfExpenditure = new List<Expenditure>();
            listOfExpenditure.Add(new Expenditure("Car Insurance", 200.0, "Living Expense"));
            listOfExpenditure.Add(new Expenditure("Electricity", 20.0, "Living Expense"));

            mainGoal = new MainGoal("Loan", "house loan from MCB", 150000.0, 2000.0, new DateTime(2011, 01, 01));

            listOfWishlistItem = new List<WalletTableItem>();
            listOfWishlistItem = new List<WalletTableItem>();
            listOfWishlistItem.Add(new WalletTableItem("Camera", "Canon", 150.0, 0.0, 10.0, 0));
            listOfWishlistItem.Add(new WalletTableItem("Phone", "IPhone", 200.0, 20.0, 10.0, 2));
            listOfWishlistItem.Add(new WalletTableItem("Washing Machine", "Samsung", 250.0, 75.0, 15.0, 5));

            _taxRate = 0.15F;
            _superannuationRate = 0.05F;
            _safetyMargin = 50;
            _mainGoalPercentage = 75;
        }

        public List<Income> retrieveListOfIncome()
        {
            return listOfIncome;
        }

        public void addIncome(string name, string source, double amount)
        {
            listOfIncome.Add(new Income(name, source, amount));
        }

        public List<Expenditure> retrieveListOfExpenditure()
        {
            return listOfExpenditure;
        }

        public void addExpenditure(string name, double amount, string type)
        {
            listOfExpenditure.Add(new Expenditure(name, amount, type));
        }

        public void setMainGoal(string name, string description, double cost, double amountSaved, DateTime deadline)
        {
            mainGoal = new MainGoal(name, description, cost, amountSaved, deadline);
        }

        public MainGoal retrieveMainGoal()
        {
            return mainGoal;
        }

        public List<WalletTableItem> retrieveListOfWishlistItem()
        {
            
            return listOfWishlistItem;
        }
        
        public float retrieveTaxRate()
        {
            return _taxRate;
        }

        public void setTaxRate(float tr)
        {
            _taxRate = tr;
        }

        public float retrieveSuperannuationRate()
        {
            return _superannuationRate;
        }

        public void setSuperannuationRate(float sp)
        {
            _superannuationRate = sp;
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
    }
}
