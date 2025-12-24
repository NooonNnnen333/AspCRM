namespace CRMSolution.Application.Exceptions;

public class BadRequestException : Exception
{
    protected BadRequestException(string message)
        : base(message)
    {
    }

    protected BadRequestException(List<string> errors)
        : base(string.Join(", ", errors))
    {
    }
}