using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Helpers
{
    public static class LogHelper
    {
        public static Logger Log = LogManager.GetCurrentClassLogger();
    }
}