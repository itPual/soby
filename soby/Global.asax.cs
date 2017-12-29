using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using soby.Domain.Entities;
using soby.Util;
using soby.Util.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace soby
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());

            // внедрение зависимостей
            NinjectModule registrations = new NinjectRegistrations();
            var kernel = new StandardKernel(registrations);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            // Remove data annotations validation provider 
            ModelValidatorProviders.Providers.Remove(
                        ModelValidatorProviders.Providers.OfType<DataAnnotationsModelValidatorProvider>().First());
        }
    }
}
