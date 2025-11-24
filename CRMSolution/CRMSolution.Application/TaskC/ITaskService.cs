using CRMSolution.Presenters;

namespace CRMSolution.Application;

public interface ITaskService
{
    Task<Guid> Create(CreateTaskDto request, CancellationToken cancellationToken);
}