using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudgeting.BLL
{
    public abstract class Goal
    {
        private string _name;
        private string _description;
        private double _cost;
        private double _amountSaved;

        public Goal(string name, string description, double cost, double amountSaved)
        {
            this.Name = name;
            this.Description = description;
            this.Cost = cost;
            this.AmountSaved = amountSaved;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
        
        public double Cost
        {
            get
            {
                return _cost;
            }
            set
            {
                _cost = value;
            }
        }
        
        public double AmountSaved
        {
            get
            {
                return _amountSaved;
            }
            set
            {
                _amountSaved = value;
            }
        }
    }
}
