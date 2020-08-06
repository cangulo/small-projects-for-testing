using FluentResults;
using MediatR;
using TaskManager.Entities;

namespace TaskManager.Domain.Operations.GetTaskQuery
{
    public class GetTaskQuery : IRequest<Result<TaskEntity>>
    {
        public int TaskId { get; set; }
    }
}