
namespace Piñata.Data
{
    public static class Provider
    {
        private const string MSSQLServerClient = "System.Data.SqlClient";
        private const string MySqlClient = "MySql.Data.MySqlClient";

        public static string MSSQLServer
        {
            get { return MSSQLServerClient; }
        }

        public static string MySql
        {
            get { return MySqlClient; }
        }
    }
}