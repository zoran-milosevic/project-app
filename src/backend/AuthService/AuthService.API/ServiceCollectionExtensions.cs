using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthService.Configuration;
using AuthService.Service;

namespace AuthService.API
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

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureIdentity();
            services.ConfigureTokenAuthentication(configuration);
        }

        public static void ConfigureGlobalErrorHandling(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    if (error != null)
                    {
                        await context.Response.WriteAsync(new
                        {
                            StatusCode = context.Response.StatusCode,
                            ErrorMessage = error.Error.Message
                        }.ToString());
                    }
                });
            });
        }

        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDependenciesFor_Service(configuration);
        }
    }
}