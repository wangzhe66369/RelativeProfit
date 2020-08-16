using System;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using log4net.Config;
using log4net;

namespace CalculateStock.Common.Log
{
    public class LogHelper
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("Loggering");

        public void LogError(string message)
        {
            log.Error(message);
        }
    }
}
