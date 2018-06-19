using Training.DotNetCore.DA;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Training.DotNetCore.API
{
    public static class IWebHostExtensions
    {
        /// <summary>Ensures that the database is created. Will NOT run the latest migrations if the database aleady exist.</summary>
        public static IWebHost EnsureDbCreated(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<DotNetCoreTrainingContext>();
                context.Database.EnsureCreated();
            }
            return host;
        }
    }
}