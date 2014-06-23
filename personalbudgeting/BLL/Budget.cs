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
        public int NoOfPaysPerYear { get; set; }
        public List<Income> ListOfIncome { get; set; }
        public List<Expenditure> ListOfExpenditure { get; set; }
        public List<WalletTableItem> ListOfWalletTableItem { get; set; }
        public float SuperannuationRate { get; set; }
        public float TaxRate { get; set; }
        public SavingsAccount SavingsAccount { get; set; }
        public MainGoal mainGoal { 
            get
            {
                return dal.retrieveMainGoal();
            }
            set
            {
                dal.setMainGoal(value.Name, value.Description, value.Cost, value.AmountSaved, value.DurationInNoOfPays);
            }
        }

        public Budget()
        {
            this.dal = new DAL();

            ListOfIncome = dal.retrieveListOfIncome();
            ListOfExpenditure = dal.retrieveListOfExpenditure();
            mainGoal = dal.retrieveMainGoal();
            ListOfWalletTableItem = dal.retrieveListOfWalletTableItem();
            TaxRate = dal.retrieveTaxRate();
            SuperannuationRate = dal.retrieveSuperannuationRate();
            //this._safetyMargin;
            SavingsAccount = dal.retrieveSavingsAccount();
            NoOfPaysPerYear = dal.retrieveNoOfPaysPerYear();
        }

        public void addWalletTableItem(string name, string description, double cost, double amountSaved, double contributionPerTick)
        {
            ListOfWalletTableItem.Add(new WalletTableItem(name, description, cost, amountSaved, contributionPerTick));
        }

        public void removeWalletTableItem(WalletTableItem WalletTableItem)
        {
            ListOfWalletTableItem.Remove(WalletTableItem);
        }

        public void addIncome(string name, Participant source, double amount)
        {
            ListOfIncome.Add(new Income(name, source, amount));
        }

        public void removeIncome(Income income)
        {
            ListOfIncome.Remove(income);
        }

        public void addExpenditure(string name, double amount, string type)
        {
            ListOfExpenditure.Add(new Expenditure(name, amount, type));
        }

        public void removeExpenditure(Expenditure expenditure)
        {
            ListOfExpenditure.Remove(expenditure);
        }
    }
}