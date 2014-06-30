using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalBudgeting.BLL;

namespace PersonalBudgeting
{
    public class Program
    {
        static void Main(string[] args)
        {
            Core core = new Core();
            Budget budget = new Budget();

            Console.WriteLine("Gross Income per year = {0}", core.getGrossIncomePerYear(budget.ListOfIncome, budget.NoOfPaysPerYear));
            
           
            Console.Read();
        }
    }
}
