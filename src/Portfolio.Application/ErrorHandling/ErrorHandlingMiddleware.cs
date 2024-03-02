using Portfolio.Domain.Dto.Result;
using Portfolio.Domain.Dto.ValidationError;
using Portfolio.Domain.Exceptions;
using System.Text.Json;

namespace Portfolio.Application.ErrorHandling
{
    public class ErrorHandlingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException exception)
            {
                var result = new ResultDto
                {
                    IsSuccess = false,
                    ErrorType = exception.GetType().Name,
                    Error = "Validation Error",
                    ValidationErrors = []
                };

                if (exception.Errors is not null && exception.Errors.Count > 0)
                {
                    result.ValidationErrors = exception.Errors.Select(x => new ValidationErrorDto
                    {
                        Property = x.Property,
                        ErrorMessage = x.ErrorMessage
                    }).ToList();
                }

                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(JsonSerializer.Serialize(result));
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var result = new ResultDto
            {
                IsSuccess = false,
                ErrorType = exception.GetType().Name,
                Error = exception?.InnerException?.Message ?? exception.Message,
                ValidationErrors = []
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
}
