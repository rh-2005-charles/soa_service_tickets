/* using System.Text;
using ic_tienda_utils.Exceptions;
using ic_tienda_business.Dtos.Otros;
using System.Diagnostics;
using ic_tienda_data.sources.BaseDeDatos.Models.ActivityLogs;
using ic_tienda_data.sources.BaseDeDatos;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.IdentityModel.Tokens;

namespace ic_tienda.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorMiddleware> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Obtener información detallada del error
            var stackTrace = new StackTrace(exception, true);
            var frame = stackTrace.GetFrame(0);
            var method = frame?.GetMethod();

            var error = new Error
            {
                Url = context.Request.Path,
                Controller = context.GetRouteValue("controller")?.ToString(),
                Ip = context.Connection.RemoteIpAddress?.ToString(),
                Method = context.Request.Method,
                UserAgent = context.Request.Headers["User-Agent"].ToString(),
                Host = context.Request.Host.ToString(),
                ClassComponent = method?.DeclaringType?.FullName,
                FunctionName = method?.Name,
                LineNumber = frame?.GetFileLineNumber() ?? 0,
                Error1 = exception.Message,
                StackTrace = exception.StackTrace,
                Request = await GetRequestBody(context),
                ErrorCode = (exception as CustomException)?.errorCode ?? 0,
                Status = (short)GetStatusCode(exception),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await LogErrorToDatabase(error);

            var result = new GeneralResponse
            {
                TitleMessage = "Ocurrió un error",
                TextMessage = exception is CustomException ? exception.Message : "Consulte con sistemas"
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = GetStatusCode(exception);
            await context.Response.WriteAsJsonAsync(result);
        }

        // Método para determinar el código de estado HTTP basado en el tipo de excepción
        private int GetStatusCode(Exception exception)
        {
            return exception switch
            {
                CustomException ex => ex.httpCode,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                SecurityTokenExpiredException => (int)HttpStatusCode.Unauthorized,
                SecurityTokenValidationException => (int)HttpStatusCode.Unauthorized,
                MySqlException => (int)HttpStatusCode.InternalServerError,
                DbUpdateException => (int)HttpStatusCode.InternalServerError,
                _ => (int)HttpStatusCode.InternalServerError
            };
        }

        private async Task<string> GetRequestBody(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering();
                string requestBody = string.Empty;

                if (context.Request.ContentType?.Contains("multipart/form-data") == true)
                {
                    var form = await context.Request.ReadFormAsync();
                    var name = form["Name"];
                    var description = form["Description"];
                    var imageFile = form.Files.Count > 0 ? form.Files[0].FileName : null;
                    requestBody = $"Name: {name}, Description: {description}, ImageFile: {imageFile}";
                }
                else
                {
                    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
                    {
                        requestBody = await reader.ReadToEndAsync();
                    }
                }

                context.Request.Body.Position = 0;
                return requestBody;
            }
            catch
            {
                return "No se pudo leer el cuerpo de la solicitud";
            }
        }

        private async Task LogErrorToDatabase(Error error)
        {
            try
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IcTiendaDbContext>();

                dbContext.Errors.Add(error);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar el error en la base de datos");
            }
        }
    }
} */