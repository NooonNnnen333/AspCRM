using CRMSolution.Domain.Task;
using Microsoft.EntityFrameworkCore;

namespace CRMSolution.Infrastructure.Postgres;

public class TaskDbContext : DbContext
{
    public DbSet<TaskC> tasks { get; set; }
}