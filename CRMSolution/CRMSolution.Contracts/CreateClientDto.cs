namespace CRMSolution.Presenters;

public record CreateClientDto(
    Guid ClientId,
    string Name,
    string Famaly,
    string? Otchestvo,
    string Mail,
    string NumberOfPhone,
    int NumbersOfReqest,
    string Passport);

public record UpdateClientDto(
    string Name,
    string Famaly,
    string? Otchestvo,
    string Mail,
    string NumberOfPhone,
    int NumbersOfReqest,
    string Passport);
