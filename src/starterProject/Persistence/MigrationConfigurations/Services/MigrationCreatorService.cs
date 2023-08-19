using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.MigrationConfigurations.Services;
public class MigrationCreatorService : IMigrationCreatorService
{
    private readonly BaseDbContext _context;

    public MigrationCreatorService(BaseDbContext context)
    {
        _context = context;
    }

    public void Initialze()
    {
        if (_context.Database.CanConnect())
            _context.Database.Migrate();
    }
}