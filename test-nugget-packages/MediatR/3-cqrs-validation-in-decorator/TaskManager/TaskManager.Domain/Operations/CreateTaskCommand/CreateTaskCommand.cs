using FluentResults;
using MediatR;
using TaskManager.Entities;

namespace TaskManager.Domain.Operations.CreateTaskCommand
{
    public class CreateTaskCommand : IRequest<Result>
    {
        public TaskEntity Task { get; set; }
    }
}