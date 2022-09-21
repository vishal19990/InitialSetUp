using Skillhouse.HRportal.Core.Models;
using Skillhouse.HRportal.Entity;
using Skillhouse.HRportal.Repository.Contracts;

namespace Skillhouse.HRportal.Repository.Infrastructure
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(HRportalDbContext hRportalDbContext, ApplicationContext applicationContext)
            : base(hRportalDbContext, applicationContext)
        {
        }
    }
}
