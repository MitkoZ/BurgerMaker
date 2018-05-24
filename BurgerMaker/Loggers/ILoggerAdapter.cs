using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMaker.Loggers
{
    internal interface ILoggerAdapter
    {
        void Log(Exception message);
    }
}
