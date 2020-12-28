using Microsoft.Extensions.DependencyInjection;
using ObjetivesTracker.Repositories.Objetives;

namespace ObjetivesTracker.Repositories.Extensions
{
    public static class RepositoryModule
    {
        public static IServiceCollection RegisterRepositoryModule(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IObjetivesRepository, ObjetivesRepository>();

            return serviceCollection;
        }
    }
}
