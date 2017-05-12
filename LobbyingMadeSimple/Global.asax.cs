using Autofac;
using Autofac.Integration.Mvc;
using LobbyingMadeSimple.Core.Interfaces;
using LobbyingMadeSimple.Models;
using LobbyingMadeSimple.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LobbyingMadeSimple.DAL;

namespace LobbyingMadeSimple
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // AutoFac Setup and Configuration
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<IssueRepository>().As<IIssueRepository>();
            builder.RegisterType<VoteRepository>().As<IVoteRepository>();
            builder.RegisterType<ApplicationDbContext>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
