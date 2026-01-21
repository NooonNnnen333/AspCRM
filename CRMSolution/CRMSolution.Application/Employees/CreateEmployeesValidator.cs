using CRMSolution.Presenters;
using FluentValidation;

namespace CRMSolution.Application;

public class CreateEmployeesValidator : AbstractValidator<CreateEmployeesDto>
{
    public CreateEmployeesValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Famaly).NotEmpty();
        RuleFor(x => x.Mail).NotEmpty();
        RuleFor(x => x.NumberOfPhone).NotEmpty();
        RuleFor(x => x.Role).IsInEnum();
    }
}
