using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BurgerMaker.Loggers
{
    internal class LoggerFactory
    {
        private readonly string logDestination = "jsonFile";
        internal ILoggerAdapter GetLoggerAdapter()
        {
            switch (logDestination)
            {
                case "jsonFile":
                    {
                        return new JSONFileAdapter();
                    }
                case "txtFile":
                    {
                        return new TextFileAdapter();
                    }
                default:
                    throw new InvalidLoggerAdapterException("The chosen logger type is invalid!");
            }
        }
    }
}