using MediatR;
using TaskForge.Models;
using TaskForge.Repositories.Interface;

namespace TaskForge.General.ProjectManagement.Commands
{
    public class AddProjectCommand : IRequest<int>
    {
        public AddProjectRequest Project { get; set; }

        public AddProjectCommand(AddProjectRequest project)
        {
            Project = project;
        }
    }

    public class AddProjectCommandHandler : IRequestHandler<AddProjectCommand, int>
    {
        private readonly IProjectRepository _projectRepository;

        public AddProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<int> Handle(AddProjectCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Project.Name))
                throw new Exception("Project name is required.");

            var newId = await _projectRepository.AddProjectAsync(request.Project);
            return newId;
        }
    }
}