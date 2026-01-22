using CRMSolution.Presenters;
using FluentValidation;

namespace CRMSolution.Application;

public class UpdateTaskValidator : AbstractValidator<UpdateTasksDto>
{
    public UpdateTaskValidator()
    {
        RuleFor(x => x.Headline).NotEmpty();
        RuleFor(x => x.DeadLine).NotEmpty();
    }
}
