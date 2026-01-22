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
        await _dbContext.tasks.AddAsync(task, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return task.TaskId;
    }

    public async Task<Guid> SaveAsync(TaskC task, CancellationToken cancellationToken)
    {
        var exists = await _dbContext.tasks.AnyAsync(x => x.TaskId == task.TaskId, cancellationToken);
        if (!exists)
        {
            throw new KeyNotFoundException($"Task {task.TaskId} not found");
        }

        _dbContext.tasks.Update(task);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return task.TaskId;
    }

    public async Task<Guid> DeleteAsync(Guid tasksId, CancellationToken cancellationToken)
    {
        var task = await _dbContext.tasks.FindAsync(new object[] { tasksId }, cancellationToken);
        if (task is null)
        {
            throw new KeyNotFoundException($"Task {tasksId} not found");
        }

        _dbContext.tasks.Remove(task);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return tasksId;
    }

    public async Task<TaskC> GetByIdAsync(Guid tasksId, CancellationToken cancellationToken)
    {
        var task = await _dbContext.tasks.AsNoTracking()
            .FirstOrDefaultAsync(x => x.TaskId == tasksId, cancellationToken);

        return task ?? throw new KeyNotFoundException($"Task {tasksId} not found");
    }

    public async Task<IReadOnlyList<TaskC>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.tasks.AsNoTracking().ToListAsync(cancellationToken);
    }
}
