using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudgeting.BLL
{
    public class WalletTableItem : Goal
    {
        private double _contributionPerTick;
        private int _noOfTicks;

        public WalletTableItem(string name, string description, double cost, double amountSaved, double contributionPerTick, int noOfTicks)
            : base(name, description, cost, amountSaved)
        {
            ContributionPerTick = contributionPerTick;
            NoOfTicks = noOfTicks;
        }

        public double ContributionPerTick
        {
            get
            {
                return _contributionPerTick;
            }
            set
            {
                _contributionPerTick = value;
            }
        }

        public int NoOfTicks
        {
            get
            {
                return _noOfTicks;
            }
            set
            {
                _noOfTicks = value;
            }
        }
    }
}