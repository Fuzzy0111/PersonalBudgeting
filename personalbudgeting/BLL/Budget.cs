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
        List<Income> _listOfIncome;
        List<Expenditure> _listOfExpenditure;
        MainGoal _mainGoal;
        List<WalletTableItem> _listOfWalletTableItem;
        float _taxRate;
        float _superannuationRate;
        //float _safetyMargin;
        SavingsAccount _savingsAccount;

        DAL dal;

        public Budget()
        {
            this.dal = new DAL();

            this._listOfIncome = dal.retrieveListOfIncome();
            this._listOfExpenditure = dal.retrieveListOfExpenditure();
            this._mainGoal = dal.retrieveMainGoal();
            this._listOfWalletTableItem = dal.retrieveListOfWalletTableItem();
            this._taxRate = dal.retrieveTaxRate();
            this._superannuationRate = dal.retrieveSuperannuationRate();
            //this._safetyMargin;
            this._savingsAccount = dal.retrieveSavingsAccount();
        }

        public List<Income> ListOfIncome
        {
            get
            {
                return _listOfIncome;
            }
            set
            {
                _listOfIncome = value;
            }
        }

        public List<Expenditure> ListOfExpenditure
        {
            get
            {
                return _listOfExpenditure;
            }
            set
            {
                _listOfExpenditure = value;
            }
        }

        public List<WalletTableItem> ListOfWalletTableItem
        {
            get
            {
                return _listOfWalletTableItem;
            }
            set
            {
                _listOfWalletTableItem = value;
            }
        }

        public float SuperannuationRate
        {
            get
            {
                return _superannuationRate;
            }
            set
            {
                _superannuationRate = value;
            }
        }

        public float TaxRate
        {
            get
            {
                return _taxRate;
            }
            set
            {
                _taxRate = value;
            }
        }

        public SavingsAccount SavingsAccount
        {
            get
            {
                return _savingsAccount;
            }
            set
            {
                _savingsAccount = value;
            }
        }

        public void addWalletTableItem(string name, string description, double cost, double amountSaved, double contributionPerTick, int noOfTicks)
        {
            _listOfWalletTableItem.Add(new WalletTableItem(name, description, cost, amountSaved, contributionPerTick, noOfTicks));
        }
        public void removeWalletTableItem(WalletTableItem WalletTableItem)
        {
            _listOfWalletTableItem.Remove(WalletTableItem);
        }


    }
}