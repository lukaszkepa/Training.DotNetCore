using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Training.DotNetCore.API
{
    public class StartupManyEnvironments
    {
        private RequestDelegate GetRequestHanlder(IHostingEnvironment env)
        {
            var resposneText = $"This is your response from a '{env.EnvironmentName}' environment!";
            return async context => await context.Response.WriteAsync(resposneText);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Run(GetRequestHanlder(env));
        }

        public void ConfigureDevelopment(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Run(GetRequestHanlder(env));
        }

        public void ConfigureStaging(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Run(GetRequestHanlder(env));
        }
    }
}