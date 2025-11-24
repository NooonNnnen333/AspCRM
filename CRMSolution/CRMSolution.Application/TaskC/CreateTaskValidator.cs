using CRMSolution.Presenters;
using FluentValidation;

namespace CRMSolution.Application;

public class CreateTaskValidator : AbstractValidator<CreateTaskDto>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Headline).NotEmpty();
        RuleFor(x => x.Headline).NotEmpty();
    }
}