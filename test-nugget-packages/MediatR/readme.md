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

<!-- # 4-cqrs-mapping-dto-in-controller  -->

<!-- # 5-cqrs-mapping-in-decorator       -->