using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class LogConsole : ILogger
    {
        public void PrintMessage(string message, Exception? exce)
        {
            DateTime DateTime = DateTime.Now;
            if (exce == null)
            {
                Console.WriteLine($"{DateTime.Day}/{DateTime.Month} - {DateTime.Hour}:{DateTime.Minute} - {message}");
            }
            else
            {
                Console.WriteLine($"{DateTime.Day}/{DateTime.Month} - {DateTime.Hour}:{DateTime.Minute} - {message}, {exce.Message}");
            }
        }
        public void Init()
        {
           
        }

        public void LogCheckHouseKeeping()
        {
           
        }

        public void LogError(string msg)
        {
            msg = "Error: " + msg;
            PrintMessage(msg,null);
        }

        public void LogEvent(string msg)
        {
            msg = "Event: " + msg;
            PrintMessage(msg, null);
        }

        public void LogException(string msg, Exception exce)
        {
            msg = "Exception occurred at: " + msg;
            PrintMessage(msg, exce);
        }
    }
}
