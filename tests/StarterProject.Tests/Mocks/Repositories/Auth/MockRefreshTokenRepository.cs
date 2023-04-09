using Application.Services.Repositories;
using Core.Security.Entities;
using Moq;
using StarterProject.Tests.Mocks.FakeDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterProject.Tests.Mocks.Repositories.Auth
{
    public class MockRefreshTokenRepository
    {
        public static IRefreshTokenRepository GetMockRefreshTokenRepository()
        {
            var tokens = RefreshTokenFakeData.Data;
            var mockRepo = new Mock<IRefreshTokenRepository>();
            mockRepo.Setup(s => s.GetOldRefreshTokensAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(() => tokens);
            return mockRepo.Object;
        }
    }
}
