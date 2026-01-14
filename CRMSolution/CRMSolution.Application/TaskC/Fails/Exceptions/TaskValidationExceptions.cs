using Shared;

namespace CRMSolution.Application.Exceptions;

public class TaskValidationException
    : BadRequestException
{
    public TaskValidationException(Shared.Error[] errors)
        : base(errors)
    {
    }
}