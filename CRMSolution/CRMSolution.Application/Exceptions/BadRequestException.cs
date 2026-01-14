using System.Text.Json;
using Shared;

namespace CRMSolution.Application.Exceptions;

/// <summary>
/// Некорректный запрос
/// </summary>
public class BadRequestException : Exception
{
    protected BadRequestException(Shared.Error[] errors)
        : base(JsonSerializer.Serialize(errors))
    {
    }
}