using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthService.Data;
using AuthService.Interface.Service;

namespace AuthService.Service
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDependenciesFor_Service(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITokenService, TokenService>();
            
            services.AddDependenciesFor_Data(configuration);
        }
    }
}