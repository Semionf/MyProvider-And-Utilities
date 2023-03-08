using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LoggingLibrary.LogFile;
using static System.Net.Mime.MediaTypeNames;

namespace MyUtilities
{
    public class Logger
    {
        static MyILogger myLog;
        public static Queue<LogItem> myQueue = new Queue<LogItem>();

        public Logger(string provider)
        {

            switch (provider)
            {
                case "File":
                    myLog = new LogFile();
                    myLog.Init();
                    break;
                case "DB":
                    myLog = new LogDB();
                    myLog.Init();
                    break;
                case "Console":
                    myLog = new LogConsole();
                    myLog.Init();
                    break;
                default:
                    myLog = new LogNone();
                    myLog.Init();
                    break;
            }
          
        }

        public void AddToLog(LogItem item)
        {
 
            if(item != null)
            {
                myQueue.Enqueue(item);
            }
           
        }

    }
}
