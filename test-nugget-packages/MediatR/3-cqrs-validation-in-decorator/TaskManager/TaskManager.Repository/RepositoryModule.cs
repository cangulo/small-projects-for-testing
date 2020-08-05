using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.Repository
{
    public static class RepositoryModule
    {
        public static void AddRepositoryClasses(this IServiceCollection services)
        {
            services.AddTransient<ITaskRepository, TaskRepository>();
        }
    }
}