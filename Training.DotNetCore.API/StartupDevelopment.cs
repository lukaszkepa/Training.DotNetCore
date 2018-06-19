using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Training.DotNetCore.API
{
    public class StartupDevelopment
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory log)
        {
            var logger = log.CreateLogger<StartupDevelopment>();
            app.Use(async (context, next) =>
            {
                logger.LogInformation("Hello World from 1st custom middleware!");
                await next();
                logger.LogInformation("Goodbye World from 1st custom middleware!");
            });

            app.Use(async (context, next) =>
            {
                logger.LogInformation("Hello World from 2nd custom middleware!");
                await next();
                logger.LogInformation("Goodbye World from 2nd custom middleware!");
            });

            app.Run(async context => 
            {
                await context.Response.WriteAsync("This is your response!");
            });
        }
    }
}