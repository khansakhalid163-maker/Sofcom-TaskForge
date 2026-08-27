using TaskForge.Models;

namespace TaskForge.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<int> AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(int id);
    }
}