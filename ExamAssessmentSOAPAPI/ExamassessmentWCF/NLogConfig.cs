using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;

namespace LMS1701.EA.SOAPAPI
{
    public class NLogConfig
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
    }
}