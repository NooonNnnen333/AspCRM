using CRMSolution.Application;
using CRMSolution.Domain.Task;

namespace CRMSolution.Infrastructure.Postgres.Repositories;

public class TaskEfCoreRepository : ITaskRepository
{
    private readonly TaskDbContext _dbContext;

    public TaskEfCoreRepository(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> AddAsync(TaskC task, CancellationToken cancellationToken)
    {
        await _dbContext.Tasks.AddAsync(task, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return task.TaskId;
    }

    public async Task<Guid> SaveAsync(TaskC _task, CancellationToken cancellationToken) => throw new NotImplementedException();

    public async Task<Guid> DeleteAsync(Guid tasksId, CancellationToken cancellationToken) => throw new NotImplementedException();

    public async Task<TaskC> GetByIdAsync(Guid tasksId, CancellationToken cancellationToken) => throw new NotImplementedException();
}