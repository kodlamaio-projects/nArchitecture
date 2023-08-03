using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts;
public class MsSqlDbContext : BaseDbContext
{
    public MsSqlDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions, configuration)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            base.OnConfiguring(
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("MsSql")));
    }
}
