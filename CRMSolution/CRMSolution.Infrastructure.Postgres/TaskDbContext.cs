using System.Text.Json;
using CRMSolution.Domain.Client;
using CRMSolution.Domain.CompanyesClients;
using CRMSolution.Domain.Emploees;
using CRMSolution.Domain.Products;
using CRMSolution.Domain.Task;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRMSolution.Infrastructure.Postgres;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
    {
    }

    public DbSet<TaskC> Tasks { get; set; } = null!;
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Employees> Employees { get; set; } = null!;
    public DbSet<Products> Products { get; set; } = null!;
    public DbSet<CompanyesClients> CompanyClients { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskC>(builder =>
        {
            builder.ToTable("tasks");
            builder.HasKey(t => t.TaskId);
            var converter = new ValueConverter<List<Guid>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<Guid>>(v, (JsonSerializerOptions?)null) ?? new List<Guid>());

            builder.Property(t => t.EmloyeesId)
                .HasConversion(converter);
            builder.Property(t => t.DateOfCreatedThis)
                .HasConversion(
                    dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
                    dateTime => DateOnly.FromDateTime(dateTime));
            builder.Property(t => t.Status)
                .HasConversion<string>();
        });

        modelBuilder.Entity<Client>(builder =>
        {
            builder.ToTable("clients");
            builder.HasKey(x => x.ClientId);
        });

        modelBuilder.Entity<Employees>(builder =>
        {
            builder.ToTable("employees");
            builder.HasKey(x => x.EmployeesId);
        });

        modelBuilder.Entity<Products>(builder =>
        {
            builder.ToTable("products");
            builder.HasKey(x => x.ProductId);
        });

        modelBuilder.Entity<CompanyesClients>(builder =>
        {
            builder.ToTable("company_clients");
            builder.HasKey(x => x.CompanyesClientsId);
        });
    }
}
