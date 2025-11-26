using System.Net;
using System.Text.Json;

namespace LoanApi.Api.Middleware;

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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var statusCode = (int)HttpStatusCode.InternalServerError;
        var message = "Internal Server Error";

        switch (exception)
        {
            case LoanApi.Application.Exceptions.ConflictException:
                statusCode = (int)HttpStatusCode.Conflict;
                message = exception.Message;
                break;
            case FluentValidation.ValidationException validationEx:
                statusCode = (int)HttpStatusCode.BadRequest;
                message = JsonSerializer.Serialize(validationEx.Errors);
                break;
            case Microsoft.EntityFrameworkCore.DbUpdateException dbEx:
                if (dbEx.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
                {
                    statusCode = (int)HttpStatusCode.Conflict;
                    message = "A record with the same key already exists.";
                }
                break;
            case Exception ex when ex.Message == "Invalid username or password.":
                statusCode = (int)HttpStatusCode.Unauthorized;
                message = ex.Message;
                break;
        }

        context.Response.StatusCode = statusCode;

        var response = new
        {
            StatusCode = statusCode,
            Message = message,
            Detailed = statusCode == 500 ? exception.Message : null 
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
