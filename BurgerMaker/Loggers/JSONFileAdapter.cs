using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Hosting;

namespace BurgerMaker.Loggers
{
    public class JSONFileAdapter : ILoggerAdapter
    {
        private readonly StreamWriter streamWriter;
        public JSONFileAdapter()
        {
            string homePath = HostingEnvironment.ApplicationPhysicalPath;
            streamWriter = new StreamWriter(homePath + "\\" + "LogMessages.json", true);
        }

        public void Log(Exception exception)
        {
            streamWriter.WriteLine(JsonConvert.SerializeObject(exception));
            streamWriter.WriteLine();
            streamWriter.Close();
        }
    }
}