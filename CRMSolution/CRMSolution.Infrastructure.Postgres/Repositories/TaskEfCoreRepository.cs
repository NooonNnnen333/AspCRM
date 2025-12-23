using CRMSolution.Application;
using CRMSolution.Domain.Task;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Guid> SaveAsync(TaskC task, CancellationToken cancellationToken)
    {
        _dbContext.Tasks.Update(task);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return task.TaskId;
    }

    public async Task<Guid> DeleteAsync(Guid tasksId, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.TaskId == tasksId, cancellationToken);
        if (entity is null)
        {
            return Guid.Empty;
        }

        _dbContext.Tasks.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return tasksId;
    }

    public Task<TaskC?> GetByIdAsync(Guid tasksId, CancellationToken cancellationToken)
    {
        return _dbContext.Tasks.AsNoTracking().FirstOrDefaultAsync(t => t.TaskId == tasksId, cancellationToken);
    }

    public async Task<List<TaskC>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Tasks.AsNoTracking().ToListAsync(cancellationToken);
    }
}
