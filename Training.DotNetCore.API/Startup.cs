using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Training.DotNetCore.BL;
using Training.DotNetCore.DA;
using Microsoft.AspNetCore.Mvc;
using Training.DotNetCore.API.Configuration;

namespace Training.DotNetCore.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                ;

            services.AddOptions();
            services.Configure<MySettings>(Configuration.GetSection("MySettings"));

            services.AddSwaggerGen(s => s.SwaggerDoc("v1", new Info { Title = "DotNetCore Training API", Version = "v1" }));

            services.AddBL();

            var connectionString = Configuration.GetConnectionString("DotNetCoreTraining");
            services.AddDA(connectionString);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ApiErrorHandlingMiddleware>();

            // Add routing and configure MVC as default request handler
            app.UseMvc();
            
            app.UseSwagger()
               .UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "DotNetCore Training API"));
        }
    }
}
