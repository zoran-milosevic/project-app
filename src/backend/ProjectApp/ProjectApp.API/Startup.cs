using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ProjectApp.API
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        private readonly string _corsPolicyName;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _corsPolicyName = configuration["App:CorsPolicyName"];
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureHsts(_configuration);
            services.ConfigureHttpsRedirection(_configuration);
            services.ConfigureCors(_configuration);
            services.ConfigureDbContext(_configuration);
            services.ConfigureAuthentication(_configuration);
            services.RegisterDependencies();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectApp.API", Version = "v1" });
            });

            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureGlobalErrorHandling(env);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectApp.API v1"));
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseCors(_corsPolicyName);
            // app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
