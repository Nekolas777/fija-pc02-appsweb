using System.Data;
using System.Net;
using si730pc2u202217485.Shared.Domain.Model.Exceptions;

namespace si730pc2u202217485.Shared.Infrastructure.Interfaces.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    
    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var code = HttpStatusCode.BadRequest;
        var result = ex.Message;

        if (ex is GenericException)
        {
            code = HttpStatusCode.BadRequest;
        }
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        var jsonResult = System.Text.Json.JsonSerializer.Serialize(new { message = result });
        await context.Response.WriteAsync(jsonResult);
    }
}