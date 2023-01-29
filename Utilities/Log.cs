﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Log
    {
        public static ILogger myLog;
      
        public Log(string provider)
        {
            switch (provider)
            {
                case "File":
                    myLog = new LogFile();
                    break;
                case "DB":
                    myLog = new LogDB();
                    break;
                case "Console":
                    myLog = new LogConsole();
                    break;
                default:
                    myLog = new LogNone();
                    break;
            }
        }
    }
}