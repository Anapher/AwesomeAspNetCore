using AwesomeAspApp.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace AwesomeAspApp.Infrastructure.Identity
{
    public class AppIdentityDbContextFactory : DesignTimeDbContextFactoryBase<AppIdentityDbContext>
    {
        protected override AppIdentityDbContext CreateNewInstance(DbContextOptions<AppIdentityDbContext> options)
        {
            return new AppIdentityDbContext(options);
        }
    }
}