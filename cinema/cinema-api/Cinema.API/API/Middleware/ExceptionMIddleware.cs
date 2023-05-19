using DatingApp.Common.Errors;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var details = e.Message;
                var message = e.Message;

                if (e is ApiException apiException)
                {
                    context.Response.StatusCode = apiException.StatusCode;
                    details = apiException.Details;
                    message = apiException.Message;
                }
                Console.WriteLine(details);


                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, message, e.StackTrace?.ToString())
                    : new ApiException(context.Response.StatusCode, message, "Internal Server Error");
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
