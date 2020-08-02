using FluentResults;
using System;

namespace TaskManager.Domain.Operations.CreateTaskCommand
{
    public class CreateTaskCommandValidator : IValidator<CreateTaskCommand>
    {
        private readonly int MaxTitleLength = 200;
        public Result Validate(CreateTaskCommand request)
        {
            var result = Result.Ok();
            if (request.Task.TodoDate < DateTime.UtcNow)
                result = Result.Fail("Todo Date should be in the future");

            if (string.IsNullOrEmpty(request.Task.Title) || )
                result = Result.Merge(result, Result.Fail("Title should not be empty"));

            if (request.Task.Title.Length > MaxTitleLength)
                result = Result.Merge(result, Result.Fail($"Title should be shorter than ${MaxTitleLength}"));

            return result;
        }
    }
}