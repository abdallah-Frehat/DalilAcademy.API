using DalilAcademy.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace DalilAcademy.API.Middleware
{
    public class GlobalExceptionHandler(
        IProblemDetailsService problemDetails,
        ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext context, Exception exception, CancellationToken ct)
        {
            var (statusCode, title) = exception switch
            {
                // إذا كانت الـ ValidationException خاصة بـ FluentValidation
                FluentValidation.ValidationException => (StatusCodes.Status400BadRequest, "Validation failed"),
                NotFoundException => (StatusCodes.Status404NotFound, "Resource not found"),
                ConflictException => (StatusCodes.Status409Conflict, "Conflict"),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
            };

            if (statusCode == StatusCodes.Status500InternalServerError)
                logger.LogError(exception, "Unhandled exception");

            context.Response.StatusCode = statusCode;

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception is NotFoundException or ConflictException ? exception.Message : null
            };

            // معالجة أخطاء FluentValidation وتجميعها بشكل صحيح
            if (exception is FluentValidation.ValidationException ve)
            {
                problem.Extensions["errors"] = ve.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            }

            return await problemDetails.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = context,
                Exception = exception,
                ProblemDetails = problem
            });
        }
    }
}