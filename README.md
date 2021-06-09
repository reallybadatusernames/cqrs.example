# CQRS Example #

An example of how to implement CQRS in an application. This project utilizes SimpleInjector, but can be adapted to other DI providers. 

**Query Example:**
```
public class EntityById
{
    public class Query : IQuery 
    {
        public int Id { get; set; }
    }

    public class Result : IQueryResult 
    {
        public Entity Entity { get; set; }
    }
 
    public class Handler : IQueryHandler<Query, Result>
    {
        public Result Retrieve(Query query)
        {
            using (var ctx = new SqlContext()) 
            {
                return new Result 
                {
                    Entity = ctx.Entities.Where(e => e.Id == query.Id).FirstOrDefault()
                };
            }
        }
    }
}
```
**Command Example**
```
public class AddEntity
{
    public class Command: ICommand
    {
        public string Name { get; set; }
    }
	
    public class Handler : ICommandHandler<Command>
    {
        public void Execute(Command command)
        {
          using (var ctx = new SqlContext()) 
          {
            ctx.Entities.Add(new Entity { Name = command.Name});
            ctx.SaveChanges();
			    }		
       }
    }
}
```

**Command Validator**
```
public class Validator : ICommandValidator<Command>
{
    public void Validate(Command command)
    {
        if (string.IsNullOrEmpty(command.User.Name))
            fail("User Name must be specified");

        if (string.IsNullOrEmpty(command.User.LastName))
            fail("Last Name must be specified");

        if (command.User.BirthDate == DateTime.MinValue)
            fail("Please specify a valid Birth Day");

        void fail(string reason)
        {
            throw new CommandValidationException(reason);
        }
    }
}
```

**Note:**   To execute asynchronously, implement either IQueryHandlerAsync or ICommandHandlerAsync in your handler class.

