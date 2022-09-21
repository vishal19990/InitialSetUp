using Skillhouse.HRportal.Business.Infrastructure;
using Skillhouse.HRportal.Common;
using Skillhouse.HRportal.Core.Models;

namespace Skillhouse.HRportal.Infrastructure
{
    public static class DependencyRegistry
    {
        public static void RegisterDependency(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(appSettings);
            services.AddScoped<ApplicationContext>();
            BusinessDependencyRegistry.RegisterDependency(services, appSettings);
        }
    }
}
