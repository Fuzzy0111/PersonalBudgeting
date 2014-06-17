using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PersonalBudgeting.MOCK_DAL;

namespace PersonalBudgeting.BLL
{
    public class Budget
    {
        MockBudget mockBudget;
        /*
        private List<Income> _listofIncome;
        private List<Expenditure> _listOfExpenditure;
        private MainGoal _mainGoal;
        private List<WishlistItem> _listOfWishlistItem;
        private static float _taxRate;
        private float _superannuationRate;
        private float _safetyMargin;
        private float _mainGoalPercentage;
        */
        public Budget()
        {
            this.mockBudget = new MockBudget();
            /*
            _listofIncome = mockBudget.retrieveListOfIncome();
            _listOfExpenditure = mockBudget.retrieveListOfExpenditure();
            _mainGoal = mockBudget.retrieveMainGoal();
            _listOfWishlistItem = mockBudget.retrieveListOfWishlistItem();
            _taxRate = mockBudget.retrieveTaxRate();
            _superannuationRate = mockBudget.retrieveSuperannuationRate();
            _safetyMargin = mockBudget.retrieveSafetyMargin();
            _mainGoalPercentage = mockBudget.retrieveMainGoalPercentage();
             * */
        }

        public MainGoal getMainGoal()
        {
            return mockBudget.retrieveMainGoal();
        }

        public float getTaxRate()
        {
            return mockBudget.retrieveTaxRate();
        }

        public float getSuperannuationRate()
        {
            return mockBudget.retrieveSuperannuationRate();
        }

        public float getMainGoalPercentage()
        {
            return mockBudget.retrieveMainGoalPercentage();
        }

        public MockBudget getMockBudget()
        {
            return mockBudget;
        }

        public List<Income> getListOfIncome()
        {
            return mockBudget.retrieveListOfIncome();
        }

        public List<Expenditure> getListOfExpenditure()
        {
            return mockBudget.retrieveListOfExpenditure();
        }

        public List<WishlistItem> getListOfWishlistItem()
        {
            return mockBudget.retrieveListOfWishlistItem();
        }

        public void addWishlistItem(string name, string description, double cost, double amountSaved, double contributionPerTick, int noOfTicks)
        {
            mockBudget.retrieveListOfWishlistItem().Add(new WishlistItem(name, description, cost, amountSaved, contributionPerTick, noOfTicks));
        }
        public void removeWishlistItem(WishlistItem wishlistItem)
        {
            mockBudget.retrieveListOfWishlistItem().Remove(wishlistItem);
        }

        /*
        public double getGrossIncome()
        {
            double grossIncome = 0;
            foreach (Income income in _listofIncome)
            {
                grossIncome += income.Amount;
            }
            return grossIncome;
        }

        public double getNetIncome()
        {
            double grossIncome = getGrossIncome();
            double tax = grossIncome * _taxRate;
            double superannuation = grossIncome * _superannuationRate;
            
            return grossIncome - tax - superannuation;
        }

        public double getTotalExpenditure()
        {
            double totalExpenditure = 0;
            foreach (Expenditure expenditure in _listOfExpenditure)
            {
                totalExpenditure += expenditure.Amount;
            }
            return totalExpenditure;
        }

        public double getAmountAvailableForGoals()
        {
            return getNetIncome() - getTotalExpenditure();
        }
         * */
    }
}
