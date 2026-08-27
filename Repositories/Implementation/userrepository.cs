using Dapper;
using TaskForge.DbConnection;
using TaskForge.Models;
using TaskForge.Repositories.Interface;

namespace TaskForge.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> AddUserAsync(User user)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string query = @"
                INSERT INTO Users (FullName, Email, PasswordHash, Role, CreatedOn)
                OUTPUT INSERTED.Id
                VALUES (@FullName, @Email, @PasswordHash, @Role, GETDATE());";

            var newId = await connection.ExecuteScalarAsync<int>(query, user);
            return newId;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string query = "SELECT * FROM Users WHERE Email = @Email";
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string query = "SELECT * FROM Users WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { Id = id });
        }
    }
}