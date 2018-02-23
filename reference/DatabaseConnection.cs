namespace reference.Common.Helper
{
    using System.Configuration;
    using System.Data.SqlClient;

    public class DataBaseConnection
    {
        private static string _connectionName = string.Empty;
        /// <summary>
        /// Database Connection used for authentication, get database connection and maybe user menu.
        /// </summary>
        private static string _defaultConnection = ConfigurationManager.AppSettings["loginConnectionString"];
       
        /// <summary>
        /// Get dynamic generated entity connection string
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return _connectionName;
            }
        }
        /// <summary>
        /// Used for getting default data before selection of databse connections.
        /// Config server.
        /// </summary>
        public static string DefaultConnectionString
        {
            get
            {
                return "name=" + _defaultConnection;
            }
        }
        /// <summary>
        /// Generate database connection string
        /// </summary>
        /// <param name="serverName">string: IP address</param>
        /// <param name="databaseName">string: Catalog name</param>
        /// <param name="providerName">string: ProviderName</param>
        /// <param name="databaseUserId">string user id</param>
        /// <param name="databasePassword">string: user password</param>
        /// <param name="databaseTimeout">string: database timeout</param>
        public static string GenerateEntityConnectionString(string serverName, string databaseName, string providerName,
          string databaseUserId, string databasePassword, short? databaseTimeout)
        {
            string conn = string.Empty;
            switch (providerName)
            {
                case "System.Data.SqlClient":
                    conn = new SqlConnectionStringBuilder()
                    {

                        UserID = databaseUserId,
                        Password = databasePassword,
                        DataSource = serverName,
                        IntegratedSecurity = false,
                        InitialCatalog = databaseName

                    }.ConnectionString;

                    break;
                case "MySql.Data.MySqlClient":
                    conn = new MySql.Data.MySqlClient.MySqlConnectionStringBuilder()
                    {
                        Server = serverName,
                        UserID = databaseUserId,
                        Password = databasePassword,
                        Database = databaseName,
                        PersistSecurityInfo = true

                    }.ConnectionString;
                    break;
                default:
                    break;
            }

            return conn;
        }


        /// <summary>
        /// Return DbContext based on providerName.
        /// </summary>
        /// <param name="connectionString">string: Connection string.</param>
        /// <param name="providerName">string: Provider Name ( MySql.Data.MySqlClient/ System.Data.SqlClient )</param>
        /// <returns>DbContext: return new DbContext()</returns>
        public static EF.CF.ModelCF getContext(string connectionString, string providerName)
        {

            EF.CF.ModelCF db = null;
            db = new EF.CF.ModelCF(connectionString, providerName);
            return db;
        }
    }
}
