using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class LogFile:ILogger
    {
        
        static int MAX_SIZE = 5000000;
        static string FILE_NAME = "Log";
        static string Original_File_Name = "Log";
        static string Ending = ".txt";
        static int counter = 0;
      
        public void Init()
        {
            
        }
        public void AddToFile(string message, Exception? exce)
        {
            DateTime DateTime = DateTime.Now;
            using (StreamWriter sw = new StreamWriter(FILE_NAME))
            {
                if (exce == null)
                {
                    sw.WriteLine($"{DateTime.Day}/{DateTime.Month}/{DateTime.Year} - {DateTime.Hour}:{DateTime.Minute} - {message}");
                }
                else
                {
                    sw.WriteLine($"{DateTime.Day}/{DateTime.Month}/{DateTime.Year} - {DateTime.Hour}:{DateTime.Minute} - {message} - Exception: {exce.Message}");
                }
            }
        }
        public void LogCheckHouseKeeping()
        {
            FileInfo fileInfo= new FileInfo(Original_File_Name + Ending);
            while (fileInfo.Exists)
            {
                if (fileInfo.Length >= MAX_SIZE)
                {
                    counter++;
                    FILE_NAME = Original_File_Name + counter.ToString() + Ending;
                    fileInfo= new FileInfo(FILE_NAME);
                }
                else
                {
                    return;
                }
            }
        }

        public void LogError(string msg)
        {
            msg = "Error: " + msg;
           AddToFile(msg,null);
        }

        public void LogEvent(string msg)
        {
            msg = "Event: " + msg;
           AddToFile(msg, null);
        }

        public void LogException(string msg, Exception exce)
        {
            msg = "Exception occurred at: " + msg;
            AddToFile(msg, exce);
        }
    }
}
