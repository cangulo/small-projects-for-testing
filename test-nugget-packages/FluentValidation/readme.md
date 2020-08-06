
<!-- 1-validation-controller-attribute 
2-validation-controller-injected  
3-validation-domain-injected      
4-validation-domain-decorator     
5-validation-domain-and-controller -->

# 3-validation-domain-injected      

* Result are encapsulated using the Result type provided by FluentResult nugget package

* Validators based on FluentValidations
  * [CreateTaskCommandValidator](./3-validation-domain-injected/TaskManager/TaskManager.Domain/Operations/CreateTaskCommand/CreateTaskCommandValidator.cs)
  * [GetTaskQueryValidator](./3-validation-domain-injected/TaskManager/TaskManager.Domain/Operations/GetTaskQuery/GetTaskQueryValidator.cs)

* Those are injected in the next request handler:
  * [CreateTaskCommandHandler](3-validation-domain-injected/TaskManager/TaskManager.Domain/Operations/CreateTaskCommand/CreateTaskCommandHandler.cs)
  * [GetTaskQueryHandler](3-validation-domain-injected/TaskManager/TaskManager.Domain/Operations/GetTaskQuery/GetTaskQueryHandler.cs)

# 4-validation-domain-decorator     

* The previous validators are now injected in the next generic decorator which is the enter point for the validations for all request
  * [RequestValidatorDecorator](4-validation-domain-decorator/TaskManager/TaskManager.Domain/Operations/RequestValidatorDecorator.cs)