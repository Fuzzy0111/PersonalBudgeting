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
                throw new ArgumentException("No of pays per year cannot be negative or null.");
            return getGrossIncome(_listofIncome) * noOfPayPerYear;
        }

        public double getNetIncomePerYear(float _taxRate, float _superannuationRate, List<Income> _listofIncome, int noOfPayPerYear)
        {
            if (_listofIncome == null) throw new ArgumentNullException();
            if (_taxRate <= 0 || _superannuationRate <= 0 || noOfPayPerYear <= 0) //todo: argExp for empty?
                throw new ArgumentException();
            return getGrossIncomePerYear(_listofIncome, noOfPayPerYear) * (1 - _taxRate - _superannuationRate);
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
        /*
        public double getTotalExpenditurePerYear(List<Expenditure> _listOfExpenditure)
        {
            return getTotalExpenditure(_listOfExpenditure) * 12;
        }*/

        public double getAmountAvailableForGoalsPerYear(float _taxRate, float _superannuationRate, List<Expenditure> _listOfExpenditure, List<Income> _listofIncome, int noOfPayPerYear)
        {
            return (getNetIncomePerYear(_taxRate, _superannuationRate, _listofIncome, noOfPayPerYear) - getTotalExpenditurePerYear(_listOfExpenditure, noOfPayPerYear));
        }

        public int getNoOfPaysRequiredToAccomplishGoal(double goalCost, double amountPerPay)
        {
            if (amountPerPay == 0)
                throw new DivideByZeroException();
            if (amountPerPay < 0 || goalCost < 0)
                throw new ArgumentOutOfRangeException();
            return (int)Math.Ceiling(goalCost / amountPerPay);
        }

        public double getMinimumAmountRequiredPerPayToAccomplishGoalBeforeDeadline(double goalCost, int desiredNoOfPaysForGoalAccomplishment)
        {
            return (goalCost / desiredNoOfPaysForGoalAccomplishment);
        }

        public Boolean succesfullyMetMainGoal(Budget myBudget)
        {
            if (myBudget.mainGoal.noOfPaysAlreadySaved >= myBudget.mainGoal.DurationInNoOfPays)
            {
                if (myBudget.mainGoal.AmountSaved == myBudget.mainGoal.Cost)
                    return true;

                else return false;
            }
            else
            {
                return false;
            }
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

        public void addSavingsForMainGoal(Budget budget, double AmountToSave)
        {
            budget.mainGoal.AmountSaved += AmountToSave;
            addSavingsForMainGoal(budget, AmountToSave);
        }

        public void clearMainGoal(Budget myBudget)
        {
            removeFromSavingForGoals(myBudget.SavingsAccount,myBudget.mainGoal.AmountSaved);
        }

        public Boolean saveForMainGoal(BankAccount myAccount, double amountForMainGoalPerPay, MainGoal mainGoal, float _taxRate, float _superannuationRate, List<Expenditure> _listOfExpenditure, List<Income> _listofIncome, int noOfPayPerYear)
        {
            if (getAmountAvailableForGoalsPerPay(_taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear) < amountForMainGoalPerPay)
            {
                return false;
            }
            else
            {
                mainGoal.AmountSaved += amountForMainGoalPerPay;
                mainGoal.noOfPaysAlreadySaved++;
                addToSavingsForGoals(myAccount, amountForMainGoalPerPay);
                return true;
            }

        }

        public double calculatePendingAmountForGoal(Goal g)
        {
            return g.Cost - g.AmountSaved;
        }

        

        public void TransferWalletTableItemToMainGoal(Budget budget, MainGoal mainGoal, WalletTableItem wti, int durationInNoOfPays)
        {
            clearMainGoal(budget);
            mainGoal.Name = wti.Name;
            mainGoal.Description = wti.Description;
            mainGoal.Cost = wti.Cost;
            mainGoal.DurationInNoOfPays = durationInNoOfPays;
            mainGoal.AmountSaved = wti.AmountSaved;
           
            budget.removeWalletTableItem(wti);
        }

        public void CancelWalletTableItem(Budget budget, WalletTableItem wti)
        {
            budget.SavingsAccount.SavingsForGoals -= wti.AmountSaved;
            budget.SavingsAccount.SavingsForPersonalUse += wti.AmountSaved;
            budget.removeWalletTableItem(wti);
        }

        public Boolean tickWalletTableItem(BankAccount myAccount, WalletTableItem wti)
        {
            //usings savingsAccount
            if (wti.AmountSaved < wti.Cost)
            {
                if ((wti.Cost - wti.AmountSaved) <= wti.ContributionPerTick)
                {
                    wti.AmountSaved += (wti.Cost - wti.AmountSaved);
                    return false;
                    //saving completed for this walletTableItem
                }
                wti.AmountSaved += wti.ContributionPerTick;
                addToSavingsForGoals(myAccount, wti.ContributionPerTick);
                return true;
            }
            else if (wti.AmountSaved == wti.Cost)
                return false;
            else
                throw new ObjectDisposedException("wallet table item's amount saved exceeds its cost");
        }

        public Boolean tickOffWalletTableItem(Budget budget, WalletTableItem wti)
        {
            if (wti.AmountSaved < wti.Cost)
            {
                return false;
            }
            else
            {
                budget.SavingsAccount.SavingsForGoals -= wti.AmountSaved;
                budget.removeWalletTableItem(wti);
                return true;
            }

        }

        public double tickAllWalletTableItems(BankAccount myAccount, List<WalletTableItem> walletTableItems, double amountForMainGoalPerPay, float _taxRate, float _superannuationRate, List<Expenditure> _listOfExpenditure, List<Income> _listofIncome, int noOfPayPerYear)
        {
            //using RemainingAmountForSecondaryGoalsPerPay
            if (walletTableItems == null) throw new ArgumentNullException();

            double totalAmountTicked = 0.0;
            double remainingAmountForSecondaryGoalsPerPay = getRemainingAmountForSecondaryGoalsPerPay(amountForMainGoalPerPay, _taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear);

            foreach (WalletTableItem wti in walletTableItems)
            {
                if ((wti.ContributionPerTick + totalAmountTicked) <= remainingAmountForSecondaryGoalsPerPay)
                {
                    Boolean walletTableItemSavingsNotCompleted = tickWalletTableItem(myAccount,wti);
                    totalAmountTicked += wti.ContributionPerTick;

                    if (!walletTableItemSavingsNotCompleted)
                    {
                        //Wallet table item saving completed
                        walletTableItems.Remove(wti);
                    }
                }
            }
            
            return totalAmountTicked;
        }

        /*public void creditAmountAvailableForGoalsPerPayInSavingsAccount(BankAccount mySavingsAccount, float _taxRate, float _superannuationRate, List<Expenditure> _listOfExpenditure, List<Income> _listofIncome, int noOfPayPerYear)
        {
            if (noOfPayPerYear <= 0)
                throw new ArgumentOutOfRangeException();
            double amountAvailableForGoalsPerPay = getAmountAvailableForGoalsPerPay(_taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear);
            mySavingsAccount.AmountAvailable += amountAvailableForGoalsPerPay;
        }*/

        /*public void updateSavingsAccount(BankAccount mySavingsAccount, float _taxRate, float _superannuationRate, List<Expenditure> _listOfExpenditure, List<Income> _listofIncome, int noOfPayPerYear, MainGoal mainGoal, double amountForMainGoalPerPay, List<WalletTableItem> _listOfWalletTableItems)
        {
            if (_listOfExpenditure == null || _listofIncome == null)
            {
                throw new ArgumentNullException();
            }
            if (noOfPayPerYear <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            creditAmountAvailableForGoalsPerPayInSavingsAccount(mySavingsAccount, _taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear);

            saveForMainGoal(mySavingsAccount, amountForMainGoalPerPay, mainGoal, _taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear);

            tickAllWalletTableItems(_listOfWalletTableItems, amountForMainGoalPerPay, _taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear);
        }*/

        public void withdrawFromSavingsAccount(BankAccount mySavingsAccount, double amountToWithdraw)
        {
            if (mySavingsAccount == null) throw new ArgumentNullException();
            if (amountToWithdraw > mySavingsAccount.SavingsForPersonalUse) throw new ArgumentOutOfRangeException();
            mySavingsAccount.SavingsForPersonalUse -= amountToWithdraw;
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

        /*public double getCurrentSurplusInSavingsAccount(BankAccount savingsAccount, MainGoal mainGoal, List<WalletTableItem> listOfWalletTableItems)
        {
            if (savingsAccount == null || mainGoal == null || listOfWalletTableItems == null)
                throw new ArgumentNullException();
            double amountSavedForGoals = mainGoal.AmountSaved;
            foreach (WalletTableItem wti in listOfWalletTableItems)
            {
                amountSavedForGoals += wti.AmountSaved;
            }
            return savingsAccount.AmountAvailable - amountSavedForGoals;
        }
        */
        public void addToSavingsForPersonalUse(BankAccount myAccount, double Amount)
        {
            myAccount.SavingsForPersonalUse += Amount;
        }
        public Boolean removeToSavingsForPersonalUse(BankAccount myAccount, double Amount)
        {
            if (myAccount.SavingsForPersonalUse == 0)
            {
                return false;
            }
            else
            {
                myAccount.SavingsForPersonalUse -= Amount;
                return true;
            }
        }

        public void addToSavingsForExpenses(BankAccount myAccount, double Amount)
        {
            myAccount.SavingsForExpenditures += Amount;
        }

        public Boolean removeFromSavingForExpenses(BankAccount myAccount, double Amount)
        {
            if (myAccount.SavingsForExpenditures == 0)
            {
                return false;
            }
            else
            {
                myAccount.SavingsForExpenditures -= Amount;
                return true;
            }

        }

        public void addToSavingsForGoals(BankAccount myAccount, double Amount)
        {
            myAccount.SavingsForGoals += Amount;
        }
        public Boolean removeFromSavingForGoals(BankAccount myAccount, double Amount)
        {
            if (myAccount.SavingsForGoals == 0)
            {
                return false;
            }
            else
            {
                myAccount.SavingsForGoals -= Amount;
                return true;
            }

        }
        public void addIncomeForCasualWorker(Budget budget, string name, Participant source, double amount)
        {
            Income casualIncome = new Income(name, source, amount);
            addToSavingsForPersonalUse(budget.SavingsAccount, casualIncome.Amount);
        }

        public void updateBankAccount(BankAccount myAccount, float _taxRate, float _superannuationRate, List<Expenditure> _listOfExpenditure, List<Income> _listofIncome, int noOfPayPerYear, MainGoal mainGoal, double amountForMainGoalPerPay, List<WalletTableItem> _listOfWalletTableItems)
        {
            if (_listOfExpenditure == null || _listofIncome == null)
            {
                throw new ArgumentNullException();
            }

            if (noOfPayPerYear <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            addToSavingsForExpenses(myAccount, getTotalExpenditure(_listOfExpenditure));

            Boolean savedforMainGoal = saveForMainGoal(myAccount,
                                                        amountForMainGoalPerPay,
                                                        mainGoal,
                                                        _taxRate,
                                                        _superannuationRate,
                                                        _listOfExpenditure,
                                                        _listofIncome,
                                                        noOfPayPerYear);
            double totalAmountTicked;
            //double totalAmountTicked = tickAllWalletTableItems(myAccount, _listOfWalletTableItems, amountForMainGoalPerPay, _taxRate, _superannuationRate, _listOfExpenditure, _listofIncome, noOfPayPerYear);
            double AmountToAddToSavingsForPersonalUse;
            if (savedforMainGoal)
            {
                totalAmountTicked = tickAllWalletTableItems( myAccount,
                                                             _listOfWalletTableItems,
                                                             amountForMainGoalPerPay,
                                                             _taxRate,
                                                             _superannuationRate,
                                                             _listOfExpenditure,
                                                             _listofIncome,
                                                             noOfPayPerYear
                                                           );
                AmountToAddToSavingsForPersonalUse = (getAmountAvailableForGoalsPerPay(_taxRate,
                                                                                _superannuationRate,
                                                                                _listOfExpenditure,
                                                                                _listofIncome,
                                                                                noOfPayPerYear
                                                                                )
                                                                 - (amountForMainGoalPerPay + totalAmountTicked)
                                                          );
            }
            else
            {
                totalAmountTicked = tickAllWalletTableItems(myAccount,
                                                            _listOfWalletTableItems,
                                                            0,
                                                            _taxRate,
                                                            _superannuationRate,
                                                            _listOfExpenditure,
                                                            _listofIncome,
                                                            noOfPayPerYear
                                                          );
                AmountToAddToSavingsForPersonalUse = (getAmountAvailableForGoalsPerPay(_taxRate,
                                                                                _superannuationRate,
                                                                                _listOfExpenditure,
                                                                                 _listofIncome,
                                                                                noOfPayPerYear
                                                                              )
                                                       - totalAmountTicked
                                              );

            }

            addToSavingsForPersonalUse(myAccount, AmountToAddToSavingsForPersonalUse);
        }
        public double calculateTaxPerYear(double income)
        {

            if (income < 0)
                throw new ArgumentException();
            if (income < 18201)
                return 0;
            if (income < 37001)
                return ((income - 18200) * 0.19);
            if (income < 80001)
                return (3572 + ((income - 37000) * 0.325));
            if (income < 180001)
                return (17547 + ((income - 80000) * 0.37));
            else //if (totalIncome > 180000)
                return (54547 + ((income - 180000) * 0.45));
        }

        public double calculateTaxPerPay(double income,int noOfPaysPerYear)
        {
            return (calculateTaxPerYear(income) /noOfPaysPerYear);
        }

        public float calculateSuperannuationRate(double desiredAmountPerYear, double totalIncomePerYear, Boolean payPacketInclusive)
        {
            if (payPacketInclusive)
            {
                return 0;
            }
            if ((desiredAmountPerYear / totalIncomePerYear) < 0.0925)
            {
                return 0.0925F;
            }
            else
            {
                return (float)(desiredAmountPerYear / totalIncomePerYear);

            }
        }
        public double calculateSuperannuationPerYear(double desiredAmountPerYear, double totalIncomePerYear, Boolean payPacketInclusive)
        {
            return (calculateSuperannuationRate(desiredAmountPerYear, totalIncomePerYear, payPacketInclusive)*totalIncomePerYear);
        }
        public double calculateSuperannuationPerPay(double desiredAmountPerYear, double totalIncomePerYear, Boolean payPacketInclusive, int noOfPaysPerYear)
        {
            return (calculateSuperannuationPerYear(desiredAmountPerYear, totalIncomePerYear, payPacketInclusive)/noOfPaysPerYear);
        }

    }
}
// todo: subtract safety margin along with expenditure
1
