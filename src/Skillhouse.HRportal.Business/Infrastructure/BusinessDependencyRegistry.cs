using Microsoft.Extensions.DependencyInjection;
using Skillhouse.HRportal.Common;
using Skillhouse.HRportal.Repository.Infrastructure;

namespace Skillhouse.HRportal.Business.Infrastructure
{
    public static class BusinessDependencyRegistry
    {
        public static void RegisterDependency(this IServiceCollection services, AppSettings appSettings)
        {
            RepositoryDependencyRegistry.DependencyRegistry(services, appSettings);

            services.AddTransient<UserBusiness>();
        }
    }
}
