using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudgeting.BLL
{
    public class Expenditure
    {
        private string _name;
        private double _amount;
        private string _type;
        private Participant _source;

        public Expenditure(string name, double amount, string type)
        {
            Name = name;
            Amount = amount;
            Type = type;
        }
        public Expenditure(string name, double amount, string type,Participant source)
        {
            Name = name;
            Amount = amount;
            Type = type;
            Source = source;
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

        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public Participant Source
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
    }
}
