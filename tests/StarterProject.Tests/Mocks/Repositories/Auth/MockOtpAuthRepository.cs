using Application.Services.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterProject.Tests.Mocks.Repositories.Auth
{
    public static class MockOtpAuthRepository
    {
        public static IOtpAuthenticatorRepository GetOtpAuthenticatorMock()
        {
            var mockRepo = new Mock<IOtpAuthenticatorRepository>();
            return mockRepo.Object;
        }
    }
}
