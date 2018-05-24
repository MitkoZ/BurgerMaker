using BurgerMaker.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BurgerMaker.Helpers
{
    public class ErrorHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            ILoggerAdapter loggerAdapter = loggerFactory.GetLoggerAdapter();
            loggerAdapter.Log(filterContext.Exception);

            filterContext.Result = new ViewResult { ViewName = "ServerError" };
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}