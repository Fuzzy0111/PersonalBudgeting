using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudgeting.BLL
{
    public class MainGoal : Goal
    {
        private DateTime _deadline;

        public MainGoal(string name, string description, double cost, double amountSaved, DateTime deadline): base(name, description, cost, amountSaved)
        {
            _deadline = deadline;
        }

        public DateTime Deadline
        {
            get
            {
                return _deadline;
            }
            set
            {
                _deadline = value;
            }
        }
    }
}
