using CRMSolution.Domain.CompanyesClients;
using CRMSolution.Presenters;

namespace CRMSolution.Application;

public interface ICompanyesClientsService
{
    Task<Guid> Create(CreateCompanyesClientsDto request, CancellationToken cancellationToken);

    Task<IReadOnlyList<CompanyesClients>> GetAll(CancellationToken cancellationToken);

    Task<CompanyesClients> GetById(Guid companyesClientsId, CancellationToken cancellationToken);

    Task<Guid> Update(Guid companyesClientsId, UpdateCompanyesClientsDto request, CancellationToken cancellationToken);

    Task<Guid> Delete(Guid companyesClientsId, CancellationToken cancellationToken);
}
