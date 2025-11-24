using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CRMSolution.Application;

public static class DependencyIndection
{
    public static IServiceCollection AddAppCollection(this IServiceCollection service)
    {
        service.AddValidatorsFromAssembly(typeof(DependencyIndection).Assembly);

        service.AddScoped<ITaskService, TaskService>();

        return service;
    }
}