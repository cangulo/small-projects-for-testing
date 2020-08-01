using FluentResults;

namespace TaskManager.Domain.Operations
{
    public interface IValidator<in TRequest>
    {
        Result Validate(TRequest request);
    }
}
