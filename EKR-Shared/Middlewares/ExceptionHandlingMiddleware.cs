using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EKR_Shared.Middlewares
{
    /// <summary>
    /// Middleware для обработки и логирования ошибок.
    /// </summary>
    /// <param name="next">Следующий Middleware.</param>
    /// <param name="logger">Логгер.</param>
    public class ExceptionHandlingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        /// <summary>
        /// Асинхронно отлавливает ошибки и передаёт управление следующей middleware.
        /// </summary>
        /// <param name="context">Http контекст.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InvalidOperationException ex)
            {
                Log.Warning(ex, "Business logic error");
                await WriteProblemDetailsAsync(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Log.Warning(ex, "Unauthorized access");
                await WriteProblemDetailsAsync(context, HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                Log.Warning(ex, "Not found error");
                await WriteProblemDetailsAsync(context, HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unhandled exception");
                await WriteProblemDetailsAsync(context, HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        private static async Task WriteProblemDetailsAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)statusCode;

            var problem = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = message,
                Detail = statusCode == HttpStatusCode.InternalServerError ? null : message,
                Instance = context.Request.Path
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(problem, options);
            await context.Response.WriteAsync(json);
        }
    }
}
