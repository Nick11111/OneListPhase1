using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Helpers;
using System.Threading;
using System.Security.Principal;

namespace OneListApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            

        }
        public static class WebApiConfig
        {
            public static void Register(HttpConfiguration config)
            {
                config.MapHttpAttributeRoutes();

                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
            }
        }
        void Application_PostAuthenticateRequest()
        {
            if (User.Identity.IsAuthenticated)
            {
                var name = User.Identity.Name; // Get current user name.

                OneListCAEntities context = new OneListCAEntities();
                var user = context.AspNetUsers.Where(u => u.UserName == name).FirstOrDefault();
                IQueryable<string> roleQuery = from u in context.AspNetUsers
                                               from r in u.AspNetRoles
                                               where u.UserName == Context.User.Identity.Name
                                               select r.Name;
                string[] roles = roleQuery.ToArray();

                HttpContext.Current.User = Thread.CurrentPrincipal =
                                           new GenericPrincipal(User.Identity, roles);
            }
        }
    }
}
