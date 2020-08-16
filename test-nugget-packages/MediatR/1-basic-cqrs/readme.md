# 1-basic-cqrs

This is the base project for testing all the MediatR features, no validation for the request is implemented in this project, and only the .NET Core features are used for the DI. There is no database, all the data is mocked.

## Requests

Next are the two requests available:

* **GetTaskQuery**: Encapsulates the request to get a specific Task. The solution will always returns the same task no matter what Id is provided (Check the [TaskRepository](./TaskManager/TaskManager.Repository/TaskRepository.cs)). 

```csharp
public class GetTaskQuery : IRequest<Result<TaskEntity>>
{
    public int TaskId { get; set; }
}
```
<p align="center">
    <a href="./TaskManager/TaskManager.Domain/Operations/GetTaskQuery/GetTaskQuery.cs"><i>GetTaskQuery.cs</i></a>
</p>

* **CreateTaskCommand**: Provides a Task object to be saved in the system. As there is no database, it won't be stored anywhere and a OK will always be returned.

```csharp
public class CreateTaskCommand : IRequest<Result>
{
    public TaskEntity Task { get; set; }
}
```

<p align="center">
    <a href="./TaskManager/TaskManager.Domain/Operations/CreateTaskCommand/CreateTaskCommand.cs"><i>CreateTaskCommand.cs</i></a>
</p>

Please note that both requests implement the `IRequest` interface, this is provided by the MediatR. Also, note that the type attached (`Result` or `Result<TaskEntity>`) is the one that should be returned for each request. Keep that in mind in the next section.

## Request Handlers

Then we handle each request handled in the next two classes:

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

The previous classes implement the `IRequestHandler` interface, this one is provided to represent the handlers. The two types attached are the request and the result:

| Request Handler Class      | Interface that implements                           | Request Type        | Result Type          |
| -------------------------- | --------------------------------------------------- | ------------------- | -------------------- |
| `GetTaskQueryHandler`      | `IRequestHandler<CreateTaskCommand, Result>`        | `GetTaskQuery`      | `Result<TaskEntity>` |
| `CreateTaskCommandHandler` | `IRequestHandler<GetTaskQuery, Result<TaskEntity>>` | `CreateTaskCommand` | `Result`             |

Then, the `Handle` method returns the result type encapsulated in the `Task` (`System.Threading.Tasks`) to handled the request asynchronously.

# Last Note

As mentioned in the beginning, there is no validation for the incoming request, that will be included in the next project version.