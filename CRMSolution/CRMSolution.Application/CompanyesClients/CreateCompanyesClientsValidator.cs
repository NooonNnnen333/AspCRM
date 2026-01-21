using CRMSolution.Presenters;
using FluentValidation;

namespace CRMSolution.Application;

public class CreateCompanyesClientsValidator : AbstractValidator<CreateCompanyesClientsDto>
{
    public CreateCompanyesClientsValidator()
    {
        RuleFor(x => x.Mail).NotEmpty();
        RuleFor(x => x.NumberOfPhone).NotEmpty();
        RuleFor(x => x.Inn).NotEmpty();
    }
}
