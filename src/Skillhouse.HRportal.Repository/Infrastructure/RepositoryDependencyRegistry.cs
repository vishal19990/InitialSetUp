using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Skillhouse.HRportal.Common;
using Skillhouse.HRportal.Repository.Contracts;

namespace Skillhouse.HRportal.Repository.Infrastructure
{
    public static class RepositoryDependencyRegistry
    {
        public static void DependencyRegistry(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddDbContext<HRportalDbContext>(options =>
            {
                options.UseSqlServer(appSettings.SkillhouseDbConnectionString);
            });

            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
