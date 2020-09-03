using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS2
{
    public class Account
    {

        public Double Total_Balance { get; internal set; }
        public string FName { get; internal set; }
        public string LName { get; internal set; }
        public string Title { get; internal set; }
        public Int64 Phone_Number { get; internal set; }
        public string Password { get; internal set; }
        public string Address { get; internal set; }
        public DateTime LastLoginDetails { get; internal set; }

        public Int64 Account_Number { get; internal set; }

        ReadAndWriteDataBase User = new ReadAndWriteDataBase();
        public void SignUp()
        {
            //Console.Clear();
            Console.WriteLine("**** Banking System | Signup Page ****\n");
            Console.Write("{0}", "Your First Name          : ");
            User.FName = Console.ReadLine();
            Console.Write("{0}", "Your Last Name          : ");
            User.LName = Console.ReadLine();
            Console.Write("{0}", "Your gender[M/F]        : ");
            char Gender = Console.ReadLine()[0];
            User.Title = (Gender == 'M' || Gender == 'm') ? "Mr" : "Ms";
            Console.Write("{0}", "Enter Your Phone Number  : ");
            User.Phone_Number = Int64.Parse(Console.ReadLine());
            Console.Write("{0}", "Address  : ");
            User.Address = Console.ReadLine();
            Console.Write("{0}", "Password[max 21 chars]  : ");
            User.Password = Console.ReadLine();
            Console.Write("{0}", "Enter amount to deposit : ");
            User.Total_Balance = UInt64.Parse(Console.ReadLine());
            Double T_Balance = User.Total_Balance;
            //Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n");
            Console.WriteLine("|{0}|", "Thanks for banking with us | Your generated account number is " + User.GenerateAccountNumber());
            User.WriteToDatabase(2);
            User.UpdatePassbook(User.Account_Number, T_Balance, "First Deposit");
            //Console.WriteLine("\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Please note down your account number and password!");
            Console.ReadLine();
        }

        public void ShowUserDetails()
        {
            Console.Clear();
            ReadAndWriteDataBase u = new ReadAndWriteDataBase();
            Console.Write("{0}", "Your Account Number          : ");
            u.Account_Number = Int64.Parse(Console.ReadLine());
            u.Userdata();
        }
        public void shw()
        {
            Console.WriteLine("****Banking System | Account Details ****\n");
            //Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("|{0}|", "");
            Console.WriteLine("|{0}|", "Account Number            :  " + Account_Number);
            Console.WriteLine("|{0}|", "Account Holder's Name     :  " + Title + ". " + FName + " " + LName);
            Console.WriteLine("|{0}|", "Phone Number            :  " + Phone_Number);
            Console.WriteLine("|{0}|", "Address           :  " + Address);
            //Console.WriteLine("|{0}|", "Total Balance in account  :  " + "Rs. " + Total_Balance);
            //Console.WriteLine("|{0}|", "Last Login Details        :  " + LastLoginDetails);
            Console.WriteLine("|{0}|", "");
            //Console.BackgroundColor = ConsoleColor.Black;
            // Console.WriteLine("\n");
            Console.WriteLine("Press any key to go back to the previous menu!");
        }

        public void Passbook()
        {

            Console.Clear();
            Console.WriteLine("**** Banking System | My Passbook ****\n");
            //Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("{0}", "Please ReEnter your Account Number: ");
            User.Account_Number = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("|{0}|", "Transaction Amount    |" + "    Time and Date of Transaction    |" + "    Transaction Description");
            Console.WriteLine("|{0}|", "|".PadLeft(28) + "|".PadLeft(37));
            System.Data.SqlClient.SqlDataReader dataReader = User.ReadPassbook();
            while (dataReader.Read())
            {
                int Length = dataReader.GetValue(1).ToString().Length;
                int DescLength = dataReader.GetValue(3).ToString().Length;
                string Amount = dataReader.GetValue(1).ToString(), DateAndTime = dataReader.GetValue(2).ToString().PadLeft(27).PadRight(36), Description = dataReader.GetValue(3).ToString().PadLeft(DescLength + 8);
                Console.WriteLine("|{0}|", "Rs. " + Amount.PadRight(((14 - Length) + Length)) + "|" + DateAndTime + "|" + Description);
            }
            Console.WriteLine("|{0}|", "|".PadLeft(28) + "|".PadLeft(37));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("\n");
            Console.WriteLine("Press any key to go back to the previous menu!");
            dataReader.Close();
        }
    }
}
