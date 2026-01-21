using CRMSolution.Presenters;

namespace CRMSolution.Application;

public interface IEmployeesService
{
    Task<Guid> Create(CreateEmployeesDto request, CancellationToken cancellationToken);
}
