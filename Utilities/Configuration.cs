using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Configuration
    {
        public static string GetCString()
        {
            try
            {
                Logger.AddToLog(new LogItem { Message = "Getting connection string", Type = "Event" });
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
                return config.GetConnectionString("MY_CS");
            }
            catch (Exception ex)
            {
                Logger.AddToLog(new LogItem { exception = ex, Message = ex.Message, Type = "Exception" });
                throw;
            }
        }

        // Runs Command with single value result
        public static object RunCommandResultSingleValue(string SqlQuery)
        {
            try
            {
                Logger.AddToLog(new LogItem { Message = "Runs Command with single value result", Type = "Event" });
                object result;
                using (SqlConnection connection = new SqlConnection(GetCString()))
                {
                    // Adapter
                    using (SqlCommand command = new SqlCommand(SqlQuery, connection))
                    {
                        connection.Open();
                        result = command.ExecuteScalar();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.AddToLog(new LogItem { exception = ex, Message = ex.Message, Type = "Exception" });
                throw;
            }
        }
    }
}
