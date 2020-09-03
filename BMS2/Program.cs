using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Banking_system obj = new Banking_system();
            Customer cs = new Customer();
            Account acc = new Account();
            do
            {
                switch (obj.MainMenu())
                {
                    case 1:
                        cs.LogIn();
                        break;
                    case 2:
                        acc.SignUp();
                        break;
                    case 3:
                        Console.WriteLine("\n");
                        Console.WriteLine("Thanks for using our service!");
                        Console.WriteLine("Press any key to close the console!");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\n");
                        Console.WriteLine("Incorrect Option | Try Again!");
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            } while (true);
        }
    }
}
