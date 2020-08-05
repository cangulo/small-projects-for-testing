using FluentResults;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Entities;
using TaskManager.Repository;

namespace TaskManager.Domain.Operations.GetTaskQuery
{
    public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, Result<TaskEntity>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IValidator<GetTaskQuery> _validator;

        public GetTaskQueryHandler(ITaskRepository taskRepository, IValidator<GetTaskQuery> validator)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<Result<TaskEntity>> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (validationResult.IsFailed)
                return validationResult.ToResult<TaskEntity>();

            return await _taskRepository.GetTaskById(request.TaskId);
        }
    }
}