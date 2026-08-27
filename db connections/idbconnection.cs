using System.Data;

namespace TaskForge.DbConnection
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}