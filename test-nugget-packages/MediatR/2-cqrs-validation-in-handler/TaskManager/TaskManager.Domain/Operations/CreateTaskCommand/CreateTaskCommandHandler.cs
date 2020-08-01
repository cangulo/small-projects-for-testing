using FluentResults;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Repository;

namespace TaskManager.Domain.Operations.CreateTaskCommand
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result>
    {
        private readonly ITaskRepository _taskRepository;

        public CreateTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository ?? throw new NullReferenceException(nameof(taskRepository));
        }

        public async Task<Result> Handle(CreateTaskCommand request, CancellationToken cancellationToken) => await _taskRepository.CreateTask(request.Task);
    }
}