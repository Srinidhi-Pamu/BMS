using System;
using System.Data.SqlClient;

namespace BMS2
{
    public class Banking_system
    {
        ReadAndWriteDataBase User = new ReadAndWriteDataBase();
        public int MainMenu()
        {
            //Console.Clear();
            Console.WriteLine("**** Welcome to  Banking System ****\n");
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("|{0}|", "");
            Console.WriteLine("|{0}|", "1. Login for Existing Customers");
            Console.WriteLine("|{0}|", "2. Open a new Account");
            Console.WriteLine("|{0}|", "3. Exit");
            Console.WriteLine("|{0}|", "");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("\n{0}", "Enter your choice : ");
            try
            {
                return (int.Parse(Console.ReadLine()));
            }
            catch (FormatException)
            {
                return 0;
            }
        }
        public void DepositMoney()
        {
            Console.Clear();
            Console.WriteLine("**** Banking System | Deposit Money ****\n");
            Console.WriteLine("{0}", "Enter your Account Number: ");
            User.Account_Number = Convert.ToInt64(Console.ReadLine());
            Console.Write("{0}", "Enter amount you want to deposit : ");
            Double DepositAmount = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("\n");
            Console.WriteLine("Amount deposited in your account successfully!");
            User.UpdatePassbook(User.Account_Number, DepositAmount, "Deposit");
            UpdatedBalance();
        }
        public void UpdatedBalance()
        {
            //Console.WriteLine("\n");
            //System.Data.SqlClient.SqlDataReader dataReader = User.UpdateBal();
            //int Amount = Convert.ToInt64(dataReader.GetValue(3));
            //while(dataReader.Read())
            //{
            //  double a = Convert.ToDouble(dataReader.GetValue(0));
            //}
            //User.Total_Balance = a;
            //int Amount = User.UpdateBal();
            User.Total_Balance = User.getBalance();
            Console.WriteLine("|{0}|", "Updated balance in your account is Rs. " + User.Total_Balance);
            //User.UpdatePassbook(User.Account_Number, User.Total_Balance, "Withdrawal");
            Console.ReadLine();
        }

        public void WithdrawMoney()
        {
            Console.Clear();
            Console.WriteLine("**** Banking System | Withdraw Money ****\n");
            Console.WriteLine("{0}", "Enter your Account Number: ");
            User.Account_Number = Convert.ToInt64(Console.ReadLine());
            Console.Write("{0}", "Enter amount you want to withdraw : ");
            Double WithDrawalAmount = Double.Parse(Console.ReadLine());
            Console.WriteLine("\n");
            User.Total_Balance = User.getBalance();
            if (WithDrawalAmount <= User.Total_Balance)
            {
                User.Total_Balance -= WithDrawalAmount;
                Console.WriteLine("Amount withdrawal from your account was successfull!");
                User.UpdatePassbook(User.Account_Number, WithDrawalAmount, "Withdrawal");
                UpdatedBalance();
            }
            else
                Console.WriteLine("You don't have sufficient balance in your account to complete this transaction!");
        }

        public void TransferMoney()
        {
            Console.Clear();
            Console.WriteLine("**** Banking System | Transfer Money ****\n");
            Console.WriteLine("{0}", "Plese ReEnter your Account Number: ");
            User.Account_Number = Convert.ToInt64(Console.ReadLine());
            User.Total_Balance = User.getBalance();
            Console.Write("{0}", "Enter amount you want to transfer               : ");
            Double TransferAmount = Double.Parse(Console.ReadLine());
            if (TransferAmount <= User.Total_Balance)
            {
                ReadAndWriteDataBase Transfer = new ReadAndWriteDataBase();
                Console.Write("{0}", "Enter Account Number where you want to transfer : ");
                Transfer.Account_Number = Int64.Parse(Console.ReadLine());
                if (Transfer.ReadFromDatabase())
                {
                    Console.WriteLine("\n{0}", "The account number " + Transfer.Account_Number + " belongs to " + Transfer.Title + ". " + Transfer.FName);
                    Console.Write("{0}", "Do you want to proceed with this transaction [y/n] ");
                    char choice = Console.ReadLine()[0];
                    Console.WriteLine("\n");
                    if (choice == 'y' || choice == 'Y')
                    {

                        Console.WriteLine("Rs. " + TransferAmount + " has been successfully transfered to " + Transfer.Title + ". " + Transfer.FName + "[" + Transfer.Account_Number + "]");
                        User.UpdatePassbook(User.Account_Number, TransferAmount, "NEFT To " + Transfer.Account_Number);
                        Transfer.UpdatePassbook(Transfer.Account_Number, TransferAmount, "NEFT From " + User.Account_Number);
                        UpdatedBalance();
                    }
                    else
                    {
                        Console.WriteLine("The transaction has been aborted!");
                    }
                }
                else
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("Sorry but the account number : " + Transfer.Account_Number + " does not exist in our database");
                    Console.WriteLine("Please check the account number and try again!");
                }
                Transfer.CloseConnection();
            }
            else
            {
                Console.WriteLine("\n");
                Console.WriteLine("You don't have sufficient balance in your account to complete this transaction");
            }
        }

        public void Logout()
        {
            User.WriteToDatabase(3);
            Console.WriteLine("\n");
            Console.WriteLine("Thanks for using our service!");
            Console.WriteLine("You have been successfully logged out!");
            Console.WriteLine("Press any key to go back to the main menu!");
        }
    }
}