using NArchitecture.Core.Persistence.Repositories;
using NArchitecture.Core.Security.Entities;

namespace Application.Services.Repositories;

public interface IRefreshTokenRepository : IAsyncRepository<RefreshToken<int, int>, int>, IRepository<RefreshToken<int, int>, int>
{
    Task<List<RefreshToken<int, int>>> GetOldRefreshTokensAsync(int userID, int refreshTokenTTL);
}
