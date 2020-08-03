using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager.Domain.Operations.CreateTaskCommand
{
    public class CreateTaskCommandValidator : IValidator<CreateTaskCommand>
    {
        private readonly int MaxTitleLength = 200;
        public Result Validate(CreateTaskCommand request)
        {
            var errors = new List<string>();
            if (request.Task.TodoDate < DateTime.UtcNow)
                errors.Add("Todo Date should be in the future");

            if (string.IsNullOrEmpty(request.Task.Title))
                errors.Add("Title should not be empty");

            if (request.Task.Title.Length > MaxTitleLength)
                errors.Add($"Title should be shorter than {MaxTitleLength}");

            return errors.Count == 0 ? Result.Ok() : Result.Merge(errors.Select(x => Result.Fail(x)).ToArray());
        }
    }
}