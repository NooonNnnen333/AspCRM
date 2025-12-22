using CRMSolution.Application;
using CRMSolution.Domain.Task;

namespace CRMSolution.Application;

public interface ITaskRepository
{
    Task<Guid> AddAsync(TaskC _task, CancellationToken cancellationToken);

    Task<Guid> SaveAsync(TaskC _task, CancellationToken cancellationToken);

    Task<Guid> DeleteAsync(Guid tasksId, CancellationToken cancellationToken);

    Task<TaskC> GetByIdAsync(Guid tasksId, CancellationToken cancellationToken);

}