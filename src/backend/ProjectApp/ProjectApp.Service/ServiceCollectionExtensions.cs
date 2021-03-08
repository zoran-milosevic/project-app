using Microsoft.Extensions.DependencyInjection;
using ProjectApp.Interface.Service;

namespace ProjectApp.Service
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDependenciesFor_Service(this IServiceCollection services)
        {
            services.AddTransient<IUserProfileService, UserProfileService>();
            services.AddTransient<ITextService, TextService>();
        }
    }
}
