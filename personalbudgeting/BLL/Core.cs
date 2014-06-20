using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudgeting.BLL
{
    public class Core
    {
        public double getGrossIncome(List<Income> _listofIncome)
        {
            double grossIncome = 0;
            foreach (Income income in _listofIncome)
            {
                grossIncome += income.Amount;
            }
            return grossIncome;
        }

        public double getGrossIncomePerYear(List<Income> _listofIncome, int noOfPayPerYear)
        {
            if (_listofIncome == null)
                throw new ArgumentNullException();
            if (noOfPayPerYear <= 0)
                throw new ArgumentException("No of pays per year cannot be negative.");
            return getGrossIncome(_listofIncome) * noOfPayPerYear;
        }

        public double getNetIncomePerYear(float _taxRate, float _superannuationRate, List<Income> _listofIncome, int noOfPayPerYear)
        {
            if (_listofIncome == null) throw new ArgumentNullException();
            if (_taxRate<=0 || _superannuationRate<=0 || noOfPayPerYear<=0) //todo: argExp for empty?
                throw new ArgumentException();
            return getGrossIncomePerYear(_listofIncome, noOfPayPerYear) *  (1 - _taxRate + _superannuationRate);
        }

        public double getTotalExpenditure(List<Expenditure> _listOfExpenditure)
        {
            if (_listOfExpenditure == null) throw new ArgumentNullException();
            double totalExpenditure = 0;
            foreach (Expenditure expenditure in _listOfExpenditure)
            {
                totalExpenditure += expenditure.Amount;
            }
            return totalExpenditure;
        }

        public double getTotalExpenditurePerYear(List<Expenditure> _listOfExpenditure, int noOfPayPerYear)
        {
            if (noOfPayPerYear <= 0)
                throw new ArgumentException("No of pays per year less or equal to zero.");

            return getTotalExpenditure(_listOfExpenditure) * noOfPayPerYear;
        }

        public double getTotalExpenditurePerYear(List<Expenditure> _listOfExpenditure)
        {
            return getTotalExpenditure(_listOfExpenditure) * 12;
        }

        public double getAmountAvailableForGoalsPerYear(float _taxRate, float _superannuationRate, List<Expenditure> _listOfExpenditure, List<Income> _listofIncome, int noOfPayPerYear)
        {
            return (getNetIncomePerYear(_taxRate, _superannuationRate, _listofIncome, noOfPayPerYear) - getTotalExpenditurePerYear(_listOfExpenditure));
        }

        public int getNoOfPaysRequiredToAccomplishGoal(double  goalCost, double amountPerPay)
        {
            if (amountPerPay == 0)
                throw new DivideByZeroException();
            if(amountPerPay <0|| goalCost<0)
                throw new ArgumentOutOfRangeException();
            return (int)Math.Ceiling(goalCost / amountPerPay);
        }

        public double getMinimumAmountRequiredPerPayToAccomplishGoalBeforeDeadline(double goalCost, int desiredNoOfPaysForGoalAccomplishment)
        {
            return (goalCost/desiredNoOfPaysForGoalAccomplishment);
        }

        public Boolean goalPayableBeforeDeadline(double goalCost, double amountPerPay, int desiredNoOfPaysForGoalAccomplishment)
        {
            if (desiredNoOfPaysForGoalAccomplishment <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            int noOfPaysRequired = getNoOfPaysRequiredToAccomplishGoal(goalCost, amountPerPay);
            if (noOfPaysRequired <= desiredNoOfPaysForGoalAccomplishment)
                return true;
            else
                return false;
        }

        public double getAmountAvailableForGoalsPerPay(float _taxRate, float _superannuationRate, List<Expenditure> _listOfExpenditure, List<Income> _listofIncome, int noOfPayPerYear)
        {
            if (_listofIncome == null || _listOfExpenditure == null)
                throw new ArgumentNullException();
            if (noOfPayPerYear == 0)
                throw new DivideByZeroException();
            if (noOfPayPerYear < 0)
                throw new ArgumentOutOfRangeException();
            else
                return (getAmountAvailableForGoalsPerYear(_taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear) / noOfPayPerYear);
        }

        public double getRemainingAmountForSecondaryGoalsPerPay(double amountForMainGoalPerPay, float _taxRate, float _superannuationRate, List<Expenditure> _listOfExpenditure, List<Income> _listofIncome, int noOfPayPerYear)
        {
            if (_listofIncome == null || _listOfExpenditure == null)
                throw new ArgumentNullException();
            if (noOfPayPerYear == 0)
                throw new DivideByZeroException();
            if (noOfPayPerYear < 0)
                throw new ArgumentOutOfRangeException();
            return (getAmountAvailableForGoalsPerPay(_taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear) - amountForMainGoalPerPay);
        }

        public void saveForMainGoal(SavingsAccount mySavingsAccount , double amountForMainGoalPerPay, MainGoal mainGoal)
        {
            if (mySavingsAccount.AmountAvailable < amountForMainGoalPerPay)
                return;
            mainGoal.AmountSaved += amountForMainGoalPerPay;
        }

        public double calculatePendingAmountForGoal(Goal g)
        {
            return g.Cost - g.AmountSaved;
        }

        public Boolean tickOffWalletTableItem(WalletTableItem wti)
        {
            wti.AmountSaved += wti.ContributionPerTick;
            wti.NoOfTicks++;
            if (wti.AmountSaved >= wti.Cost)
            {
                //Wallet table item saving completed
                return false;
            }
            return true;
        }

        public Boolean tickOffWalletTableItem(WalletTableItem wti, SavingsAccount mySavingsAccount)
        {
            if (wti.AmountSaved < wti.Cost)
            {
                if ((wti.Cost - wti.AmountSaved) <= wti.ContributionPerTick)
                {
                    wti.AmountSaved += (wti.Cost - wti.AmountSaved);
                    return false;
                    //saving completed for this walletTableItem
                }
                wti.AmountSaved += wti.ContributionPerTick;
                return true;
            }
            else if (wti.AmountSaved == wti.Cost)
                return false;
            else
                throw new ObjectDisposedException("wallet table item's amount saved exceeds its cost");
        }

        public List<WalletTableItem> tickAllWalletTableItems(List<WalletTableItem> walletTableItems, double amountForMainGoalPerPay, float _taxRate, float _superannuationRate, List<Expenditure> _listOfExpenditure, List<Income> _listofIncome, int noOfPayPerYear)
        {
            double totalAmountTicked = 0.0;
            List<WalletTableItem> unTickedWalletTableItems = new List<WalletTableItem>();
            if (walletTableItems == null) throw new ArgumentNullException();
            foreach (WalletTableItem wti in walletTableItems)
            {
                if ((wti.ContributionPerTick + totalAmountTicked) > getRemainingAmountForSecondaryGoalsPerPay(amountForMainGoalPerPay, _taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear))
                {
                    //Not enough money to tick this wallet table item
                    unTickedWalletTableItems.Add(wti);
                }
                else
                {
                    Boolean walletTableItemTicked=tickOffWalletTableItem(wti);
                    totalAmountTicked += wti.ContributionPerTick;

                    if (!walletTableItemTicked)
                    {
                        //Wallet table item saving completed
                        walletTableItems.Remove(wti);
                    }
                }
            }
            return unTickedWalletTableItems;
        }

        public void tickOffAllWalletTableItems(SavingsAccount mySavingsAccount, List<WalletTableItem> walletTableItems)
        {
            if (walletTableItems == null) throw new ArgumentNullException();
            foreach (WalletTableItem wti in walletTableItems)
            {
                Boolean walletTableItemSavingsNotCompleted = tickOffWalletTableItem(wti, mySavingsAccount);
                if (!walletTableItemSavingsNotCompleted)
                {
                    walletTableItems.Remove(wti);
                }
            }
        }

        public void creditAmountAvailableForGoalsPerPayInSavingsAccount(SavingsAccount mySavingsAccount, float _taxRate, float _superannuationRate, List<Expenditure> _listOfExpenditure, List<Income> _listofIncome, int noOfPayPerYear)
        {
            double amountAvailableForGoalsPerPay = getAmountAvailableForGoalsPerPay(_taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear);
            mySavingsAccount.AmountAvailable += amountAvailableForGoalsPerPay;
        }

        public void updateSavingsAccount(SavingsAccount mySavingsAccount, float _taxRate, float _superannuationRate, List<Expenditure> _listOfExpenditure, List<Income> _listofIncome, int noOfPayPerYear, MainGoal mainGoal, double amountForMainGoalPerPay, List<WalletTableItem> _listOfWalletTableItems)
        {
            creditAmountAvailableForGoalsPerPayInSavingsAccount(mySavingsAccount, _taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear);

            saveForMainGoal(mySavingsAccount,amountForMainGoalPerPay,mainGoal);

            tickOffAllWalletTableItems(mySavingsAccount, _listOfWalletTableItems);
        }
        
        public void withdrawFromSavingsAccount(SavingsAccount mySavingsAccount, double amountToWithdraw)
        {
            if (mySavingsAccount == null) throw new ArgumentNullException();
            if (amountToWithdraw > mySavingsAccount.AmountAvailable) throw new ArgumentOutOfRangeException();
            mySavingsAccount.AmountAvailable -= amountToWithdraw;
        }

        public double getSurplusAmountPerPay(List<WalletTableItem> walletTableItems, double amountForMainGoalPerPay, float _taxRate, float _superannuationRate, List<Expenditure> _listOfExpenditure, List<Income> _listofIncome, int noOfPayPerYear)
        {
            if (walletTableItems == null)
                return getRemainingAmountForSecondaryGoalsPerPay(amountForMainGoalPerPay, _taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear);
            else
            {
                double totalContributionPerTick = 0;
                foreach (WalletTableItem wti in walletTableItems)
                {
                    totalContributionPerTick += wti.ContributionPerTick;
                }

                return getRemainingAmountForSecondaryGoalsPerPay(amountForMainGoalPerPay, _taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear) - totalContributionPerTick;
            }
        }

        public double getCurrentSurplusInSavingsAccount(SavingsAccount savingsAccount, MainGoal mainGoal, List<WalletTableItem> listOfWalletTableItems)
        {
            double amountSavedForGoals = mainGoal.AmountSaved;
            foreach (WalletTableItem wti in listOfWalletTableItems)
            {
                amountSavedForGoals += wti.AmountSaved;
            }
            return savingsAccount.AmountAvailable - amountSavedForGoals;
        }
    }
}
// todo: subtract safety margin along with expenditure

/*
0 – $18,200 --- Nil
$18,201 – $37,000 --- 19c for each $1 over $18,200
$37,001 – $80,000 --- $3,572 plus 32.5c for each $1 over $37,000
$80,001 – $180,000 --- $17,547 plus 37c for each $1 over $80,000
$180,001 and over --- $54,547 plus 45c for each $1 over $180,000
*/
