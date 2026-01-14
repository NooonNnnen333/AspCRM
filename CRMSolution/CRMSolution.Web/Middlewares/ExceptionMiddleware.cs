using System.Net;
using CRMSolution.Application.Exceptions;
using FluentValidation;
using Newtonsoft.Json;
using Shared;

namespace CRMSolution.Web.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exeption)
        {
            await HandlExceptionAsync(context, exeption);
        }
    }

    private async Task HandlExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, exception.Message);

        var (code, errors) = exception switch
        {
            BadRequestException => (
                StatusCodes.Status500InternalServerError,
                System.Text.Json.JsonSerializer.Deserialize<Error[]>(exception.Message)),

            NotFoundException => (
                StatusCodes.Status404NotFound, System.Text.Json.JsonSerializer.Deserialize<Error[]>(exception.Message)),

            _ => (StatusCodes.Status500InternalServerError, [Error.Failure(null, "n")])
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;

        await context.Response.WriteAsJsonAsync(errors);
    }
}

public static class ExceptionMiddleWareExtension
{
    public static IApplicationBuilder UseExceptionMidleware(this WebApplication app)
        => app.UseMiddleware<ExceptionMiddleware>();
}