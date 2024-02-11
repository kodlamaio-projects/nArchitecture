using Application.Services.Repositories;
using Moq;

namespace StarterProject.Application.Tests.Mocks.Repositories.Auth;

public static class MockOtpAuthRepository
{
    public static IOtpAuthenticatorRepository GetOtpAuthenticatorMock()
    {
        var mockRepo = new Mock<IOtpAuthenticatorRepository>();
        return mockRepo.Object;
    }
}
