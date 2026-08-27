using MediatR;
using TaskForge.Models;
using TaskForge.Repositories.Interface;

namespace TaskForge.General.TaskManagement.Commands
{
    public class UpdateTaskCommand : IRequest<bool>
    {
        public UpdateTaskRequest Task { get; set; }
        public UpdateTaskCommand(UpdateTaskRequest task) => Task = task;
    }

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, bool>
    {
        private readonly ITaskRepository _taskRepository;
        public UpdateTaskCommandHandler(ITaskRepository taskRepository) => _taskRepository = taskRepository;

        public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var existing = await _taskRepository.GetTaskByIdAsync(request.Task.Id);
            if (existing == null)
                throw new Exception("Task not found.");

            return await _taskRepository.UpdateTaskAsync(request.Task);
        }
    }

    public class DeleteTaskCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteTaskCommand(int id) => Id = id;
    }

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly ITaskRepository _taskRepository;
        public DeleteTaskCommandHandler(ITaskRepository taskRepository) => _taskRepository = taskRepository;

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            return await _taskRepository.DeleteTaskAsync(request.Id);
        }
    }
}