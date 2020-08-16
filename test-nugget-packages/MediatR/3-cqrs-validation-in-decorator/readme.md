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