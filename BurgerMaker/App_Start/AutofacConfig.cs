using Autofac;
using Autofac.Integration.Mvc;
using DataAccess;
using DataAccess.Interfaces;
using Repositories;
using Repositories.Interfaces;
using System.Web.Mvc;

namespace BurgerMaker.App_Start
{
    public class AutofacConfig
    {
        public static void Register()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<BurgerMakerDbContext>().As<IDbContext>().SingleInstance();
            builder.RegisterType<UserRepository>().As<IUserRepository>().SingleInstance();
            builder.RegisterType<IngredientTypeRepository>().As<IIngredientTypeRepository>().SingleInstance();
            builder.RegisterType<BunRepository>().As<IBunRepository>().SingleInstance();
            builder.RegisterType<IngredientRepository>().As<IIngredientRepository>().SingleInstance();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}