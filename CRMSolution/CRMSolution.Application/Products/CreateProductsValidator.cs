using CRMSolution.Presenters;
using FluentValidation;

namespace CRMSolution.Application;

public class CreateProductsValidator : AbstractValidator<CreateProductsDto>
{
    public CreateProductsValidator()
    {
        RuleFor(x => x.NameOfProduct).NotEmpty();
    }
}
