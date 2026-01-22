using CRMSolution.Domain.Client;
using CRMSolution.Presenters;

namespace CRMSolution.Application;

public interface IClientService
{
    Task<Guid> Create(CreateClientDto request, CancellationToken cancellationToken);

    Task<IReadOnlyList<Client>> GetAll(CancellationToken cancellationToken);

    Task<Client> GetById(Guid clientId, CancellationToken cancellationToken);

    Task<Guid> Update(Guid clientId, UpdateClientDto request, CancellationToken cancellationToken);

    Task<Guid> Delete(Guid clientId, CancellationToken cancellationToken);
}
