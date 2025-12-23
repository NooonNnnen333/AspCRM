using CRMSolution.Domain.Task;
using CRMSolution.Presenters;

namespace CRMSolution.Application;

public interface ITaskService
{
    Task<Guid> Create(CreateTaskDto request, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<TaskC>> Get(GetTasksDto filter, CancellationToken cancellationToken);

    Task<TaskC?> GetById(Guid taskId, CancellationToken cancellationToken);

    Task Update(Guid taskId, UpdateTasksDto request, CancellationToken cancellationToken);

    Task Delete(Guid taskId, CancellationToken cancellationToken);
}
