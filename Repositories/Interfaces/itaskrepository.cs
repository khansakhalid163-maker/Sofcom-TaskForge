using TaskForge.Models;

namespace TaskForge.Repositories.Interface
{
    public interface ITaskRepository
    {
        Task<int> AddTaskAsync(AddTaskRequest request);
        Task<TaskItem> GetTaskByIdAsync(int id);
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task<bool> UpdateTaskAsync(UpdateTaskRequest request);
        Task<bool> DeleteTaskAsync(int id);
    }
}