using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts;

public class InMemoryDbContext : BaseDbContext
{
    public InMemoryDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions, configuration)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            base.OnConfiguring(
                optionsBuilder.UseInMemoryDatabase("InMemoryDb"));
    }
}
