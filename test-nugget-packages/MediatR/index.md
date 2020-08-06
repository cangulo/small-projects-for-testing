
| Folder                           | Main Subject                      | Link Folder                                               |
| -------------------------------- | --------------------------------- | --------------------------------------------------------- |
| 1-basic-cqrs                     | Basic CQRS project using IMediatR | [Project](./1-basic-cqrs/TaskManager/TaskManager)         |
| 2-cqrs-validation-in-handler     | B2                                | [Project](./2-cqrs-validation-in-handler/TaskManager)     |
| 3-cqrs-validation-in-decorator   | B3                                | [Project](./3-cqrs-validation-in-decorator/TaskManager)   |
| 4-cqrs-mapping-dto-in-controller | B3                                | [Project](./4-cqrs-mapping-dto-in-controller/TaskManager) |
| 5-cqrs-mapping-in-decorator      | B3                                | [Project](./5-cqrs-mapping-in-decorator/TaskManager)      |


# 1-basic-cqrs

Use directly the [MediatR](https://github.com/jbogard/MediatR) nugget package to implement the CQRS. Only one query and one command are coded. This will be the basic project to be improved in the next folders. It is important to remark I use the [FluentResult](https://github.com/altmann/FluentResults) nugget package to encapsulate results.

I created only the next requests and handlers:

* [GetTaskQuery](./1-basic-cqrs/TaskManager/TaskManager.Domain/Operations/GetTaskQuery/GetTaskQuery.cs)
  * Handler: [GetTaskQueryHandler](./1-basic-cqrs/TaskManager/TaskManager.Domain/Operations/GetTaskQuery/GetTaskQueryHandler.cs)
* [CreateTaskCommand](./1-basic-cqrs/TaskManager/TaskManager.Domain/Operations/CreateTaskCommand/CreateTaskCommand.cs)
  * Handler: [CreateTaskCommandHandler](./1-basic-cqrs/TaskManager/TaskManager.Domain/Operations/CreateTaskCommand/CreateTaskCommandHandler.cs)

# 2-cqrs-validation-in-handler     

Added validations for the queries and commands. 

* Created the next interface for the validations:
<!-- START CODE --Interface IValidator --PATH ./2-cqrs-validation-in-handler/TaskManager/TaskManager.Domain/Operations/IValidator.cs -->
```csharp
public interface IValidator<in TRequest>
{
    Result Validate(TRequest request);
}
```
<!-- END CODE -->

<p align="center">
    <a href="2-cqrs-validation-in-handler/TaskManager/TaskManager.Domain/Operations/IValidator.cs"><i>IValidator</i></a>
</p>

Please note the `Result` type returned is from the FluentResult package.

* Created the next validators
    * [GetTaskQueryValidator](./2-cqrs-validation-in-handler/TaskManager/TaskManager.Domain/Operations/GetTaskQuery/GetTaskQueryValidator.cs)
    * [CreateTaskCommandValidator](./2-cqrs-validation-in-handler/TaskManager/TaskManager.Domain/Operations/CreateTaskCommand/CreateTaskCommandValidator.cs)

* The validators are injected using .NET Core default DI. Check the [DomainModule](./2-cqrs-validation-in-handler/TaskManager/TaskManager.Domain/DomainModule.cs) file for

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

# 4-cqrs-mapping-dto-in-controller 

# 5-cqrs-mapping-in-decorator      