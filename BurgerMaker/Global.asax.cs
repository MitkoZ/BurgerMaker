using BurgerMaker.App_Start;
using BurgerMaker.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace BurgerMaker
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutofacConfig.Register();
            Helpers.AuthorizeAttribute authorizeAttribute = new Helpers.AuthorizeAttribute();
            GlobalFilters.Filters.Add(authorizeAttribute);
            GlobalFilters.Filters.Add(new ErrorHandlerAttribute());

        }
    }
}
