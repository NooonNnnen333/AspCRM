using CRMSolution.Presenters;
using FluentValidation;

namespace CRMSolution.Application;

public class CreateTaskValidator : AbstractValidator<CreateTaskDto>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Как продуктк существовать без id?");
        RuleFor(x => x.Headline)
            .NotEmpty().WithMessage("Такое Аллах не прощает");
        RuleFor(x => x.ClientId)
            .NotEmpty().WithMessage("Кто делать будет эту задачу");
    }
}