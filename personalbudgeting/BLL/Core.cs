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
                throw new NullReferenceException();
            if (!_listofIncome.Any())
                throw new ArgumentException();
            if (noOfPayPerYear <= 0)
                throw new ArgumentException("No of pays per year cannot be negative.");
            return getGrossIncome(_listofIncome) * noOfPayPerYear;
        }

        public double getNetIncomePerYear(float _taxRate, float _superannuationRate, List<Income> _listofIncome, int noOfPayPerYear)
        {
            return getGrossIncomePerYear(_listofIncome, noOfPayPerYear) *  (1 - _taxRate + _superannuationRate);
        }

        public double getTotalExpenditure(List<Expenditure> _listOfExpenditure)
        {
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

        public int getNoOfPaysRequiredToAccomplishGoal(double amountPerPay, double goalCost)
        {
            return (int)Math.Ceiling(goalCost / amountPerPay);
        }

        public double getMinimumAmountRequiredPerPayToAccomplishGoalBeforeDeadline(double goalCost, int desiredNoOfPaysForGoalAccomplishment)
        {
            return (goalCost/desiredNoOfPaysForGoalAccomplishment);
        }

        public Boolean goalPayableBeforeDeadline(double amountPerPay, double goalCost, int desiredNoOfPaysForGoalAccomplishment)
        {
            int noOfPaysRequired = getNoOfPaysRequiredToAccomplishGoal(amountPerPay, goalCost);
            if (noOfPaysRequired <= desiredNoOfPaysForGoalAccomplishment)
                return true;
            else
                return false;
        }

        public double getAmountAvailableForGoalsPerPay(float _taxRate, float _superannuationRate, List<Expenditure> _listOfExpenditure, List<Income> _listofIncome, int noOfPayPerYear)
        {
            if (noOfPayPerYear == 0)
                throw new DivideByZeroException();
            else
                return (getAmountAvailableForGoalsPerYear(_taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear) / noOfPayPerYear);
        }

        public double getRemainingAmountForSecondaryGoalsPerPay(double amountForMainGoalPerPay, float _taxRate, float _superannuationRate, List<Expenditure> _listOfExpenditure, List<Income> _listofIncome, int noOfPayPerYear)
        {
            return (getAmountAvailableForGoalsPerPay(_taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear) - amountForMainGoalPerPay);
        }

        public void saveForMainGoal(MainGoal mg,double mainGoalDepositPerPay)
        {
            mg.AmountSaved += mainGoalDepositPerPay;
        }

        public double calculatePendingAmountForGoal(Goal g)
        {
            return g.Cost - g.AmountSaved;
        }

        public Boolean tickOffWalletTableItem(WalletTableItem wli)
        {
            wli.AmountSaved += wli.ContributionPerTick;
            wli.NoOfTicks++;
            if (wli.AmountSaved >= wli.Cost)
            {
                //Wallet table item saving completed
                return false;
            }
            return true;
        }

        public List<WalletTableItem> tickOffAllWalletTableItems(List<WalletTableItem> walletTableItems, double amountForMainGoalPerPay, float _taxRate, float _superannuationRate, List<Expenditure> _listOfExpenditure, List<Income> _listofIncome, int noOfPayPerYear)
        {
            double totalAmountTicked = 0.0;
            List<WalletTableItem> unTickedWalletTableItems = new List<WalletTableItem>();
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

        public void updateSavingsAccount(SavingsAccount mySavingsAccount, float _taxRate, float _superannuationRate, List<Expenditure> _listOfExpenditure, List<Income> _listofIncome, int noOfPayPerYear, MainGoal mg, double mainGoalDepositPerPay, List<WalletTableItem> walletTableItems, double amountForMainGoalPerPay)
        {
            double amountAvailableForGoalsPerPay = getAmountAvailableForGoalsPerPay(_taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear);
            
            saveForMainGoal(mg, mainGoalDepositPerPay);

            List<WalletTableItem> unTickedWalletTableItems  = tickOffAllWalletTableItems(walletTableItems, amountForMainGoalPerPay, _taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear);
            double totalAmountForUntickedWalletTableItems = 0.0;
            foreach (WalletTableItem wti in unTickedWalletTableItems)
            {
                totalAmountForUntickedWalletTableItems += wti.ContributionPerTick;
            }

            mySavingsAccount.AmountAvailable += amountAvailableForGoalsPerPay - mainGoalDepositPerPay - totalAmountForUntickedWalletTableItems;
        }
    }
}
