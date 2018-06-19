using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Training.DotNetCore.API.Configuration
{
    public class ApiErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _env;
        private readonly ILogger<ApiErrorHandlingMiddleware> _logger;
        
        public ApiErrorHandlingMiddleware(RequestDelegate next, IHostingEnvironment env, ILogger<ApiErrorHandlingMiddleware> logger)
        {
            _next = next;
            _env = env;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            var response = new Dictionary<string, object>();
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;

                response.Add("errorMessage", ex.Message);
                if (_env.IsDevelopment())
                {
                    response.Add("errorDetails", ex.StackTrace);
                }
            }
            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                response.Add("code", context.Response.StatusCode);
                var json = JsonConvert.SerializeObject(response);
                await context.Response.WriteAsync(json);
            }
        }
    }
}