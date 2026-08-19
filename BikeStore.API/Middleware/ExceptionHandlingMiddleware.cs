using System.Net;
using System.Text.Json;
using BikeStore.API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, title) = exception switch
            {
                NotFoundException => (HttpStatusCode.NotFound, exception.Message),
                BusinessRuleException => (HttpStatusCode.BadRequest, exception.Message),
                DbUpdateException => (HttpStatusCode.Conflict, "No se pudo completar la operacion por una restriccion de datos relacionados."),
                _ => (HttpStatusCode.InternalServerError, "Ocurrio un error inesperado al procesar la solicitud.")
            };

            if (statusCode == HttpStatusCode.InternalServerError)
                _logger.LogError(exception, "Error no controlado procesando {Path}", context.Request.Path);
            else
                _logger.LogWarning(exception, "Error controlado procesando {Path}", context.Request.Path);

            var problemDetails = new
            {
                status = (int)statusCode,
                title,
                instance = context.Request.Path.Value
            };

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }
}
