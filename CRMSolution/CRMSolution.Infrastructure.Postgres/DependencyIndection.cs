using CRMSolution.Application;
using CRMSolution.Infrastructure.Postgres.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRMSolution.Infrastructure.Postgres;

public static class DependencyIndection
{
    public static IServiceCollection AddPostgresInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddScoped<ITaskRepository, TaskEfCoreRepository>();

        service.AddDbContext<TaskDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("Database");
            options.UseNpgsql(connectionString);
        });

        return service;
    }
}
