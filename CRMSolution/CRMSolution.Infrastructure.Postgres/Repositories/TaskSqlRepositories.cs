using CRMSolution.Application;
using CRMSolution.Domain.Task;
using Dapper;
using Npgsql;

namespace CRMSolution.Infrastructure.Postgres.Repositories;

public class TaskSqlRepositories : ITaskRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public TaskSqlRepositories(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Guid> AddAsync(TaskC _task, CancellationToken cancellationToken)
    {
        const string sql = """
                            INSERT INTO tasks (task_id, emloyees_id, client_id, product_id, title, date_of_created_this, date_of_plane_do, note, status)
                           VALUES (@TaskId, @EmloyeesId, @ClientId, @ProductId, @Title, @DateOfCreatedThis, @DateOfPlaneDo, @Note, @Status)
                           """;
        using var connection = _sqlConnectionFactory.Create();
        await connection.ExecuteAsync(sql, new
        {
            TaskId = _task.TaskId,
            EmloyeesId = _task.EmloyeesId.ToArray(),
            ClientId = _task.ClientId,
            ProductId = _task.ProductId,
            Title = _task.Title,
            DateOfCreatedThis = _task.DateOfCreatedThis,
            DateOfPlaneDo = _task.DateOfPlaneDo,
            Note = _task.Note,
            Status = _task.Status,
        });

        return _task.TaskId;
    }

    public async Task<Guid> SaveAsync(TaskC _task, CancellationToken cancellationToken)
    {
        const string sql = """
                           UPDATE tasks
                           SET emloyees_id = @EmloyeesId,
                               client_id = @ClientId,
                               product_id = @ProductId,
                               title = @Title,
                               date_of_plane_do = @DateOfPlaneDo,
                               note = @Note,
                               status = @Status
                           WHERE task_id = @TaskId
                           """;
        using var connection = _sqlConnectionFactory.Create();
        await connection.ExecuteAsync(sql, new
        {
            TaskId = _task.TaskId,
            EmloyeesId = _task.EmloyeesId.ToArray(),
            ClientId = _task.ClientId,
            ProductId = _task.ProductId,
            Title = _task.Title,
            DateOfPlaneDo = _task.DateOfPlaneDo,
            Note = _task.Note,
            Status = _task.Status,
        });

        return _task.TaskId;
    }

    public async Task<Guid> DeleteAsync(Guid tasksId, CancellationToken cancellationToken)
    {
        const string sql = "DELETE FROM tasks WHERE task_id = @TaskId";
        using var connection = _sqlConnectionFactory.Create();
        await connection.ExecuteAsync(sql, new { TaskId = tasksId });
        return tasksId;
    }

    public async Task<TaskC?> GetByIdAsync(Guid tasksId, CancellationToken cancellationToken)
    {
        const string sql = """
                           SELECT task_id AS TaskId,
                                  emloyees_id AS EmloyeesId,
                                  client_id AS ClientId,
                                  product_id AS ProductId,
                                  title,
                                  date_of_created_this AS DateOfCreatedThis,
                                  date_of_plane_do AS DateOfPlaneDo,
                                  note,
                                  status
                           FROM tasks
                           WHERE task_id = @TaskId
                           """;
        using var connection = _sqlConnectionFactory.Create();
        return await connection.QueryFirstOrDefaultAsync<TaskC>(sql, new { TaskId = tasksId });
    }

    public async Task<List<TaskC>> GetAllAsync(CancellationToken cancellationToken)
    {
        const string sql = """
                           SELECT task_id AS TaskId,
                                  emloyees_id AS EmloyeesId,
                                  client_id AS ClientId,
                                  product_id AS ProductId,
                                  title,
                                  date_of_created_this AS DateOfCreatedThis,
                                  date_of_plane_do AS DateOfPlaneDo,
                                  note,
                                  status
                           FROM tasks
                           """;
        using var connection = _sqlConnectionFactory.Create();
        var result = await connection.QueryAsync<TaskC>(sql);
        return result.ToList();
    }
}
