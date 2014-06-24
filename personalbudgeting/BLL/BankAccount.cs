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
            this.SavingsForExpenditures= 0;
            this.SavingsForGoals = 0;
            this.SavingsForPersonalUse = 0;
        }

        public BankAccount(double SavingsForPersonalUse)
        {
            this.SavingsForExpenditures = 0;
            this.SavingsForGoals = 0;
            this.SavingsForPersonalUse = SavingsForPersonalUse;
        }
        public BankAccount(double SavingsForPersonalUse, double SavingsForGoals, double SavingsForExpenditures)
        {
            this.SavingsForPersonalUse = SavingsForPersonalUse;
            this.SavingsForGoals = SavingsForGoals;
            this.SavingsForExpenditures = SavingsForExpenditures;

        }
        public double getTotalBalance()
        {
            return (SavingsForExpenditures + SavingsForGoals + SavingsForPersonalUse);
        }
    }
}
