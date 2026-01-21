using CRMSolution.Domain.CompanyesClients;

namespace CRMSolution.Application;

public interface ICompanyesClientsRepository
{
    Task<Guid> AddAsync(CompanyesClients companyesClients, CancellationToken cancellationToken);

    Task<Guid> SaveAsync(CompanyesClients companyesClients, CancellationToken cancellationToken);

    Task<Guid> DeleteAsync(Guid companyesClientsId, CancellationToken cancellationToken);

    Task<CompanyesClients> GetByIdAsync(Guid companyesClientsId, CancellationToken cancellationToken);
}
