using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace BurgerMaker.Loggers
{
    internal class TextFileAdapter : ILoggerAdapter
    {
        private readonly StreamWriter streamWriter;
        public TextFileAdapter()
        {
            string homePath = HostingEnvironment.ApplicationPhysicalPath;
            streamWriter = new StreamWriter(homePath + "\\" + "LogMessages.txt", true);
        }

        public void Log(Exception exception)
        {
            streamWriter.WriteLine(exception);
            streamWriter.WriteLine();
            streamWriter.Close();
        }
    }
}