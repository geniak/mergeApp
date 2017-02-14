using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.OleDb;


namespace mergeApp
{
    class Program
    {



        static void Main(string[] args)
        {
			// test commit on Mac
			Console.WriteLine("Hi, This line was written on Mac");

			string conn1 =
			(@"Provider=Microsoft.ACE.OLEDB.12.0;" +
			 @"Data Source=test.accdb;" +
			 @"Mode=ReadWrite;" +
			 @"User Id=admin;Password=1234;");

			OleDbConnection conn = new OleDbConnection(conn1);
			OleDbCommand cmd = new OleDbCommand();

			try
			{
				conn.Open();
				Console.WriteLine("open");
				cmd.Connection = conn;
				cmd.CommandText = "Creat TABLE MyTable(Time1 text, Time2 text, Time3 text, WeekDay text, Path text, S_Time text)";
				cmd.ExecuteNonQuery();
				cmd.CommandText = "INSERT INTO MyTable(Time1, Time2, Time3, WeekDay, Path, S_Time) VALUE(@Time1, @Time2, @Time3, @WeekDay, @Path, @S_Time)";
				cmd.Parameters.Add(new OleDbParameter("@Time1", Time1));
				cmd.Parameters.Add(new OleDbParameter("@Time2", Time2));
				cmd.Parameters.Add(new OleDbParameter("@Time3", Time3));
				cmd.Parameters.Add(new OleDbParameter("@WeekDay", WeekDay[0] + WeekDay[1] + WeekDay[2] + WeekDay[3] + WeekDay[4] + WeekDay[5] + WeekDay[6]));
				cmd.Parameters.Add(new OleDbParameter("@Path", Path));
				cmd.Parameters.Add(new OleDbParameter("@S_Time", S_Time));
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				conn.Close();
			}

			Console.ReadLine();
        }
    }
}
