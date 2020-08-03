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

        public GetTaskQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        }

        public async Task<Result<TaskEntity>> Handle(GetTaskQuery request, CancellationToken cancellationToken) => await _taskRepository.GetTaskById(request.TaskId);
    }
}