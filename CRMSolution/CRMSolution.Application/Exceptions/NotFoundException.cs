namespace CRMSolution.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string record, Guid id)
    : base($"{record} with id {id} not found")
    {
    }
}