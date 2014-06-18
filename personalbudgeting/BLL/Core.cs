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
            //if (_listofIncome == null) throw new NullReferenceException();
            if (_taxRate<=0 || _superannuationRate<=0 || !_listofIncome.Any() || noOfPayPerYear<=0)
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
            if (noOfPayPerYear == 0)
                throw new DivideByZeroException();
            if (noOfPayPerYear < 0)
                throw new ArgumentOutOfRangeException();
            else
                return (getAmountAvailableForGoalsPerYear(_taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear) / noOfPayPerYear);
        }

        public double getRemainingAmountForSecondaryGoalsPerPay(double amountForMainGoalPerPay, float _taxRate, float _superannuationRate, List<Expenditure> _listOfExpenditure, List<Income> _listofIncome, int noOfPayPerYear)
        {
            return (getAmountAvailableForGoalsPerPay(_taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear) - amountForMainGoalPerPay);
        }

        public Boolean saveForMainGoal(MainGoal mg,double currentDeposit)
        {
            mg.AmountSaved += currentDeposit;
            if (mg.AmountSaved >= mg.Cost)
            {
                //saving completed for main goal
                return false;
            }
            return true;
        }

        public double calculatePendingAmountForGoal(Goal g)
        {
            return g.Cost - g.AmountSaved;
        }

        public Boolean TickOffWalletTableItem(WalletTableItem wli)
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

        public List<WalletTableItem> TickOffAllWalletTableItems(List<WalletTableItem> walletTableItems, double amountForMainGoalPerPay, float _taxRate, float _superannuationRate, List<Expenditure> _listOfExpenditure, List<Income> _listofIncome, int noOfPayPerYear)
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
                    Boolean walletTableItemTicked=TickOffWalletTableItem(wti);
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

    }
}
