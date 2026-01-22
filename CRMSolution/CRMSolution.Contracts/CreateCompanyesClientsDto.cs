namespace CRMSolution.Presenters;

public record CreateCompanyesClientsDto(
    Guid CompanyesClientsId,
    string Mail,
    string NumberOfPhone,
    string Inn);

public record UpdateCompanyesClientsDto(
    string Mail,
    string NumberOfPhone,
    string Inn);
