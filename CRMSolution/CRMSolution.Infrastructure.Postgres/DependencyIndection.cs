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

        service.AddDbContext<TaskDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("Database");
            options.UseNpgsql(connectionString);
        });


        service.AddScoped<ITaskRepository, TaskEfCoreRepository>();
        service.AddScoped<IClientRepository, ClientEfCoreRepository>();
        service.AddScoped<IEmployeesRepository, EmployeesEfCoreRepository>();
        service.AddScoped<ICompanyesClientsRepository, CompanyesClientsEfCoreRepository>();
        service.AddScoped<IProductsRepository, ProductsEfCoreRepository>();

        return service;
    }
}
