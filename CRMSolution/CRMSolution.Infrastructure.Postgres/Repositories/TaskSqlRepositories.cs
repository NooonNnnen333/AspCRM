using CRMSolution.Application;
using CRMSolution.Domain.Task;
using Dapper;
using Microsoft.Extensions.Configuration;
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

    public async Task<Guid> SaveAsync(TaskC _task, CancellationToken cancellationToken) => throw new NotImplementedException();

    public async Task<Guid> DeleteAsync(Guid tasksId, CancellationToken cancellationToken) => throw new NotImplementedException();

    public async Task<TaskC> GetByIdAsync(Guid tasksId, CancellationToken cancellationToken) => throw new NotImplementedException();
}