using CRMSolution.Presenters;

namespace CRMSolution.Application;

public interface IClientService
{
    Task<Guid> Create(CreateClientDto request, CancellationToken cancellationToken);
}
