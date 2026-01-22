using CRMSolution.Application;
using CRMSolution.Domain.CompanyesClients;
using Microsoft.EntityFrameworkCore;

namespace CRMSolution.Infrastructure.Postgres.Repositories;

public class CompanyesClientsEfCoreRepository : ICompanyesClientsRepository
{
    private readonly TaskDbContext _dbContext;

    public CompanyesClientsEfCoreRepository(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> AddAsync(CompanyesClients companyesClients, CancellationToken cancellationToken)
    {
        await _dbContext.companyesClients.AddAsync(companyesClients, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return companyesClients.CompanyesClientsId;
    }

    public async Task<Guid> SaveAsync(CompanyesClients companyesClients, CancellationToken cancellationToken)
    {
        var exists = await _dbContext.companyesClients.AnyAsync(
            x => x.CompanyesClientsId == companyesClients.CompanyesClientsId,
            cancellationToken);
        if (!exists)
        {
            throw new KeyNotFoundException($"CompanyesClients {companyesClients.CompanyesClientsId} not found");
        }

        _dbContext.companyesClients.Update(companyesClients);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return companyesClients.CompanyesClientsId;
    }

    public async Task<Guid> DeleteAsync(Guid companyesClientsId, CancellationToken cancellationToken)
    {
        var companyesClients = await _dbContext.companyesClients.FindAsync(
            new object[] { companyesClientsId }, cancellationToken);
        if (companyesClients is null)
        {
            throw new KeyNotFoundException($"CompanyesClients {companyesClientsId} not found");
        }

        _dbContext.companyesClients.Remove(companyesClients);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return companyesClientsId;
    }

    public async Task<CompanyesClients> GetByIdAsync(Guid companyesClientsId, CancellationToken cancellationToken)
    {
        var companyesClients = await _dbContext.companyesClients.AsNoTracking()
            .FirstOrDefaultAsync(x => x.CompanyesClientsId == companyesClientsId, cancellationToken);

        return companyesClients ?? throw new KeyNotFoundException($"CompanyesClients {companyesClientsId} not found");
    }

    public async Task<IReadOnlyList<CompanyesClients>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.companyesClients.AsNoTracking().ToListAsync(cancellationToken);
    }
}
