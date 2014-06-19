using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudgeting.BLL
{
    public class MainGoal : Goal
    {
        private int durationInNoOfPays;

        public MainGoal(string name, string description, double cost, double amountSaved, int durationInNoOfPays)
            : base(name, description, cost, amountSaved)
        {
            this.durationInNoOfPays = durationInNoOfPays;
        }

        public int DurationInNoOfPays
        {
            get
            {
                return durationInNoOfPays;
            }
            set
            {
                durationInNoOfPays = value;
            }
        }
    }
}
