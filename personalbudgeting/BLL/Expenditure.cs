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
        private List<Participant> Participants;

        public Expenditure(string name, double amount, string type)
        {
            Name = name;
            Amount = amount;
            Type = type;
        }
        public Expenditure(string name, double amount, string type,List<Participant> Participants)
        {
            Name = name;
            Amount = amount;
            Type = type;
            Source = Participants;
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

        public List<Participant> Source
        {
            get
            {
                return Participants;
            }
            set
            {
                Participants = value;
            }
        }
    }
}
