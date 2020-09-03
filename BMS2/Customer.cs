using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS2
{
    public class Customer
    {
        ReadAndWriteDataBase User = new ReadAndWriteDataBase();
        Banking_system bs = new Banking_system();
        Account acc = new Account();
        public int LoggedInMenu()
        {
            Console.WriteLine("**** Banking System | Welcome " + User.Title + ". " + User.FName + " " + User.LName + " ****\n");
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("|{0}|", "");
            Console.WriteLine("|{0}|", "1. Deposit Money");
            Console.WriteLine("|{0}|", "2. Withdraw Money");
            Console.WriteLine("|{0}|", "3. Tranfer Money");
            Console.WriteLine("|{0}|", "4. My Passbook");
            Console.WriteLine("|{0}|", "5. Show My Account Details");
            Console.WriteLine("|{0}|", "6. Logout");
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
        public void LogIn()
        {

            Console.WriteLine("**** Banking System | Login Page ****\n");
            Console.Write("{0}", "Enter your account number   :  ");
            User.Account_Number = Int64.Parse(Console.ReadLine());
            if (User.ReadFromDatabase())
            {
                Console.Write("{0}", "Enter your account password :  ");
                string UserPassword = Console.ReadLine();
                if (UserPassword == User.Password)
                {
                    bool LoggedInFlag = true;
                    while (LoggedInFlag)
                    {
                        switch (LoggedInMenu())
                        {
                            case 1:
                                bs.DepositMoney();
                                break;
                            case 2:
                                bs.WithdrawMoney();
                                break;
                            case 3:
                                bs.TransferMoney();
                                break;
                            case 4:
                                acc.Passbook();
                                break;
                            case 5:
                                acc.ShowUserDetails();
                                break;
                            case 6:
                                bs.Logout();
                                LoggedInFlag = false;
                                break;
                            default:
                                Console.WriteLine("\n");
                                Console.WriteLine("Incorrect Option | Try Again!");
                                break;
                        }
                        if (LoggedInFlag)
                            Console.ReadKey();
                    }
                }
                else
                    Console.WriteLine("The password you entered is incorrect");
            }
            else
            {
                Console.WriteLine("Sorry but the account number : " + User.Account_Number + " does not exist in our database");
                Console.WriteLine("Please check the account number and try again!");
            }
        }
    }
}
