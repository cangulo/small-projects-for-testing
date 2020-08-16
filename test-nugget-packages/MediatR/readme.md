# Nugget packaged tested:

[MediatR](https://github.com/jbogard/MediatR/wiki)

# What it is used for?

Library to make it easy to develop a solution following the CQRS (Command Query Responsibility Segregation)

# Basic Projects

The basic project is a Task Manager API with the next two features:

| Feature          | Endpoint        | HTTP Method | Query / Command class                                                                                                | Handler                                                                                                                            |
| ---------------- | --------------- | ----------- | -------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| Get a Task by Id | `api/task/{id}` | GET         | [GetTaskQuery](./1-basic-cqrs/TaskManager/TaskManager.Domain/Operations/GetTaskQuery/GetTaskQuery.cs)                | [GetTaskQueryHandler](./1-basic-cqrs/TaskManager/TaskManager.Domain/Operations/GetTaskQuery/GetTaskQueryHandler.cs)                |
| Create a Task    | `api/task`      | POST        | [CreateTaskCommand](./1-basic-cqrs/TaskManager/TaskManager.Domain/Operations/CreateTaskCommand/CreateTaskCommand.cs) | [CreateTaskCommandHandler](./1-basic-cqrs/TaskManager/TaskManager.Domain/Operations/CreateTaskCommand/CreateTaskCommandHandler.cs) |

Please note all the data is mocked. There is no database linked, check the [TaskRepository.cs](./1-basic-cqrs/TaskManager/TaskManager.Repository/TaskRepository.cs). It is also important to mention that the [FluentResult](https://github.com/altmann/FluentResults) nugget package is used to encapsulate the results.

# Versions

All the next projects are developed incrementally, each one introduce an improvement to the previous one. 

| Folder                         | Main Subject                               | Link Folder                                             |
| ------------------------------ | ------------------------------------------ | ------------------------------------------------------- |
| 1-basic-cqrs                   | Basic CQRS project using MediatR           | [Project](./1-basic-cqrs/TaskManager/TaskManager)       |
| 2-cqrs-validation-in-handler   | Include a validation layer in the handlers | [Project](./2-cqrs-validation-in-handler/TaskManager)   |
| 3-cqrs-validation-in-decorator | Improve the validation layer               | [Project](./3-cqrs-validation-in-decorator/TaskManager) |

# 1-basic-cqrs

This is the basic project, no validation for the request is implemented and all the dependency injection used the default .NET Core features.

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


# 3-cqrs-validation-in-decorator   

Next are the decorators I have:
* [CreateTaskCommandHandler.cs](./3-cqrs-validation-in-decorator/TaskManager/TaskManager.Domain/Operations/CreateTaskCommand/CreateTaskCommandHandler.cs)
* [GetTaskQueryHandler.cs](./3-cqrs-validation-in-decorator/TaskManager/TaskManager.Domain/Operations/GetTaskQuery/GetTaskQueryHandler.cs)

As all the RequestHandlers implement the generic interface `IRequestHandler<,>` and have the next validation code in common:

<!-- START CODE --Line 21 --PATH ./2-cqrs-validation-in-handler/TaskManager/TaskManager.Domain/Operations/CreateTaskCommand/CreateTaskCommandHandler.cs -->
```csharp
public async Task<Result> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
{
    // start common code for validation

    var validationResult = _validator.Validate(request);
    if (validationResult.IsFailed)
        return validationResult;
    
    // end common code for validation

    return await _taskRepository.CreateTask(request.Task);
}
```
<!-- END CODE -->
  
We can work on create a Decorator than encapsulate the validation Layer. This one should be executed before the real handler, in order to do that our DI framework should be able to inject the decorator as the first implementation when a `IRequestHandler<,>` , then, when the next implementation is requested, the real decorator will be provided. Keeping that in mind we know our decorator should accomplish the next points:

1. Implement the generic `IRequestHandler<,>` interface
2. Request for the next `IRequestHandler<,>` implementation. In the `Handle` method the validation will be performed first, if the result is OK, we proceed to provide the request to the real Handler, this is done by calling the method of the real implementation and passing the request as parameter. If the validation results KO, we return the error. 

However, as the `Handler` method we should implement in the decorator returns a different result type for each implementation RequestHandler implementation (remember the interface is `IRequestHandler<TRequest,TResponse>`). We can not return directly the validation result of our `IValidator` because this is always of type `Result`. Let's see this in action:

<!-- TODO: Post the code of GetTaskQuery -->
<!-- TODO: Post the code of CreateTaskCommand -->

<!-- Then explains the result types, explain the only problems is for the queries which return a type value attached to the Result. Then explain how you can solve that with reflection  -->

In order to solve that I create an extension method to cast 

* The decorator is injected using [Autofac](https://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html)
* As the CQRS is used, if we want to have a decorator we should be careful about what we return.

<!-- # 4-cqrs-mapping-dto-in-controller  -->

<!-- # 5-cqrs-mapping-in-decorator       -->