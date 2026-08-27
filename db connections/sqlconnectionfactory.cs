using Microsoft.Data.SqlClient;
using System.Data;

namespace TaskForge.DbConnection
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TaskForgeDb")
                ?? throw new Exception("Connection string 'TaskForgeDb' not found in appsettings.json");
        }

        public IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}