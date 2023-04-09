using Application.Services.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterProject.Tests.Mocks.Repositories.Auth
{
    public static class MockEmailAuthenticatorRepository
    {
        public static IEmailAuthenticatorRepository GetEmailAuthenticatorRepositoryMock()
        {
            var mockRepo = new Mock<IEmailAuthenticatorRepository>();
            return mockRepo.Object;
        }
    }
}
