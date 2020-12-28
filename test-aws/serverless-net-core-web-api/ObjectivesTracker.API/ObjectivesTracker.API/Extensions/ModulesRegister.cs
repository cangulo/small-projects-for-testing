using Microsoft.Extensions.DependencyInjection;
using ObjetivesTracker.Domain.Extensions;
using ObjetivesTracker.Repositories.Extensions;

namespace ObjectivesTracker.API.Extensions
{
    public static class ModulesRegister
    {
        public static IServiceCollection RegisterProjectModules(this IServiceCollection serviceCollection)
        {
            serviceCollection.RegisterDomainModule();
            serviceCollection.RegisterRepositoryModule();
            return serviceCollection;
        }
    }
}