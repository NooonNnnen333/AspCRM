using CRMSolution.Presenters;

namespace CRMSolution.Application;

public interface ICompanyesClientsService
{
    Task<Guid> Create(CreateCompanyesClientsDto request, CancellationToken cancellationToken);
}
