
using Autofac;
using MediatR;
using TaskManager.Domain.Operations;

namespace TaskManager.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterGenericDecorator(typeof(RequestValidatorDecorator<,>), typeof(IRequestHandler<,>));
        }
    }
}