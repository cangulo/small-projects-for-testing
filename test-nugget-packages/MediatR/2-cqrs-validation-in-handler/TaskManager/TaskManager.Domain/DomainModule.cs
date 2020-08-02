using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Domain.Operations;
using TaskManager.Domain.Operations.CreateTaskCommand;

namespace TaskManager.Domain
{
    public static class DomainModule
    {
        public static void AddMediatRClasses(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DomainModule));
            services.AddTransient<IValidator<CreateTaskCommand>, CreateTaskCommandValidator>();
        }
    }
}