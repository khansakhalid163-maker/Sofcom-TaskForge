using System.Reflection;
using TaskForge.DbConnection;
using TaskForge.Repositories.Implementation;
using TaskForge.Repositories.Interface;

namespace TaskForge.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection RegisterServicesToDI(this IServiceCollection services, IConfiguration configuration)
        {
            // DB Connection
            services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();

            // Repositories
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // MediatR - scans this assembly for all Commands/Queries/Handlers
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}