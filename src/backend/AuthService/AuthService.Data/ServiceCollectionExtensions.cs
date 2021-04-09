using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthService.Data.Repository;
using AuthService.Interface.Repository;
using AuthService.Data.Context;

namespace AuthService.Data
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureAuthDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AuthConnection");

            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromHours(24).TotalSeconds));
            });
        }

        public static void AddDependenciesFor_Data(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureAuthDbContext(configuration);

            services.AddScoped<AuthDbContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
        }
    }
}