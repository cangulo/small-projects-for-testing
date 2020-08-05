using FluentResults;

namespace TaskManager.Domain.Operations.GetTaskQuery
{
    public class GetTaskQueryValidator : IValidator<GetTaskQuery>
    {
        public Result Validate(GetTaskQuery request)
        {
            if (request.TaskId < 1)
                return Result.Fail("TaskId should be greater than 0");

            return Result.Ok();
        }
    }
}