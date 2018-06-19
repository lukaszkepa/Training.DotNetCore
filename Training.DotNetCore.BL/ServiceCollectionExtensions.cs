using Microsoft.Extensions.DependencyInjection;
using Training.DotNetCore.DA;
using Training.DotNetCore.BL.Services;
using Training.DotNetCore.BL.MapperProfiles;
using AutoMapper;

namespace Training.DotNetCore.BL
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>Add BL layer dependencies.</summary>
        public static IServiceCollection AddBL(this IServiceCollection services)
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<TrainingMapperProfile>();
            });

            services.AddSingleton(Mapper.Instance);
            services.AddScoped<ITrainingService, TrainingService>();
            
            return services;
        }
    }
}