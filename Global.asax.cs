using MySql.Data.Entity;
using MySQLApp.Infrastructure;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MySQLApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
            //DependencyResolver.SetResolver(new NinjectDI());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
