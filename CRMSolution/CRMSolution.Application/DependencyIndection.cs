using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CRMSolution.Application;

public static class DependencyIndection
{
    public static IServiceCollection AddAppCollection(this IServiceCollection service)
    {
        service.AddValidatorsFromAssembly(typeof(DependencyIndection).Assembly);

        service.AddScoped<ITaskService, TaskService>();
        service.AddScoped<IClientService, ClientService>();
        service.AddScoped<IEmployeesService, EmployeesService>();
        service.AddScoped<ICompanyesClientsService, CompanyesClientsService>();
        service.AddScoped<IProductsService, ProductsService>();

        return service;
    }
}
