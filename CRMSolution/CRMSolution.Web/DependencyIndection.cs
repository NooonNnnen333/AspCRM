using CRMSolution.Application;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CRMSolution.Web;

public static class DependencyIndection
{
    public static IServiceCollection AddProgramDependencies(this IServiceCollection service)
    {
        service.AddWebDependencies();

        service.AddAppCollection();

        return service;
    }

    private static IServiceCollection AddWebDependencies(this IServiceCollection service)
    {
        service.AddControllers();
        service.AddOpenApi();

        return service;
    }
}