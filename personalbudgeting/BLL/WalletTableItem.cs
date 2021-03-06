﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudgeting.BLL
{
    public class WalletTableItem : Goal
    {
        private double _contributionPerTick;

        public WalletTableItem(string name, string description, double cost, double amountSaved, double contributionPerTick)
            : base(name, description, cost, amountSaved)
        {
            ContributionPerTick = contributionPerTick;
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
    }
}