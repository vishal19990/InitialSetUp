using Microsoft.EntityFrameworkCore;

namespace Skillhouse.HRportal.Repository
{
    public partial class HRportalDbContext : DbContext
    {
        public async Task<int> SaveAsync()
        {
            return await this.SaveChangesAsync();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}