using FluentResults;
using System;
using System.Threading.Tasks;
using TaskManager.Entities;

namespace TaskManager.Repository
{
    public class TaskRepository : ITaskRepository
    {
        public Task<Result> CreateTask(TaskEntity task)
        {
            return Task.FromResult(Result.Ok());
        }

        public Task<Result<TaskEntity>> GetTaskById(int taskId)
        {
            return Task.FromResult(
                        Result.Ok<TaskEntity>(
                            new TaskEntity
                            {
                                TaskId = 1,
                                Title = "Remember the milk",
                                TodoDate = DateTime.UtcNow
                            }));
        }
    }
}