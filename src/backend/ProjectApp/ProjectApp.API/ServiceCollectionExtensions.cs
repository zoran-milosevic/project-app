using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectApp.Service;
using ProjectApp.Data;

namespace ProjectApp.API
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureHsts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHsts(options =>
            {
                options.Preload = Convert.ToBoolean(configuration["Hsts:preload"]);
                options.IncludeSubDomains = Convert.ToBoolean(configuration["Hsts:includeSubDomains"]);
                options.MaxAge = TimeSpan.FromDays(Convert.ToInt16(configuration["Hsts:maxAgeInDays"]));

                foreach (var host in configuration.GetSection("Hsts:excludedHosts").GetChildren().ToArray().Select(c => c.Value).ToArray())
                {
                    options.ExcludedHosts.Add(host);
                }
            });
        }

        public static void ConfigureHttpsRedirection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status308PermanentRedirect;
                options.HttpsPort = Convert.ToInt16(configuration["https_port"]);
            });
        }

        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            var enableCorsOriginsOnly = Convert.ToBoolean(configuration["App:EnableCorsOriginsOnly"]);
            var corsPolicyName = configuration["App:CorsPolicyName"];
            var corsOrigins = configuration["App:CorsOrigins"];

            services.AddCors(options =>
            {
                options.AddPolicy(corsPolicyName, builder =>
                {
                    if (enableCorsOriginsOnly)
                    {
                        // define origins in appsettings.json 

                        builder.WithOrigins(
                            corsOrigins // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.TrimEnd('/'))
                                .ToArray()
                            );
                    }
                    else
                    {
                        builder.AllowAnyOrigin();
                    }

                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    //builder.AllowCredentials();
                });
            });
        }

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            // services.ConfigureAuthDbContext(configuration);
            services.ConfigureAppDbContext(configuration);
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // services.ConfigureIdentity();
            // services.ConfigureTokenAuthentication(configuration);
        }

        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddDependenciesFor_Service();
            services.AddDependenciesFor_Data();
        }
    }
}