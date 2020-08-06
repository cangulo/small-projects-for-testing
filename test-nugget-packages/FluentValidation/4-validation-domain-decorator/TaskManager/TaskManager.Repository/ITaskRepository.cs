using FluentResults;
using System.Threading.Tasks;
using TaskManager.Entities;

namespace TaskManager.Repository
{
    public interface ITaskRepository
    {
        Task<Result> CreateTask(TaskEntity task);

        Task<Result<TaskEntity>> GetTaskById(int taskId);
    }
}