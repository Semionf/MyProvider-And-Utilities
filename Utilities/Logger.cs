﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Net.Mime.MediaTypeNames;

namespace Utilities
{
    public class Logger
    {
        static MyILogger myLog;
        public static Queue<LogItem> myQueue = new Queue<LogItem>();

        private static Logger _logger = new Logger();

        public static Logger logger
        {
            get { return _logger; }
            set { _logger = value; }
        }

        public Logger()
        {
            
            switch (GetLogProvider())
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

        public static void AddToLog(LogItem item)
        {
            if(item != null)
            {
                myQueue.Enqueue(item);
            }
        }


        private string GetLogProvider()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            return config["LogProvider"];
        }
    }
}
