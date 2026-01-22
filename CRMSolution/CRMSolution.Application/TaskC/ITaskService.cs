using CRMSolution.Domain.Task;
using CRMSolution.Presenters;

namespace CRMSolution.Application;

public interface ITaskService
{
    Task<Guid> Create(CreateTaskDto request, CancellationToken cancellationToken);

    Task<IReadOnlyList<TaskC>> GetAll(CancellationToken cancellationToken);

    Task<TaskC> GetById(Guid taskId, CancellationToken cancellationToken);

    Task<Guid> Update(Guid taskId, UpdateTasksDto request, CancellationToken cancellationToken);

    Task<Guid> Delete(Guid taskId, CancellationToken cancellationToken);
}
