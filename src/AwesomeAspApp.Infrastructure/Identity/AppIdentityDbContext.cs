#pragma warning disable CS8618 // Non-nullable field is uninitialized. Fields are automatically initialized by EF Core

using AwesomeAspApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AwesomeAspApp.Infrastructure.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }

        public DbSet<RefreshToken> AspNetRefreshTokens { get; set; }
    }
}