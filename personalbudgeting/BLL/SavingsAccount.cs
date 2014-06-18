using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudgeting.BLL
{
    public class SavingsAccount
    {
        private double _amountAvailable;

        public SavingsAccount()
        {
            this._amountAvailable = 0;
        }

        public SavingsAccount(int existingAmountInSavingsAccount)
        {
            this._amountAvailable = existingAmountInSavingsAccount;
        }

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
