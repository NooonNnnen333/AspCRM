using CRMSolution.Application;
using CRMSolution.Infrastructure.Postgres.Repositories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CRMSolution.Infrastructure.Postgres;

public static class DependencyIndection
{
    public static IServiceCollection AddPostgresInfrastructure(this IServiceCollection service)
    {
        service.AddScoped<ITaskRepository, TaskEfCoreRepository>();

        service.AddDbContext<TaskDbContext>();

        return service;  
    }
}