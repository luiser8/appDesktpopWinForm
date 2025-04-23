using System.Configuration;
using System.Data.SqlClient;

namespace InventoryApp
{
    public static class ConnectionManager
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["Inventario"].ToString();

        public static SqlConnection GetConnection()
        {
            SqlConnection conexion = new SqlConnection(connectionString);
            return conexion;
        }
    }
}
