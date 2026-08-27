using TaskForge.Models;

namespace TaskForge.Repositories.Interface
{
    public interface IProjectRepository
    {
        Task<int> AddProjectAsync(AddProjectRequest request);
        Task<Project> GetProjectByIdAsync(int id);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
    }
}