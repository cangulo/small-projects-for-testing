# 2-cqrs-validation-in-handler     

In this project simple validations for the queries and commands has been added. Next are the steps done:

1. Define a contract for all the validators in the next interface:

<!-- START CODE --Interface IValidator --PATH ./2-cqrs-validation-in-handler/TaskManager/TaskManager.Domain/Operations/IValidator.cs -->
```csharp
public interface IValidator<in TRequest>
{
    Result Validate(TRequest request);
}
```
<!-- END CODE -->

<p align="center">
    <a href="./2-cqrs-validation-in-handler/TaskManager/TaskManager.Domain/Operations/IValidator.cs"><i>IValidator.cs</i></a>
</p>

Please note all the validators will return a `Result` type, this is from the FluentResult package mentioned before.

2. Create the implementations for the request we handle: 

* GetTaskQueryValidator

```csharp
public class GetTaskQueryValidator : IValidator<GetTaskQuery>
{
    public Result Validate(GetTaskQuery request)
    {
        if (request.TaskId < 1)
            return Result.Fail("TaskId should be greater than 0");

        return Result.Ok();
    }
}
```

<p align="center">
    <a href="./2-cqrs-validation-in-handler/TaskManager/TaskManager.Domain/Operations/GetTaskQuery/GetTaskQueryValidator.cs"><i>GetTaskQueryValidator.cs</i></a>
</p>

* CreateTaskCommandValidator

```csharp
    public class CreateTaskCommandValidator : IValidator<CreateTaskCommand>
    {
        private readonly int MaxTitleLength = 200;
        public Result Validate(CreateTaskCommand request)
        {
            var errors = new List<string>();
            if (request.Task.TodoDate < DateTime.UtcNow)
                errors.Add("Todo Date should be in the future");

            if (string.IsNullOrEmpty(request.Task.Title))
                errors.Add("Title should not be empty");

            if (request.Task.Title.Length > MaxTitleLength)
                errors.Add($"Title should be shorter than {MaxTitleLength}");

            return errors.Count == 0 ? Result.Ok() : Result.Merge(errors.Select(x => Result.Fail(x)).ToArray());
        }
    }
```

<p align="center">
    <a href="./2-cqrs-validation-in-handler/TaskManager/TaskManager.Domain/Operations/CreateTaskCommand/CreateTaskCommandValidator.cs"><i>CreateTaskCommandValidator.cs</i></a>
</p>

1. Register the validators in the default .NET Core container:

```csharp
public static class DomainModule
{
    public static void AddMediatRClasses(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DomainModule));
        services.AddTransient<IValidator<CreateTaskCommand>, CreateTaskCommandValidator>();
        services.AddTransient<IValidator<GetTaskQuery>, GetTaskQueryValidator>();
    }
}
```
<p align="center">
    <a href="./2-cqrs-validation-in-handler/TaskManager/TaskManager.Domain/DomainModule.cs"><i>DomainModule.cs</i></a>
</p>


4. Inject them in the handlers:
