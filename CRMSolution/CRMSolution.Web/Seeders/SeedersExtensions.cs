using CRMSolution.Application;
using CRMSolution.Infrastructure.Postgres.Seeders;

namespace CRMSolution.Web.Seeders;

public static class SeedersExtensions
{

    public static async Task<WebApplication> UseSeeders(this WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();

        var seeders = scope.ServiceProvider.GetService<IEnumerable<ITasksSeeders>>();

        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync();
        } 

        return app;
    }
}