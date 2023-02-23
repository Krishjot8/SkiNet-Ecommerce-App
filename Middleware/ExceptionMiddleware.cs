using ECommerce_App.Errors;
using System.Net;
using System.Text.Json;

namespace ECommerce_App.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger <ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context) // ExceptionHandling Middleware
        {
            try
            { 

                await _next(context);
                //if no exception it will move on next stage
            
            }
            catch(Exception ex)
            { //if there is an exception we will catch it

                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

               
                //if in development
                var response = _environment.IsDevelopment()
                    ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message,
                    ex.StackTrace.ToString())
                  : new ApiException((int)HttpStatusCode.InternalServerError); //if in production 


                var options = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};


                var json = JsonSerializer.Serialize(response);


                await context.Response.WriteAsync(json);
            }

        }
    }
}
