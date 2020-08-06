using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Domain.Extensions;

namespace TaskManager.Domain.Operations
{
    public class RequestValidatorDecorator<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult> where TResult : class
    {
        private readonly IValidator<TRequest> _validator;
        private readonly IRequestHandler<TRequest, TResult> _decorated;

        public RequestValidatorDecorator(IValidator<TRequest> validator, IRequestHandler<TRequest, TResult> decorated)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _decorated = decorated ?? throw new ArgumentNullException(nameof(decorated));
        }

        public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (validationResult.IsFailed)
            {
                return validationResult.ToGenericFailedResult<TResult>();
            }
            return await _decorated.Handle(request, cancellationToken);
        }
    }
}
