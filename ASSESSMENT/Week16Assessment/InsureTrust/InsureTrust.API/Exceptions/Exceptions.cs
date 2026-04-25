using System.Text.Json;
using FluentValidation;
using InsureTrust.API.Common;
using Serilog;

namespace InsureTrust.API.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message) : base(message) { }
}

public class ForbiddenException : Exception
{
    public ForbiddenException(string message) : base(message) { }
}

/// <summary>
/// Global exception middleware that catches all unhandled exceptions and maps them
/// to <see cref="ApiResponse{T}"/> payloads with appropriate HTTP status codes.
/// FluentValidation <see cref="ValidationException"/> is handled here too.
/// All errors are logged via Serilog.
/// </summary>
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException vex)
        {
            // FluentValidation errors → 400 with full error list
            Log.Warning("Validation failure on {Path}: {Errors}",
                context.Request.Path,
                string.Join("; ", vex.Errors.Select(e => e.ErrorMessage)));

            var errors = vex.Errors.Select(e => e.ErrorMessage);
            await WriteResponseAsync(context, 400,
                ApiResponse<object>.Fail("One or more validation errors occurred.", 400, errors));
        }
        catch (Exception ex)
        {
            var (statusCode, logLevel) = ex switch
            {
                NotFoundException  => (404, "warning"),
                BadRequestException => (400, "warning"),
                UnauthorizedException => (401, "warning"),
                ForbiddenException => (403, "warning"),
                _ => (500, "error")
            };

            if (logLevel == "error")
                Log.Error(ex, "Unhandled exception on {Method} {Path}", context.Request.Method, context.Request.Path);
            else
                Log.Warning("Business exception [{Type}] on {Method} {Path}: {Message}",
                    ex.GetType().Name, context.Request.Method, context.Request.Path, ex.Message);

            var message = statusCode == 500
                ? "An unexpected error occurred. Please try again later."
                : ex.Message;

            await WriteResponseAsync(context, statusCode,
                ApiResponse<object>.Fail(message, statusCode));
        }
    }

    private static async Task WriteResponseAsync<T>(HttpContext context, int statusCode, ApiResponse<T> response)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        await context.Response.WriteAsync(json);
    }
}
