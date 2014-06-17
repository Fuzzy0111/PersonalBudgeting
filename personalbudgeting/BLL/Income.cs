using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudgeting.BLL
{
    public class Income
    {
        private string _name;
        private string _source;
        private double _amount;
        
        public Income(string name, string source, double amount)
        {
            Name = name;
            Source = source;
            Amount = amount;
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

        public string Source 
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
            }
        }

        public double Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
            }
        }
    }
}
