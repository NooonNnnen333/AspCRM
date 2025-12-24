namespace CRMSolution.Application.Exceptions;

public class TaskNotFound : NotFoundException
{
    public TaskNotFound(Guid id)
        : base("Task", id)
    {
    }
    
}