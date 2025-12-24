namespace CRMSolution.Application.Exceptions;

public class TaskValidationException(string massage)
        : BadRequestException(massage);