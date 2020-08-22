using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

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
        static double AccountBalance(string cOrS, List<Transaction> history)
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
        static double GetTransactionAmount()
        {
            double amount = 999999999;
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
                else
                {
                    if (trueDouble == false)
                    {
                        Console.Write("Please enter the amount:"); // Later implement quit option
                        trueDouble = double.TryParse(Console.ReadLine(), out amount);
                    }
                    if (trueDouble == false)
                    {
                        Console.WriteLine("Sorry, we couldn't recognize the amount type.");
                    }
                    if (amount < 0)
                    {
                        Console.WriteLine("Sorry, the specified amount was negative. Please input the amount in positive form");
                        trueDouble = false;
                        tries--;
                    }
                }
            }
            if (amount == 0)
            {
                Console.WriteLine("The specified amount was $0.00, which means no change will happen to your balance.\r\n");
                amount = -1;
            }
            return amount;
        }
        static bool CheckBounce(string cOrS, List<Transaction> balance, double amount)
        {
            var total = AccountBalance(cOrS, balance);
            if (total >= amount)
            {
                return true;
            }
            else
                return false;
        }
        static int CheckUserChoice(int options, int tryinput)
        {
            int choice = 1000;
            bool trueInt = false;
            int tries = -1;
            while (trueInt == false)
            {
                tries++;
                if (tries == tryinput && tries != 1)
                {
                    Console.WriteLine("Amount of input tries has been exceeded.");
                    trueInt = true;
                    choice = 2;
                }
                if (trueInt == false)
                {
                    trueInt = int.TryParse(Console.ReadLine(), out choice);
                    if (trueInt == false || choice > options || choice <= 0)
                    {
                        Console.WriteLine("Input was not recognized. Try again.");
                        trueInt = false;
                    }
                }
            }
            return choice;
        }
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Welcome Gavin to the First Bank of Suncoast! \r\n\r\n\r\n");
            var gavinsTransactions = new List<Transaction>();

            if (File.Exists("gavinsTransactions.csv"))
            {
                var fileName = "gavinsTransactions.csv";
                var reader = new StreamReader(fileName);
                var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                gavinsTransactions = csvReader.GetRecords<Transaction>().ToList();
                reader.Close();
            }
            // stream in Gavin's CSV file and populate List, for now, we will use below dummy data
            /*gavinsTransactions.Add(new Transaction()
            {
                amount = 36,
                checkingOrSavings = "C",
                withdrawalOrDeposit = "D",
            });
            gavinsTransactions.Add(new Transaction()
            {
                amount = 34,
                checkingOrSavings = "C",
                withdrawalOrDeposit = "D",
            });
            gavinsTransactions.Add(new Transaction()
            {
                amount = 35,
                checkingOrSavings = "C",
                withdrawalOrDeposit = "W",
            });
            gavinsTransactions.Add(new Transaction()
            {
                amount = 26,
                checkingOrSavings = "S",
                withdrawalOrDeposit = "D",
            });
            gavinsTransactions.Add(new Transaction()
            {
                amount = 24,
                checkingOrSavings = "S",
                withdrawalOrDeposit = "D",
            });
            gavinsTransactions.Add(new Transaction()
            {
                amount = 25,
                checkingOrSavings = "S",
                withdrawalOrDeposit = "W",
            });*/

            bool running = true;
            while (running == true)
            {
                Console.WriteLine("\r\n What would you like to do, Gavin:\r\n");
                Console.WriteLine("{ 1 }" + "Deposit in Savings");
                Console.WriteLine("{ 2 }" + "Withdraw Savings");
                Console.WriteLine("{ 3 }" + "Deposit in Checking");
                Console.WriteLine("{ 4 }" + "Withdraw Checking");
                Console.WriteLine("{ 5 }" + "View Savings and Checking Account Balance");
                Console.WriteLine("{ 6 }" + "Quit");
                Console.WriteLine("\r\n Please choose an action for this session:\r\n {1} {2} {3} {4} {5} {6}");
                int choice = CheckUserChoice(6, 1);

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Please specify the amount you want to deposit in your savings account:");
                        var amountDS = GetTransactionAmount();
                        if (amountDS >= 0.01)
                        {
                            gavinsTransactions.Add(new Transaction()
                            {
                                amount = amountDS,
                                checkingOrSavings = "S",
                                withdrawalOrDeposit = "D",
                            });
                            Console.WriteLine($"Deposit has been accepted and processed!\r\n Your Savings balance is {AccountBalance("S", gavinsTransactions)}");
                        }
                        Console.WriteLine("\r\nReturning back to menu. \r\nPress enter to continue.");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case 2:
                        int case2Running = 1;
                        while (case2Running == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Please specify the amount you want to withdraw in your savings account:");
                            var amountWS = GetTransactionAmount();
                            if (amountWS >= 0.01)
                            {
                                if (amountWS > AccountBalance("S", gavinsTransactions))
                                {
                                    Console.WriteLine($"Transaction Denied. The specified withdrawal of {amountWS}\r\n is greater than your savings balance of {AccountBalance("S", gavinsTransactions)}");
                                    Console.WriteLine("\r\nWould you like to:\r\n{ 1 } try a different amount\r\n{ 2 } return back to the menu");
                                    Console.WriteLine("\r\n Choices: {1} {2}");
                                    case2Running = CheckUserChoice(2, 3);
                                }
                                else
                                {
                                    gavinsTransactions.Add(new Transaction()
                                    {
                                        amount = amountWS,
                                        checkingOrSavings = "S",
                                        withdrawalOrDeposit = "W",
                                    });
                                    Console.WriteLine($"Withdrawal has been accepted and processed!\r\n Your Savings balance is {AccountBalance("S", gavinsTransactions)}");
                                    case2Running = 2;
                                }
                            }
                            else
                                case2Running = 2;
                        }
                        Console.WriteLine("\r\nReturning back to menu. \r\nPress enter to continue.");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("Please specify the amount you want to deposit in your checking account:");
                        var amountDC = GetTransactionAmount();
                        if (amountDC >= 0.01)
                        {
                            gavinsTransactions.Add(new Transaction()
                            {
                                amount = amountDC,
                                checkingOrSavings = "C",
                                withdrawalOrDeposit = "D",
                            });
                            Console.WriteLine($"Deposit has been accepted and processed!\r\n Your checking balance is {AccountBalance("C", gavinsTransactions)}");
                        }
                        Console.WriteLine("\r\nReturning back to menu. \r\nPress enter to continue.");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case 4:
                        int case4Running = 1;
                        while (case4Running == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Please specify the amount you want to withdraw in your checking account:");
                            var amountWS = GetTransactionAmount();
                            if (amountWS >= 0.01)
                            {
                                if (amountWS > AccountBalance("C", gavinsTransactions))
                                {
                                    Console.WriteLine($"Transaction Denied. The specified withdrawal of {amountWS}\r\n is greater than your checking balance of {AccountBalance("C", gavinsTransactions)}");
                                    Console.WriteLine("\r\nWould you like to:\r\n{ 1 } try a different amount\r\n{ 2 } return back to the menu");
                                    Console.WriteLine("\r\n Choices: {1} {2}");
                                    case4Running = CheckUserChoice(2, 3);
                                }
                                else
                                {
                                    gavinsTransactions.Add(new Transaction()
                                    {
                                        amount = amountWS,
                                        checkingOrSavings = "C",
                                        withdrawalOrDeposit = "W",
                                    });
                                    Console.WriteLine($"Withdrawal has been accepted and processed!\r\n Your Checking balance is {AccountBalance("C", gavinsTransactions)}");
                                    case4Running = 2;
                                }
                            }
                            else
                                case4Running = 2;
                        }
                        Console.WriteLine("\r\nReturning back to menu. \r\nPress enter to continue.");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine($"Your Savings balance is {AccountBalance("S", gavinsTransactions)}.\r\nYour Checking balance is {AccountBalance("C", gavinsTransactions)}.");
                        Console.WriteLine("\r\nReturning back to menu. \r\nPress enter to continue.");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case 6:
                        running = false;
                        break;
                }
                var fileWriter = new StreamWriter("gavinsTransactions.csv");
                var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
                csvWriter.WriteRecords(gavinsTransactions);
                fileWriter.Close();









            }
            Console.Clear();
            Console.WriteLine("Session Ended. Thank you for using First Bank of Suncoast!");



        }
    }
}
