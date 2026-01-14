using FluentValidation.Results;
using Shared;

namespace CRMSolution.Application.Extension;

public static class ValidationExtension
{
    public static Error[] ToErrors(this ValidationResult failures)
        => failures.Errors.Select(e => Error.Validation(e.ErrorCode, e.ErrorMessage, e.PropertyName)).ToArray();
}