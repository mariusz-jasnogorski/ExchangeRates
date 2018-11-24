using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

using ExchangeRates.API.Controllers;


namespace ExchangeRates.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;


        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ExchangeRatesController> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Unexpected exception");

            var code = HttpStatusCode.InternalServerError; // 500 if unexpected            

            var result = JsonConvert.SerializeObject(new { error = exception.Message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
