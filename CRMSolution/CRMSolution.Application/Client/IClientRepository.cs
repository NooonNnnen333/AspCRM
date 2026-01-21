using CRMSolution.Domain.Client;

namespace CRMSolution.Application;

public interface IClientRepository
{
    Task<Guid> AddAsync(Client client, CancellationToken cancellationToken);

    Task<Guid> SaveAsync(Client client, CancellationToken cancellationToken);

    Task<Guid> DeleteAsync(Guid clientId, CancellationToken cancellationToken);

    Task<Client> GetByIdAsync(Guid clientId, CancellationToken cancellationToken);
}
