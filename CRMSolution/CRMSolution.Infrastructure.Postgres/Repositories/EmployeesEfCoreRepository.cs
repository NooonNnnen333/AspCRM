using CRMSolution.Application;
using CRMSolution.Domain.Emploees;
using Microsoft.EntityFrameworkCore;

namespace CRMSolution.Infrastructure.Postgres.Repositories;

public class EmployeesEfCoreRepository : IEmployeesRepository
{
    private readonly TaskDbContext _dbContext;

    public EmployeesEfCoreRepository(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> AddAsync(Employees employees, CancellationToken cancellationToken)
    {
        await _dbContext.employees.AddAsync(employees, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return employees.EmployeesId;
    }

    public async Task<Guid> SaveAsync(Employees employees, CancellationToken cancellationToken)
    {
        var exists = await _dbContext.employees.AnyAsync(x => x.EmployeesId == employees.EmployeesId, cancellationToken);
        if (!exists)
        {
            throw new KeyNotFoundException($"Employees {employees.EmployeesId} not found");
        }

        _dbContext.employees.Update(employees);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return employees.EmployeesId;
    }

    public async Task<Guid> DeleteAsync(Guid employeesId, CancellationToken cancellationToken)
    {
        var employees = await _dbContext.employees.FindAsync(new object[] { employeesId }, cancellationToken);
        if (employees is null)
        {
            throw new KeyNotFoundException($"Employees {employeesId} not found");
        }

        _dbContext.employees.Remove(employees);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return employeesId;
    }

    public async Task<Employees> GetByIdAsync(Guid employeesId, CancellationToken cancellationToken)
    {
        var employees = await _dbContext.employees.AsNoTracking()
            .FirstOrDefaultAsync(x => x.EmployeesId == employeesId, cancellationToken);

        return employees ?? throw new KeyNotFoundException($"Employees {employeesId} not found");
    }

    public async Task<IReadOnlyList<Employees>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.employees.AsNoTracking().ToListAsync(cancellationToken);
    }
}
