using CRMSolution.Domain.Client;
using CRMSolution.Domain.CompanyesClients;
using CRMSolution.Domain.Emploees;
using CRMSolution.Domain.Products;
using CRMSolution.Domain.Task;
using Microsoft.EntityFrameworkCore;

namespace CRMSolution.Infrastructure.Postgres;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<TaskC> tasks { get; set; }
    public DbSet<Client> clients { get; set; }
    public DbSet<Employees> employees { get; set; }
    public DbSet<CompanyesClients> companyesClients { get; set; }
    public DbSet<Products> products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskDbContext).Assembly);
    }
}
