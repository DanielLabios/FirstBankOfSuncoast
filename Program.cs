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
            while (trueDouble == false)
            {
                Console.WriteLine("Please enter the amount:"); //Later, implement try counter == 5 and quit
                trueDouble = double.TryParse(Console.ReadLine(), out amount);
                if (trueDouble == false)
                {
                    Console.WriteLine("Sorry, we couldn't recognize the amount type. Please try again. \r\n");
                }
                else if (amount < 0)
                {
                    Console.WriteLine("Sorry, the specified amount was negative. Please input the amount in positive form");
                    trueDouble = false;
                }
            }
            if (amount == 0)
            {
                Console.WriteLine("The specified amount was $0.00, which means no change will happen to your balance.\r\n Returning to the main menu.\r\n");
                amount = -1;
            }
            return amount;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to C#");
        }
    }
}
