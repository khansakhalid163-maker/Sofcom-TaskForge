using MediatR;
using TaskForge.Models;
using TaskForge.Repositories.Interface;

namespace TaskForge.General.TaskManagement.Commands
{
    // The Command - carries the data
    public class AddTaskCommand : IRequest<int>
    {
        public AddTaskRequest Task { get; set; }

        public AddTaskCommand(AddTaskRequest task)
        {
            Task = task;
        }
    }

    // The Handler - contains the actual logic
    public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, int>
    {
        private readonly ITaskRepository _taskRepository;

        public AddTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<int> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Task.Title))
                throw new Exception("Task title is required.");

            var newId = await _taskRepository.AddTaskAsync(request.Task);
            return newId;
        }
    }
}