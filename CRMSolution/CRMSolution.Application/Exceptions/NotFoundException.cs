using System.Text.Json;
using Shared;

namespace CRMSolution.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(Shared.Error[] errors)
    : base(JsonSerializer.Serialize(errors))
    {
    }
}