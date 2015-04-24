using System.Data.SqlClient;
using System.Web.Configuration;

namespace BellatorTabernae.Model.DAL
{
    public abstract class DALBase
    {
        private static string _connectionString;

        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        static DALBase()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["BellatorTabernaeConnectionString"].ConnectionString;
        }
    }
}