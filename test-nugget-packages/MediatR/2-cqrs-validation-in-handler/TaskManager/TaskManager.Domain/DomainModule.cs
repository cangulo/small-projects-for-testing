using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Domain.Operations;
using TaskManager.Domain.Operations.CreateTaskCommand;
using TaskManager.Domain.Operations.GetTaskQuery;

namespace TaskManager.Domain
{
    public static class DomainModule
    {
        public static void AddMediatRClasses(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DomainModule));
            services.AddTransient<IValidator<CreateTaskCommand>, CreateTaskCommandValidator>();
            services.AddTransient<IValidator<GetTaskQuery>, GetTaskQueryValidator>();
        }
    }
}