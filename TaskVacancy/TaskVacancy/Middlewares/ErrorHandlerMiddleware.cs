using Newtonsoft.Json;
using TaskVacancy.Exceptions;
using TaskVacancy.Services;

namespace TaskVacancy.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogService logService;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogService logService)
        {
            _next = next;
            this.logService = logService;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AccessDeniedException)
            {
                await HandleCustomError(context, "permissionError", 401);
            }
            catch (WalletException ex)
            {
                await HandleCustomError(context, ex.Message);
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(context, exception);
            }
        }

        private Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var message = $"{exception.Message} \n{exception.StackTrace}";
            if (exception.InnerException != null)
                message = $"{message}\n {exception.InnerException.Message}\n " +
                    $"{exception.InnerException.StackTrace}";

            return HandleCustomError(context, message);
        }

        private Task HandleCustomError(HttpContext context, string message, int statusCode = 500)
        {
            var response = new { message = message };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            logService.Crash(message);
            return context.Response.WriteAsync(payload);
        }
    }
}
