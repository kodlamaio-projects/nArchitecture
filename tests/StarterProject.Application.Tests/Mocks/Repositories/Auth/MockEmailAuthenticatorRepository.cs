using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using Moq;

namespace StarterProject.Application.Tests.Mocks.Repositories.Auth
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
