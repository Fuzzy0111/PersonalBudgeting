using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalBudgeting.MOCK_DAL;
using PersonalBudgeting.BLL;

namespace PersonalBudgeting
{
    public class Program
    {
        static void Main(string[] args)
        {
            Budget myBudget = new Budget();
            Core myCore = new Core();

            Console.WriteLine("Total Gross income: {0}", myCore.getGrossIncome(myBudget.getListOfIncome()));
            Console.WriteLine("Total Net income: {0}", myCore.getNetIncome(myBudget.getTaxRate(), myBudget.getSuperannuationRate(), myBudget.getListOfIncome()));
            Console.WriteLine("Total Expenditure: {0}", myCore.getTotalExpenditure(myBudget.getListOfExpenditure()));
            Console.WriteLine("Amount available for goals: {0}", myCore.getAmountAvailableForGoals(myBudget.getTaxRate(), myBudget.getSuperannuationRate(), myBudget.getListOfExpenditure(), myBudget.getListOfIncome()));

            int amount;
            do
            {
                Console.Write("Amount to be assigned to {0}:", ((Goal)(myBudget.getMainGoal())).Name);
                string input = Console.ReadLine();
                amount = Convert.ToInt32(input);
            }
            while (amount > myCore.getAmountAvailableForGoals(myBudget.getTaxRate(), myBudget.getSuperannuationRate(), myBudget.getListOfExpenditure(), myBudget.getListOfIncome()));
            Console.WriteLine("amount is: {0}", amount);

            if (!myCore.increment(myBudget.getMainGoal(), amount))
            {
                Console.WriteLine("Main goal completed!");
            }

            double totalAmountTicked = 0.0;
            List<WishlistItem> wishlistItems =  myBudget.getListOfWishlistItem();
            foreach (WishlistItem wli in wishlistItems)
            {
                if ((wli.ContributionPerTick + totalAmountTicked) > myCore.getAmountAvailableForGoals(myBudget.getTaxRate(), myBudget.getSuperannuationRate(), myBudget.getListOfExpenditure(), myBudget.getListOfIncome()) - amount)
                {
                    Console.WriteLine("Not enough money to tick all wishlist items!!");
                    break;
                }
                
                if (!myCore.Tick(wli))
                {
                    Console.WriteLine("Wish list item saving completed for " + wli.Name);
                    myBudget.removeWishlistItem(wli);
                }
                Console.WriteLine("{0}: Pending amount: {1}; Amount ticked: {2}", wli.Name, myCore.calculatePendingAmount(wli), wli.ContributionPerTick);
                totalAmountTicked += wli.ContributionPerTick;
            }
            Console.WriteLine("Amount left is: {0}", myCore.getAmountAvailableForGoals(myBudget.getTaxRate(), myBudget.getSuperannuationRate(), myBudget.getListOfExpenditure(),myBudget.getListOfIncome()) - amount - totalAmountTicked);
            Console.ReadKey();
        }
    }
}
