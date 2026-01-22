using CRMSolution.Application;
using CRMSolution.Domain.Client;
using Microsoft.EntityFrameworkCore;

namespace CRMSolution.Infrastructure.Postgres.Repositories;

public class ClientEfCoreRepository : IClientRepository
{
    private readonly TaskDbContext _dbContext;

    public ClientEfCoreRepository(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> AddAsync(Client client, CancellationToken cancellationToken)
    {
        await _dbContext.clients.AddAsync(client, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return client.ClientId;
    }

    public async Task<Guid> SaveAsync(Client client, CancellationToken cancellationToken)
    {
        var exists = await _dbContext.clients.AnyAsync(x => x.ClientId == client.ClientId, cancellationToken);
        if (!exists)
        {
            throw new KeyNotFoundException($"Client {client.ClientId} not found");
        }

        _dbContext.clients.Update(client);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return client.ClientId;
    }

    public async Task<Guid> DeleteAsync(Guid clientId, CancellationToken cancellationToken)
    {
        var client = await _dbContext.clients.FindAsync(new object[] { clientId }, cancellationToken);
        if (client is null)
        {
            throw new KeyNotFoundException($"Client {clientId} not found");
        }

        _dbContext.clients.Remove(client);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return clientId;
    }

    public async Task<Client> GetByIdAsync(Guid clientId, CancellationToken cancellationToken)
    {
        var client = await _dbContext.clients.AsNoTracking()
            .FirstOrDefaultAsync(x => x.ClientId == clientId, cancellationToken);

        return client ?? throw new KeyNotFoundException($"Client {clientId} not found");
    }

    public async Task<IReadOnlyList<Client>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.clients.AsNoTracking().ToListAsync(cancellationToken);
    }
}
