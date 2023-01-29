using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Utilities
{
    public class LogDB : ILogger
    {
        static string ConnectionString = "Integrated Security = SSPI; Persist Security Info = False; Initial Catalog = Northwind; Data Source = MSI\\\\SQLEXPRESS";
        
        public void InitDB()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                string queryString = "if not exists (Select * From LogDB) begin CREATE TABLE LogDB (\r\n int id identity,\r\n    Message nvarchar(100) NOT NULL,\r\n Type nvarchar(40) NOT NULL, Date Date not null, Exception nvarchar(90) end\r\n)";

                // Adapter
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    //Reader
                    command.ExecuteNonQuery();
                }
            }
        }
        public void AddToDB(string Type, string message, Exception? exce)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DateTime DateTime = DateTime.Now;
                string queryString;
                if (exce != null)
                {
                     queryString = $"insert into LogDB (Message, Type, Exception, Date) values ('@message','@type', @'exception', GETDATE())";
                }
                else
                {
                     queryString = $"insert into LogDB (Message, Type, Date) values ('@message','@type', GETDATE())";
                }

                // Adapter
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@type", Type);
                    command.Parameters.AddWithValue("@message", message);
                    if(exce != null)
                    {
                        command.Parameters.AddWithValue("@exception", exce.Message);
                    }
                    //Reader
                    command.ExecuteNonQuery();
                }
            }
        }
        public void Init()
        {
            InitDB();
        }

        public void LogCheckHouseKeeping()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                
                string queryString = "if exists (Select * from LogDB where Date < DATEADD(month,-3, GETDATE())) begin DELETE FROM LogDB WHERE Date < DATEADD(month, -3, GETDATE()) end";

                // Adapter
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    //Reader
                    command.ExecuteNonQuery();
                }
            }
        }

        public void LogError(string msg)
        {
            AddToDB("Error", msg, null);
        }

        public void LogEvent(string msg)
        {
            AddToDB("Event", msg, null);
        }

        public void LogException(string msg, Exception exce)
        {
            AddToDB("Exception", msg, exce);
        }
    }
}
