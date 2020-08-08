using FluentValidation;

namespace TaskManager.Domain.Operations.GetTaskQuery
{
    public class GetTaskQueryValidator : AbstractValidator<GetTaskQuery>
    {
        public GetTaskQueryValidator()
        {
            RuleFor(getTaskQuery => getTaskQuery.TaskId)
                .GreaterThan(0).WithMessage("TaskId should be greater than 0");
        }
    }
}