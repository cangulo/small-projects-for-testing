using Microsoft.Extensions.DependencyInjection;
using ObjetivesTracker.Contracts.Services;
using ObjetivesTracker.Domain.Services;

namespace ObjetivesTracker.Domain.Extensions
{
    public static class DomainModule
    {
        public static IServiceCollection RegisterDomainModule(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IObjectiveService, ObjectiveService>();

            return serviceCollection;
        }
    }
}