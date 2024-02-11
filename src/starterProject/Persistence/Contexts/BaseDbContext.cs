using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NArchitecture.Core.Security.Entities;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<EmailAuthenticator<int, int>> EmailAuthenticators { get; set; }
    public DbSet<OperationClaim<int, int>> OperationClaims { get; set; }
    public DbSet<OtpAuthenticator<int, int>> OtpAuthenticators { get; set; }
    public DbSet<RefreshToken<int, int>> RefreshTokens { get; set; }
    public DbSet<User<int, int>> Users { get; set; }
    public DbSet<UserOperationClaim<int, int>> UserOperationClaims { get; set; }

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration)
        : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
