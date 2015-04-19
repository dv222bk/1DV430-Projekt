using System.Web.Routing;

namespace BellatorTabernae
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("Error", "serverfel", "~/Pages/Shared/Error.aspx");
            routes.MapPageRoute("Default", "", "~/Pages/Default.aspx");
        }
    }
}