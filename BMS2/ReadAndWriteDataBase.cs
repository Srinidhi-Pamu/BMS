using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BMS2
{
    public class ReadAndWriteDataBase
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

        public ReadAndWriteDataBase()
        {
            String cs = "Data Source =INTEL\\SQLEXPRESS; Initial Catalog=BMS; Integrated Security=True;";
            SqlConnection con = new SqlConnection(cs);
            try
            {
                con.Open();
            }
            catch (SqlException)
            {
                Console.WriteLine("There is an error while establishing a connection with the SqlServer");
                Console.ReadKey();
            }
        }

        public Boolean UpdatePassbook(Int64 Account_Number, Double Amount, string Description)
        {
            String cs = "Data Source =INTEL\\SQLEXPRESS; Initial Catalog=BMS; Integrated Security=True;";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = null;
            Boolean successflag = false;
            cmd = new SqlCommand("insert into Passbook values(" + Account_Number + "," + Amount + ", SYSDATETIME(),'" + Description + "')", con);
            int count = cmd.ExecuteNonQuery();
            if (count > 0)
            {
                if (Description == "Withdrawal")
                {
                    cmd = new SqlCommand("update TransactionDetails set TotalBalance=TotalBalance-" + Amount + " where AccountNumber=" + Account_Number, con);
                    count = cmd.ExecuteNonQuery();
                }
                else if (Description == "Deposit")
                {
                    cmd = new SqlCommand("update TransactionDetails set TotalBalance=TotalBalance+" + Amount + " where AccountNumber=" + Account_Number, con);
                    count = cmd.ExecuteNonQuery();
                }
                else
                {
                    char l = 'T';
                    char k = 'F';
                    char p = Description[5];
                    if (p == l)
                    {
                        cmd = new SqlCommand("update TransactionDetails set TotalBalance=TotalBalance-" + Amount + " where AccountNumber=" + Account_Number, con);
                        count = cmd.ExecuteNonQuery();
                    }
                    else if (p == k)
                    {
                        cmd = new SqlCommand("update TransactionDetails set TotalBalance=TotalBalance+" + Amount + " where AccountNumber=" + Account_Number, con);
                        count = cmd.ExecuteNonQuery();
                    }

                }
                if (count > 1)
                {
                    successflag = true;
                    return successflag;
                }
            }
            return true;
        }

        public void WriteToDatabase(int choice)
        {
            String cs = "Data Source =INTEL\\SQLEXPRESS; Initial Catalog=BMS; Integrated Security=True;";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand command = new SqlCommand(GetQuery(choice), con);
            command.ExecuteNonQuery();
            SqlCommand cmd = new SqlCommand(GetQuery(5), con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public UInt64 GenerateAccountNumber()
        {
            SqlCommand Command = new SqlCommand(GetQuery(0));
            String cs = "Data Source =INTEL\\SQLEXPRESS; Initial Catalog=BMS; Integrated Security=True;";
            SqlConnection con = new SqlConnection(cs);
            Command.Connection = con;
            con.Open();
            SqlDataReader DataReader = Command.ExecuteReader();
            DataReader.Read();
            if (DataReader.GetValue(0) != null)
                Account_Number = Convert.ToInt64("" + DataReader.GetValue(0)) + 1;
            else
                Account_Number = 12081999;
            DataReader.Close();
            Command.Dispose();
            return (uint)Account_Number;

        }
        private string GetQuery(int i)
        {
            string[] Query = new string[] {
            "select max(AccountNumber) from Account  ",
            "select * from Account where AccountNumber = " + Account_Number,
            "insert into Account values(" + this.Account_Number + ",'" + this.Title + "','" + this.FName + "','" + this.LName + "'," + this.Phone_Number + ",'"+ this.Password + "','"+ this.Address +"')",
           // "update UserData set Title = '" + Title + "', Name = '" + FName + "', TotalBalance = " + Total_Balance + ", LastLoginDetails = SYSDATETIME() where AccountNumber = " + Account_Number,
           // "update UserData set TotalBalance = " + Total_Balance + " where AccountNumber = " + Account_Number,
            "select * from Passbook where AccountNumber = " + Account_Number,
            "select TotalBalance from TransactionDetails where AccountNumber = " + Account_Number ,
            "insert into TransactionDetails values(" + this.Account_Number + "," + this.Total_Balance + "," + "SYSDATETIME())"
        };
            return Query[i];
        }

        public bool ReadFromDatabase()
        {
            String cs = "Data Source =INTEL\\SQLEXPRESS; Initial Catalog=BMS; Integrated Security=True;";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            bool AccountFound = false;
            SqlCommand command = new SqlCommand(GetQuery(1), con);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                Title = ("" + dataReader.GetValue(1));
                FName = ("" + dataReader.GetValue(2));
                LName = ("" + dataReader.GetValue(3));
                //Total_Balance = Convert.ToDouble("" + dataReader.GetValue(3));
                Phone_Number = Convert.ToInt64(dataReader.GetValue(4));
                Password = ("" + dataReader.GetValue(5));
                Address = ("" + dataReader.GetValue(6));
                //LastLoginDetails = Convert.ToDateTime(dataReader.GetValue(5));
                AccountFound = true;
            }
            else
            {
                AccountFound = false;
            }
            dataReader.Close();
            command.Dispose();
            return AccountFound;
        }
        public void CloseConnection()
        {
            String cs = "Data Source =INTEL\\SQLEXPRESS; Initial Catalog=BMS; Integrated Security=True;";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            con.Close();
            con.Dispose();
        }

        public SqlDataReader ReadPassbook()
        {
            String cs = "Data Source =INTEL\\SQLEXPRESS; Initial Catalog=BMS; Integrated Security=True;";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand command = new SqlCommand(GetQuery(3), con);
            SqlDataReader dataReader = command.ExecuteReader();
            return dataReader;
        }

        public SqlDataReader UpdateBal()
        {
            String cs = "Data Source =A49\\SQLEXPRESS; Initial Catalog=BMS; Integrated Security=True;";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand command = new SqlCommand(GetQuery(6), con);
            SqlDataReader dataReader = command.ExecuteReader();
            return dataReader;
        }
        public Double getBalance()
        {
            String cs = "Data Source =INTEL\\SQLEXPRESS; Initial Catalog=BMS; Integrated Security=True;";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand command = new SqlCommand(GetQuery(4), con);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {

                Total_Balance = Convert.ToDouble("" + dataReader.GetValue(0));
            }
            return Total_Balance;

        }

        public void Userdata()
        {
            String cs = "Data Source =INTEL\\SQLEXPRESS; Initial Catalog=BMS; Integrated Security=True;";
            Account acc1 = new Account();
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand command = new SqlCommand(GetQuery(1), con);
            SqlDataReader dataReader = command.ExecuteReader();
            //Console.WriteLine("BEFORE LOOP\n");
            if (dataReader.Read())
            {

                acc1.Account_Number = Convert.ToInt64("" + dataReader.GetValue(0));
                acc1.Title = ("" + dataReader.GetValue(1));
                acc1.FName = ("" + dataReader.GetValue(2));
                acc1.LName = ("" + dataReader.GetValue(3));
                //acc1.Total_Balance = Convert.ToDouble("" + dataReader.GetValue(3));
                acc1.Phone_Number = Convert.ToInt64(dataReader.GetValue(4));
                acc1.Address = ("" + dataReader.GetValue(6));
            }

            dataReader.Close();
            command.Dispose();
            acc1.shw();
        }
    }
}
