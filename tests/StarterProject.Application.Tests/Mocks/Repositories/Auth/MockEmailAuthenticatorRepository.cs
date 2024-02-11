using Application.Services.Repositories;
using Moq;

namespace StarterProject.Application.Tests.Mocks.Repositories.Auth;

public static class MockEmailAuthenticatorRepository
{
    public static IEmailAuthenticatorRepository GetEmailAuthenticatorRepositoryMock()
    {
        var mockRepo = new Mock<IEmailAuthenticatorRepository>();
        return mockRepo.Object;
    }
}
