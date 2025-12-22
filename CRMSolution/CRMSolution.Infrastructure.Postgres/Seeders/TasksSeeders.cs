using CRMSolution.Application;

namespace CRMSolution.Infrastructure.Postgres.Seeders;

public class TasksSeeders : ITasksSeeders
{
    private readonly TaskDbContext _dbContext;

    public TasksSeeders(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Task SeedAsync()
    {
        throw new NotImplementedException();
    }
}