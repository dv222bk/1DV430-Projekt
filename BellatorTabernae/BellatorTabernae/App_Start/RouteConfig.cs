using System.Web.Routing;

namespace BellatorTabernae
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("CreateUser", "nyanvändare", "~/Pages/CreateUser.aspx");
            routes.MapPageRoute("Leaderboard", "topplista", "~/Pages/Leaderboard.aspx");
            routes.MapPageRoute("BattleResult", "stridsrapport", "~/Pages/BattleResult.aspx");
            routes.MapPageRoute("Battle", "strid", "~/Pages/Battle.aspx");
            routes.MapPageRoute("Market", "torget", "~/Pages/Market.aspx");
            routes.MapPageRoute("Character", "karaktär", "~/Pages/Character.aspx");
            routes.MapPageRoute("OtherCharacter", "karaktär/{*charID}", "~/Pages/Character.aspx");
            routes.MapPageRoute("Chat", "chat", "~/Pages/Chat.aspx");
            routes.MapPageRoute("LevelUp", "level upp!", "~/Pages/Levelup.aspx");
            routes.MapPageRoute("Error", "serverfel", "~/Pages/Shared/Error.aspx");
            routes.MapPageRoute("Default", "", "~/Pages/Default.aspx");
        }
    }
}