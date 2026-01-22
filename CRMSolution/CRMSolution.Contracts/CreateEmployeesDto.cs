using CRMSolution.Domain.Emploees;

namespace CRMSolution.Presenters;

public record CreateEmployeesDto(
    Guid EmployeesId,
    string Name,
    string Famaly,
    string? Otchestvo,
    string Mail,
    Role Role,
    string NumberOfPhone);

public record UpdateEmployeesDto(
    string Name,
    string Famaly,
    string? Otchestvo,
    string Mail,
    Role Role,
    string NumberOfPhone);
