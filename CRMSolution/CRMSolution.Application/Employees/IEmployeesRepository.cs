using CRMSolution.Domain.Emploees;

namespace CRMSolution.Application;

public interface IEmployeesRepository
{
    Task<Guid> AddAsync(Employees employees, CancellationToken cancellationToken);

    Task<Guid> SaveAsync(Employees employees, CancellationToken cancellationToken);

    Task<Guid> DeleteAsync(Guid employeesId, CancellationToken cancellationToken);

    Task<Employees> GetByIdAsync(Guid employeesId, CancellationToken cancellationToken);

    Task<IReadOnlyList<Employees>> GetAllAsync(CancellationToken cancellationToken);
}
