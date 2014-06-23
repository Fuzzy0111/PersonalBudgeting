using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudgeting.BLL
{
    public class Participant
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public Boolean IsCasualWorker { get; set; }

        public Participant(string First_Name,string Last_Name,Boolean IsCasualWorker)
        {
            this.First_Name = First_Name;
            this.Last_Name = Last_Name;
            this.IsCasualWorker = IsCasualWorker;
        }

    }
}
