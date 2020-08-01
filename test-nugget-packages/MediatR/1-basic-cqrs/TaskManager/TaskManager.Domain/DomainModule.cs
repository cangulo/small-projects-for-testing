using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.Domain
{
    public static class DomainModule
    {
        public static void AddMediatRClasses(this IServiceCollection services) => services.AddMediatR(typeof(DomainModule));
    }
}