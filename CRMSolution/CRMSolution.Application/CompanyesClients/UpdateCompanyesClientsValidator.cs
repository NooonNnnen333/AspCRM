using CRMSolution.Presenters;
using FluentValidation;

namespace CRMSolution.Application;

public class UpdateCompanyesClientsValidator : AbstractValidator<UpdateCompanyesClientsDto>
{
    public UpdateCompanyesClientsValidator()
    {
        RuleFor(x => x.Mail).NotEmpty();
        RuleFor(x => x.NumberOfPhone).NotEmpty();
        RuleFor(x => x.Inn).NotEmpty();
    }
}
