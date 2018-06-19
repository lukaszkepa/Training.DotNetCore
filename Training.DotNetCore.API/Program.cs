using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Training.DotNetCore.API
{
    public class Program
    {
        public static void Main(string[] args)
        {          
            BuildWebHost(args).EnsureDbCreated().Run();

            //BuildCustomWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) => {
                    config.AddJsonFile("mysettings.json");
                })
                .UseStartup<Startup>()
                //.UseStartup<StartupManyEnvironments>()
                //.UseStartup(typeof(Program).Assembly.GetName().Name)
                .Build();

        public static IWebHost BuildCustomWebHost(string[] args)
        {
            return new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .AddCommandLine(args);
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging
                        .AddConfiguration(hostingContext.Configuration.GetSection("Logging"))
                        .AddConsole()
                        .AddDebug();
                })
                .UseIISIntegration()
                .CaptureStartupErrors(true)
                .UseDefaultServiceProvider((hostingContext, options) => {
                    // Performs check to verify if Scoped services aren't directly or indirectly injected into singletons.
                    options.ValidateScopes = hostingContext.HostingEnvironment.IsDevelopment();
                })
                .UseStartup<Startup>()
                .Build();
        }
    }
}
