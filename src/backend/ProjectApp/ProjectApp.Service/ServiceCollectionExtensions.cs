using Microsoft.Extensions.DependencyInjection;
using ProjectApp.Interface.Factory;
using ProjectApp.Interface.Service;
using ProjectApp.Service.Factory;

namespace ProjectApp.Service
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDependenciesFor_Service(this IServiceCollection services)
        {
            services.AddTransient<IUserProfileService, UserProfileService>();
            services.AddTransient<ITextService, TextService>();
            services.AddTransient<ITextFactory, TextFactory>();
        }
    }
}
