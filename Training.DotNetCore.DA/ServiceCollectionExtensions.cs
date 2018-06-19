using System;
using Training.DotNetCore.DA.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Training.DotNetCore.DA
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>Add DA layer dependencies.</summary>
        public static IServiceCollection AddDA(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
            services.AddDbContext<DotNetCoreTrainingContext>(opt => opt.UseSqlServer(connectionString));

            services.AddScoped<ITrainingRepository, TrainingRepository>();

            return services;
        }
    }
}