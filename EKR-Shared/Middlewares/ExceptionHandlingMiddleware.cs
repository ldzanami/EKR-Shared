using System.Net;
using System.Text.Json;
using EKR_Shared.Exceptions;
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
            catch (ServerSideException ex)
            {
                Log.Warning(ex.ToString());
                await WriteProblemDetailsAsync(context, HttpStatusCode.InternalServerError, ex);
            }
            catch (ClientSideException ex)
            {
                Log.Warning(ex.ToString());
                await WriteProblemDetailsAsync(context, HttpStatusCode.BadRequest, ex);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "UnexpectedError");
                await WriteProblemDetailsAsync(context, HttpStatusCode.InternalServerError, new EKRException(ex.Message, ex, "UnexpectedError"));
            }
        }

        private static async Task WriteProblemDetailsAsync(HttpContext context, HttpStatusCode statusCode, EKRException ex)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)statusCode;

            var problem = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = ex.Type,
                Detail = ex.ToString(),
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
