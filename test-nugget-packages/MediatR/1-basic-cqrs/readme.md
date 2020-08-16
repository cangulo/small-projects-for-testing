# 1-basic-cqrs

This is the basic project, no validation for the request is implemented and all the dependency injection used the default .NET Core features.

Next are the two request available:

* **GetTaskQuery**: Encapsulate the request to get a specific Task. The solution will always returns the same task no matter what Id is provided (Check the [TaskRepository](./TaskManager/TaskManager.Repository/TaskRepository.cs)).

```csharp
public class GetTaskQuery : IRequest<Result<TaskEntity>>
{
    public int TaskId { get; set; }
}
```
<p align="center">
    <a href="./TaskManager/TaskManager.Domain/Operations/GetTaskQuery/GetTaskQuery.cs"><i>GetTaskQuery.cs</i></a>
</p>

* **CreateTaskCommand**: Provide a Task object to be saved in the system. As there is no database it won't be stored.

```csharp
public class CreateTaskCommand : IRequest<Result>
{
    public TaskEntity Task { get; set; }
}
```

<p align="center">
    <a href="./TaskManager/TaskManager.Domain/Operations/CreateTaskCommand/CreateTaskCommand.cs"><i>CreateTaskCommand.cs</i></a>
</p>

Then we handle each one in the next RequestHandler classes:

* CreateTaskCommandHandler

```csharp
public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result>
{
    private readonly ITaskRepository _taskRepository;

    public CreateTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
    }

    public async Task<Result> Handle(CreateTaskCommand request, CancellationToken cancellationToken) => await _taskRepository.CreateTask(request.Task);
}
```

<p align="center">
    <a href="./TaskManager/TaskManager.Domain/Operations/CreateTaskCommand/CreateTaskCommandHandler.cs"><i>CreateTaskCommandHandler.cs</i></a>
</p>



* GetTaskQueryHandler

```csharp
    public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, Result<TaskEntity>>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTaskQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        }

        public async Task<Result<TaskEntity>> Handle(GetTaskQuery request, CancellationToken cancellationToken) => await _taskRepository.GetTaskById(request.TaskId);
    }
```

<p align="center">
    <a href="./TaskManager/TaskManager.Domain/Operations/GetTaskQuery/GetTaskQueryHandler.cs"><i>GetTaskQueryHandler.cs</i></a>
</p>

As mention before, there is no validation for the incoming request. That will be included in the version of the project.