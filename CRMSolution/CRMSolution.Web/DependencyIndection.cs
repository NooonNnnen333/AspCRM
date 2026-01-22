using CRMSolution.Application;
using CRMSolution.Infrastructure.Postgres;
using CRMSolution.Web.Security;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CRMSolution.Web;

public static class DependencyIndection
{
    public static IServiceCollection AddProgramDependencies(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddWebDependencies();

        service.AddAppCollection();

        service.AddPostgresInfrastructure(configuration);
        service.AddScoped<ITokenService, JwtTokenService>();

        return service;
    }

    private static IServiceCollection AddWebDependencies(this IServiceCollection service)
    {
        service.AddControllers();
        service.AddOpenApi();

        return service;
    }
}
