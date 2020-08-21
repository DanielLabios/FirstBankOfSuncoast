using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstBankOfSuncoast
{
    class Transaction
    {
        public double amount { get; set; }
        public string checkingOrSavings { get; set; }
        public string withdrawalOrDeposit { get; set; }
    }
    class Program
    {
        public double AccountBalance(string cOrS, List<Transaction> history)
        {
            var accountType = new List<Transaction>();
            double total = 0.00;
            accountType.AddRange(history.Where(history => history.checkingOrSavings == cOrS));
            foreach (Transaction transaction in accountType)
            {
                if (transaction.withdrawalOrDeposit == "W")
                {
                    total = total - transaction.amount;
                }
                else if (transaction.withdrawalOrDeposit == "D")
                {
                    total = total + transaction.amount;
                }
            }
            return total;
        }
        public double GetTransactionAmount()
        {
            double amount;
            bool trueDouble = false;
            int tries = -1;
            while (trueDouble == false)
            {
                tries++;
                if (tries == 5)
                {
                    Console.WriteLine("You have exceeded the amount of tries alloted for this attempt.");
                    trueDouble = true;
                    amount = -1;
                }
                if (trueDouble == false)
                {
                    Console.WriteLine("Please enter the amount:"); // Later implement quit option
                    trueDouble = double.TryParse(Console.ReadLine(), out amount);
                }
                if (trueDouble == false)
                {
                    Console.WriteLine("Sorry, we couldn't recognize the amount type.");
                }
                else if (amount < 0)
                {
                    Console.WriteLine("Sorry, the specified amount was negative. Please input the amount in positive form");
                    trueDouble = false;
                    tries--;
                }
            }
            if (amount == 0)
            {
                Console.WriteLine("The specified amount was $0.00, which means no change will happen to your balance.\r\n Returning to the main menu.\r\n");
                amount = -1;
            }
            return amount;
        }
        public bool CheckBounce(string cOrS, List<Transaction> balance, double amount)
        {
            var total = AccountBalance(cOrS, balance);
            if (total >= amount)
            {
                return true;
            }
            else
                return false;
        }
        public int CheckUserChoice(int options, int tryinput)
        {
            int choice;
            bool trueInt = false;
            int tries = -1;
            while (trueInt == false)
            {
                tries++;
                if (tries == tryinput && tries != 1)
                {
                    Console.WriteLine("You have exceeded the amount of tries alloted for this attempt.");
                    trueInt = true;
                    choice = 1;
                }
                trueInt = int.TryParse(Console.ReadLine(), out choice);
                if (trueInt == false || choice > options || choice <= 0)
                {
                    Console.WriteLine("Input was not recognized. Try again.");
                    trueInt = false;
                }
            }
            return choice;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to C#");
        }
    }
}
