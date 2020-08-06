using FluentValidation;
using System;

namespace TaskManager.Domain.Operations.CreateTaskCommand
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {

        private readonly int MaxTitleLength = 200;

        public CreateTaskCommandValidator()
        {
            RuleFor(createTaskCommand => createTaskCommand.Task.TodoDate)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Todo Date should be in the future");
            RuleFor(createTaskCommand => createTaskCommand.Task.Title)
                .NotEmpty().WithMessage("Title should not be empty")
                .MaximumLength(MaxTitleLength).WithMessage($"Title should be shorter than {MaxTitleLength}");
        }
    }
}