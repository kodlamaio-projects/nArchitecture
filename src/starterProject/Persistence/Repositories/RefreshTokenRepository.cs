using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Persistence.Repositories;
using NArchitecture.Core.Security.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class RefreshTokenRepository : EfRepositoryBase<RefreshToken<int, int>, int, BaseDbContext>, IRefreshTokenRepository
{
    public RefreshTokenRepository(BaseDbContext context)
        : base(context) { }

    public async Task<List<RefreshToken<int, int>>> GetOldRefreshTokensAsync(int userID, int refreshTokenTTL)
    {
        List<RefreshToken<int, int>> tokens = await Query()
            .AsNoTracking()
            .Where(r =>
                r.UserId == userID
                && r.RevokedDate == null
                && r.ExpiresDate >= DateTime.UtcNow
                && r.CreatedDate.AddDays(refreshTokenTTL) <= DateTime.UtcNow
            )
            .ToListAsync();

        return tokens;
    }
}
