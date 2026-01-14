namespace CRMSolution.Application.Exceptions;

public class TaskNotFound : NotFoundException
{
    public TaskNotFound(Shared.Error[] errors)
        : base(errors)
    {
    }
}