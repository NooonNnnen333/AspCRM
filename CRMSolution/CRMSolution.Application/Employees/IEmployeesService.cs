using CRMSolution.Domain.Emploees;
using CRMSolution.Presenters;

namespace CRMSolution.Application;

public interface IEmployeesService
{
    Task<Guid> Create(CreateEmployeesDto request, CancellationToken cancellationToken);

    Task<IReadOnlyList<Employees>> GetAll(CancellationToken cancellationToken);

    Task<Employees> GetById(Guid employeesId, CancellationToken cancellationToken);

    Task<Guid> Update(Guid employeesId, UpdateEmployeesDto request, CancellationToken cancellationToken);

    Task<Guid> Delete(Guid employeesId, CancellationToken cancellationToken);
}
