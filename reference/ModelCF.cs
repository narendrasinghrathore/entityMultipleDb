namespace reference.EF.CF
{
    using System.Data.Entity;
    [DbConfigurationType(typeof(ModelCodeFistConfig))]
    public partial class ModelCF : DbContext
    {
        /// <summary>
        /// DbContext takes connection string and provider name.
        /// </summary>
        /// <param name="connection">string: Connection string</param>
        /// <param name="providerName">string: Provider name.</param>
        public ModelCF(string connection, string providerName)
            : base(connection)
        {
            if (providerName == "MySql.Data.MySqlClient")
            {

                Database.DefaultConnectionFactory = new MySql.Data.Entity.MySqlConnectionFactory();
            }
            if (providerName == "System.Data.SqlClient")
            {
                System.Data.Common.DbProviderFactories.GetFactory(providerName);
                Database.DefaultConnectionFactory = new System.Data.Entity.Infrastructure.SqlConnectionFactory();
            }
        }

        public virtual DbSet<yourModelClass> yourModelClass { get; set; }
       

    public class ModelCodeFistConfig : DbConfiguration
    {
        /// <summary>
        /// Used on app initilization only*
        /// </summary>
        public ModelCodeFistConfig()
        {
            #region SetDefaultConnectionFactory and Provider services
            
            #endregion
        }
    }
}
