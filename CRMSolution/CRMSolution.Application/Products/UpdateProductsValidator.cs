using CRMSolution.Presenters;
using FluentValidation;

namespace CRMSolution.Application;

public class UpdateProductsValidator : AbstractValidator<UpdateProductsDto>
{
    public UpdateProductsValidator()
    {
        RuleFor(x => x.NameOfProduct).NotEmpty();
    }
}
