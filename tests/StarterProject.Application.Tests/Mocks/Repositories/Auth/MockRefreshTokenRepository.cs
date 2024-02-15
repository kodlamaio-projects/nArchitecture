using Application.Services.Repositories;
using Moq;
using StarterProject.Application.Tests.Mocks.FakeDatas;

namespace StarterProject.Application.Tests.Mocks.Repositories.Auth;

public class MockRefreshTokenRepository
{
    private readonly RefreshTokenFakeData _refreshTokenFakeData;

    public MockRefreshTokenRepository(RefreshTokenFakeData refreshTokenFakeData)
    {
        _refreshTokenFakeData = refreshTokenFakeData;
    }

    public IRefreshTokenRepository GetMockRefreshTokenRepository()
    {
        List<Domain.Entities.RefreshToken> tokens = _refreshTokenFakeData.Data;
        var mockRepo = new Mock<IRefreshTokenRepository>();
        mockRepo.Setup(s => s.GetOldRefreshTokensAsync(It.IsAny<Guid>(), It.IsAny<int>())).ReturnsAsync(() => tokens);
        return mockRepo.Object;
    }
}
