using CRMSolution.Application;
using CRMSolution.Web;
using CRMSolution.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddProgramDependencies(builder.Configuration);

var app = builder.Build();

app.UseExceptionMidleware(); // исползование метода с обработкой исключений

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "CRMSolutions"));
}

app.MapControllers();



app.Run();