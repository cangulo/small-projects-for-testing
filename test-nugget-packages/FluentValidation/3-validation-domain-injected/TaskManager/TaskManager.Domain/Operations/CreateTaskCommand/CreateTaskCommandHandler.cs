using FluentResults;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Repository;

namespace TaskManager.Domain.Operations.CreateTaskCommand
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IValidator<CreateTaskCommand> _validator;

        public CreateTaskCommandHandler(ITaskRepository taskRepository, IValidator<CreateTaskCommand> validator)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<Result> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (validationResult.IsValid)
                return await _taskRepository.CreateTask(request.Task);

            var errors = validationResult
                                .Errors
                                .Select(x => Result.Fail(x.ErrorMessage))
                                .ToArray();
            return Result.Merge(errors);
        }
    }
}