using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudgeting.BLL
{
    public class Income
    {
        public string _name;
        public Participant _source;
        public double _amount;
        public double Tax { get; set; }
        public double DesiredAmountPerYear { get; set; }
        public double Superannuation { get; set; }
        public Boolean PayPacketInclusive { get; set; }

        public Income(string name, Participant source, double amount)
        {
            Name = name;
            Source = source;
            Amount = amount;
            Tax = 0;
            Superannuation = 0;
        }
        public Income(string name,Participant source,double amount,Boolean payPacketInclusive,double desiredAmountPerYear)
        {
            Name = name;
            Source = source;
            Amount = amount;
            Tax = 0;
            Superannuation =0;
            PayPacketInclusive = payPacketInclusive;
            DesiredAmountPerYear = desiredAmountPerYear;
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
