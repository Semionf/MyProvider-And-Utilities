using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
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
                    break;
                default:
                    myLog = new LogNone();
                    break;
            }
            myLog.Init();
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
