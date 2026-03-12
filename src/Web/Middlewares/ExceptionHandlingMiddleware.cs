using System.Text.Json;
using FluentValidation;

namespace Web;

public sealed class ExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray());

            var payload = new
            {
                error = "Validation failed",
                details = errors
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
        }
        catch (KeyNotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Response.ContentType = "application/json";

            var payload = new { error = ex.Message };
            await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
        }
        catch (ArgumentException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var payload = new { error = ex.Message };
            await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
        }
        catch (Exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var payload = new { error = "Internal server error" };
            await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
        }
    }
}
