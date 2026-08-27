using Dapper;
using TaskForge.DbConnection;
using TaskForge.Models;
using TaskForge.Repositories.Interface;

namespace TaskForge.Repositories.Implementation
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public TaskRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> AddTaskAsync(AddTaskRequest request)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string query = @"
                INSERT INTO Tasks (Title, Description, ProjectId, AssignedToUserId, Priority, Status, CreatedOn)
                OUTPUT INSERTED.Id
                VALUES (@Title, @Description, @ProjectId, @AssignedToUserId, @Priority, 'Pending', GETDATE());";

            var newId = await connection.ExecuteScalarAsync<int>(query, request);
            return newId;
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string query = "SELECT * FROM Tasks WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<TaskItem>(query, new { Id = id });
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            const string query = "SELECT * FROM Tasks ORDER BY CreatedOn DESC";
            return await connection.QueryAsync<TaskItem>(query);
        }

        public async Task<bool> UpdateTaskAsync(UpdateTaskRequest request)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string query = @"
                UPDATE Tasks
                SET Title = @Title,
                    Description = @Description,
                    AssignedToUserId = @AssignedToUserId,
                    Priority = @Priority,
                    Status = @Status,
                    UpdatedOn = GETDATE()
                WHERE Id = @Id";

            var rowsAffected = await connection.ExecuteAsync(query, request);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string query = "DELETE FROM Tasks WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
}