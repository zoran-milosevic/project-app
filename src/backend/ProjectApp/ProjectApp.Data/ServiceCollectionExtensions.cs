using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectApp.Data.Context;
using ProjectApp.Data.Repository;
using ProjectApp.Interface.Repository;

namespace ProjectApp.Data
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureAuthDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AuthConnection");

            // services.AddDbContext<AuthDbContext>(options =>
            // {
            //     options.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromHours(24).TotalSeconds));
            // });
        }

        public static void ConfigureAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AppConnection");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromHours(24).TotalSeconds));
            });
        }

        public static void AddDependenciesFor_Data(this IServiceCollection services)
        {
            // services.AddScoped<AuthDbContext>();
            services.AddScoped<AppDbContext>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<ITextRepository, TextRepository>();
        }
    }
}
