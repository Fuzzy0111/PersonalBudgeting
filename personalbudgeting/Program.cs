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
            Console.WriteLine("Net Income per year = {0}", core.getNetIncomePerYear(budget.TaxRate, budget.SuperannuationRate, budget.ListOfIncome, budget.NoOfPaysPerYear));
            Console.WriteLine("Amount Available for goals per year = {0}", core.getAmountAvailableForGoalsPerYear(budget.TaxRate, budget.SuperannuationRate, budget.ListOfExpenditure, budget.ListOfIncome, budget.NoOfPaysPerYear));
            Console.WriteLine("Amount Available for goals = {0}", core.getAmountAvailableForGoalsPerPay(budget.TaxRate, budget.SuperannuationRate, budget.ListOfExpenditure, budget.ListOfIncome, budget.NoOfPaysPerYear));
            Console.Read();
        }
    }
}
