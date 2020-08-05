
| Folder                           | Main Subject | Link Folder |
| -------------------------------- | ------------ | ----------- |
| 1-basic-cqrs                     | B1           | C1          |
| 2-cqrs-validation-in-handler     | B2           | C2          |
| 3-cqrs-validation-in-decorator   | B3           | C3          |
| 4-cqrs-mapping-dto-in-controller | B3           | C3          |
| 5-cqrs-mapping-in-decorator      | B3           | C3          |


# 1-basic-cqrs

Use directly IMediatR, one query and one command. There are no validations

# 2-cqrs-validation-in-handler     

Added validations with a 

* IValidator
* Injected using .NET Core default DI 

# 3-cqrs-validation-in-decorator   

* A Decorator to encapsulate all the validations
* The decorator is injected using [Autofac](https://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html)
* As the CQRS is used, if we want to have a decorator we should be careful about what we return.

# 4-cqrs-mapping-dto-in-controller 
# 5-cqrs-mapping-in-decorator      