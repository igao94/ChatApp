using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Middleware;

internal sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
    }

    private static async Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
    {
        Dictionary<string, string[]> validationErrors = [];

        if (ex.Errors is not null)
        {
            foreach (var error in ex.Errors)
            {
                if (validationErrors.TryGetValue(error.PropertyName, out var exstingErrors))
                {
                    validationErrors[error.PropertyName] = exstingErrors
                        .Append(error.ErrorMessage).ToArray();
                }
                else
                {
                    validationErrors[error.PropertyName] = new[] { error.ErrorMessage };
                }
            }
        }

        context.Response.StatusCode = StatusCodes.Status400BadRequest;

        var validationProblemDetails = new ValidationProblemDetails(validationErrors)
        {
            Status = context.Response.StatusCode,
            Type = "Validation failed",
            Title = "Validation error",
            Detail = "One or more validation error occurred"
        };

        await context.Response.WriteAsJsonAsync(validationProblemDetails);
    }
}
