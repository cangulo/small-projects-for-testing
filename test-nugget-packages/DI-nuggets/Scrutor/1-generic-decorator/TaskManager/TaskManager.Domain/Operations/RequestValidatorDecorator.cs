using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
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
            if (validationResult.IsValid)
                return await _decorated.Handle(request, cancellationToken);

            var errors = validationResult.Errors
                            .Select(x => Result.Fail(x.ErrorMessage))
                            .ToArray();
            return Result.Merge(errors).ToGenericFailedResult<TResult>();
        }
    }
}
