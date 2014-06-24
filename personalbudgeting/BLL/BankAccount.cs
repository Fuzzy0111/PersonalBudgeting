using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudgeting.BLL
{
    public class BankAccount
    {

        public double SavingsForPersonalUse { get; set; }
        public double SavingsForGoals { get; set; }
        public double SavingsForExpenditures { get; set; }

        public BankAccount()
        {
            this._amountAvailable = 0;
            this.SavingsForPersonalUse = 0;
            this.SavingsForGoals = 0;
            this.SavingsForPersonalUse = 0;
        }

        public BankAccount(double SavingsForPersonalUse)
        {
            this.SavingsForPersonalUse = 0;
            this.SavingsForGoals = 0;
            this.SavingsForPersonalUse = SavingsForPersonalUse;
        }
        public BankAccount(double SavingsForPersonalUse, double SavingsForGoals, double SavingsForExpenditures)
        {
            this.SavingsForPersonalUse = SavingsForPersonalUse;
            this.SavingsForGoals = SavingsForGoals;
            this.SavingsForExpenditures = SavingsForExpenditures;

        }

        //todo:to be removed
        private double _amountAvailable;



        public double AmountAvailable
        {
            get
            {
                return _amountAvailable;
            }
            set
            {
                _amountAvailable = value;
            }
        }
    }
}
