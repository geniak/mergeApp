﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.OleDb;
// Microsoft ADO Ext. 6.0 DLL and Security
using ADOX;
using System.IO;
using Microsoft.Office.Interop.Access.Dao;
using System.Data;


namespace mergeApp
{
    class Program
    {



        static void Main(string[] args)
        {
			// test commit on Mac
			Console.WriteLine("Hi, This line was written on Mac");
            int answer = 0;
            while (answer == 0)
            {
                answer = answerMachine();
                if (answer == 1)
                {
                    createDB();
                }
            }
            Console.WriteLine("Checking File on System...");
            string[] filePath = checkFile();
            Console.Write("There is {0} DB file. Choose Number : ", filePath.Count());
            
            try
            {
                int selectNum = Int16.Parse(Console.ReadLine());
                checkData(filePath, selectNum);
            }
            catch (Exception err)
            {
                Console.WriteLine("Error : {0}", err);
                Console.Write("There is {0} DB file. Choose Number : ", filePath.Count());
            }
            
            
            Console.Read();
        }

        static int answerMachine()
        {
            Console.Write("Do you want Create Standard Database file?( [Y]es or [N]o : ");
            string answer = Console.ReadLine();
            answer.ToLower();
            int retVal;
            
            if (answer == "yes" || answer == "y")
            {
                retVal = 1;
            }
            else if (answer == "no" || answer == "n")
            {
                retVal = 2;
            }
            else
            {
                Console.WriteLine("Your answer is not available.");
                retVal = 0;
               
            }
            
            return retVal;

        }

        static void createDB()
        {
            Console.Write("Create DB Name : ");
            string txtDB = Console.ReadLine();
            Catalog myCatalog = new Catalog();
            string strProvider = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                                 "Data Source=" + txtDB.ToString() + ".accdb";

            try
            {
                // if "error system.runtime.interopservices.comexception: Class does not exist" error occurred change platform target on 'option - build - platform target' to x86 or x64
                myCatalog.Create(strProvider);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : {0}", ex);
            }


            string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                                 "Data Source=" + txtDB.ToString() + ".accdb; Persist Security Info=False";

            OleDbConnection conn = new OleDbConnection(connStr);
            conn.Open();

            string sql = "CREATE TABLE Student(ID text(8) NOT NULL, NAME text(10) NOT NULL, PHONE text(16) )";
            OleDbCommand comm = new OleDbCommand(sql, conn);

            try
            {
                int x = comm.ExecuteNonQuery();
                if (x == 0)
                    Console.WriteLine("정상적으로 Student Table이 생성되었습니다");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "MakeTable");
            }
            conn.Close();
        }
        
        static void connectDB()
        {
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
				//cmd.Parameters.Add(new OleDbParameter("@Time1", Time1));
				//cmd.Parameters.Add(new OleDbParameter("@Time2", Time2));
				//cmd.Parameters.Add(new OleDbParameter("@Time3", Time3));
				//cmd.Parameters.Add(new OleDbParameter("@WeekDay", WeekDay[0] + WeekDay[1] + WeekDay[2] + WeekDay[3] + WeekDay[4] + WeekDay[5] + WeekDay[6]));
				//cmd.Parameters.Add(new OleDbParameter("@Path", Path));
				//cmd.Parameters.Add(new OleDbParameter("@S_Time", S_Time));
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

        }

		static string[] checkFile()
		{
            string filePath = Directory.GetCurrentDirectory();
            Console.WriteLine(filePath);
            string searchPattern = "*.accdb";
            string[] searchFile = Directory.GetFiles(filePath, searchPattern);
            
            if (searchFile.Count() != 0)
            {
                Console.WriteLine("Find file");
                
                for (int i = 0; i < searchFile.Count(); i++)
                {
                    Console.WriteLine("[{0}] {1}", (i+1), searchFile[i]);
                }
                return searchFile;
            }
            else
            {
                Console.WriteLine("There is no such file");
                return null;
            }
            
		}

		static void checkData(string[] FilePath, int dbNum)
		{
            string conInfo =
            (@"Provider=Microsoft.ACE.OLEDB.12.0;" +
             @"Data Source=" + FilePath[dbNum-1] +";" +
             @"Mode=ReadWrite;");

            OleDbConnection conn = new OleDbConnection(conInfo);
            OleDbCommand cmd = new OleDbCommand();

            DataSet ds = new DataSet();
            OleDbDataAdapter adapter = new OleDbDataAdapter("SHOW TABLE", conInfo);

            conn.Open();
            Console.WriteLine("DB opened");
            cmd.Connection = conn;

            adapter.Fill(ds);





            /*
            cmd.CommandText = "SHOW TABLE";
            cmd.CommandText = "SELECT * FROM PHONE";
            OleDbDataReader readDB = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (readDB.Read())
            {
                Console.WriteLine("Read success");
            }
            //cmd.CommandText = 
            */

            conn.Close();
        }

        // sql 명령어
        // Database create/show
        // mysql> CREATE DATABASE dbname;
        // mysql> SHOW DATABASE;
        // mysql> USE dbname; // declare specific database
        // DROP DATABASE [IF EXISTS] dbname; // delete database

        // Create & Show Table
        // mysql> CREATE TABLE tablename (
        // column_name1 INT PRIMARY KEY AUTO_INCREMENT,
        // column_name2 VARCHAR(15) NOT NULL,
        // column_name3 INT
        // ) ENGINE=INNODB;
        // mysql> SHOW TABLES;
        // mysql> EXPLAIN tablesname;
        // mysql> DESCRIBE tablename;
        // mysql> RENAME TABLE tablename1 TO tablename2[, tablename3 TO tablename4];
        // mysql> DROP TABLE [IF EXISTS] tablename;

        // Insert
        // mysql> INSERT INTO tablename VALUES(value1, value2, ...);
        // mysql> INSERT INTO tablename (col1, col2, ...) VALUES(value1, value2, ...);

        // Select
        // mysql> SELECT col1, col2, ... FROM tablename;
        // you can use * instead of col, it means select all column.
        // mysql> SELECT col1 AS 'Name', col2 AS 'Score' FROM grade;
        // mysql> SELECT * FROM tablename ORDER BY col1 DESC;
        // mysql> SELECT col1, korean + math english AS 'Total Score' FROM tablename ORDER BY 'Total Score' ASC;
        // DESC : descending order, ASC : ascending order;
        // mysql> SELECT * FROM grade WHERE korean < 90;
        // mysql> SELECT * FROM grade LIMIT 10;
        // take 10 result from first
        // mysql> SELECT * FROM grade LIMIT 100, 10;
        // take 10 result from 100th. ( records are start from 0 )

        // Update
        // mysql> UPDATE tablename SET col1=newvalue WHERE condition

        // Delete
        // mysql> DELETE FROM tablename WHERE condition
    }
}
