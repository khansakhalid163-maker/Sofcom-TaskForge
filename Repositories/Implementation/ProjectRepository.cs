using Dapper;
using TaskForge.DbConnection;
using TaskForge.Models;
using TaskForge.Repositories.Interface;

namespace TaskForge.Repositories.Implementation
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ProjectRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> AddProjectAsync(AddProjectRequest request)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string query = @"
                INSERT INTO Projects (Name, Description, CreatedOn)
                OUTPUT INSERTED.Id
                VALUES (@Name, @Description, GETDATE());";

            var newId = await connection.ExecuteScalarAsync<int>(query, request);
            return newId;
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string query = "SELECT * FROM Projects WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Project>(query, new { Id = id });
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            const string query = "SELECT * FROM Projects ORDER BY CreatedOn DESC";
            return await connection.QueryAsync<Project>(query);
        }
    }
}