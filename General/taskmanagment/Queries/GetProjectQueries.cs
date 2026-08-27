using MediatR;
using TaskForge.Models;
using TaskForge.Repositories.Interface;

namespace TaskForge.General.ProjectManagement.Queries
{
    public class GetProjectByIdQuery : IRequest<Project>
    {
        public int Id { get; set; }
        public GetProjectByIdQuery(int id) => Id = id;
    }

    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Project>
    {
        private readonly IProjectRepository _projectRepository;
        public GetProjectByIdQueryHandler(IProjectRepository projectRepository) => _projectRepository = projectRepository;

        public async Task<Project> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            return await _projectRepository.GetProjectByIdAsync(request.Id);
        }
    }

    public class GetAllProjectsQuery : IRequest<IEnumerable<Project>>
    {
    }

    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<Project>>
    {
        private readonly IProjectRepository _projectRepository;
        public GetAllProjectsQueryHandler(IProjectRepository projectRepository) => _projectRepository = projectRepository;

        public async Task<IEnumerable<Project>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            return await _projectRepository.GetAllProjectsAsync();
        }
    }
}