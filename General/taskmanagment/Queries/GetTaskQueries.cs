using MediatR;
using TaskForge.Models;
using TaskForge.Repositories.Interface;

namespace TaskForge.General.TaskManagement.Queries
{
    public class GetTaskByIdQuery : IRequest<TaskItem>
    {
        public int Id { get; set; }
        public GetTaskByIdQuery(int id) => Id = id;
    }

    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskItem>
    {
        private readonly ITaskRepository _taskRepository;
        public GetTaskByIdQueryHandler(ITaskRepository taskRepository) => _taskRepository = taskRepository;

        public async Task<TaskItem> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            return await _taskRepository.GetTaskByIdAsync(request.Id);
        }
    }

    public class GetAllTasksQuery : IRequest<IEnumerable<TaskItem>>
    {
    }

    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, IEnumerable<TaskItem>>
    {
        private readonly ITaskRepository _taskRepository;
        public GetAllTasksQueryHandler(ITaskRepository taskRepository) => _taskRepository = taskRepository;

        public async Task<IEnumerable<TaskItem>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            return await _taskRepository.GetAllTasksAsync();
        }
    }
}