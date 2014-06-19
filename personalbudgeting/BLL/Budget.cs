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
        DAL dal;

        public Budget()
        {
            this.dal = new DAL();
        }

        public MainGoal getMainGoal()
        {
            return dal.retrieveMainGoal();
        }

        //public float getTaxRate()
        //{
        //    return dal.retrieveTaxRate();
        //}

        public float getSuperannuationRate()
        {
            return dal.retrieveSuperannuationRate();
        }

        public float getMainGoalPercentage()
        {
            return dal.retrieveMainGoalPercentage();
        }

        public DAL getMockDAL()
        {
            return dal;
        }

        public List<Income> getListOfIncome()
        {
            return dal.retrieveListOfIncome();
        }

        public List<Expenditure> getListOfExpenditure()
        {
            return dal.retrieveListOfExpenditure();
        }

        public List<WalletTableItem> getListOfWalletTableItem()
        {
            return dal.retrieveListOfWalletTableItem();
        }

        public void addWalletTableItem(string name, string description, double cost, double amountSaved, double contributionPerTick, int noOfTicks)
        {
            dal.retrieveListOfWalletTableItem().Add(new WalletTableItem(name, description, cost, amountSaved, contributionPerTick, noOfTicks));
        }
        public void removeWalletTableItem(WalletTableItem WalletTableItem)
        {
            dal.retrieveListOfWalletTableItem().Remove(WalletTableItem);
        }
    }
}
