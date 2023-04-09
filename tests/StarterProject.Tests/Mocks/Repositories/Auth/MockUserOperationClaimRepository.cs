using Application.Services.Repositories;
using Core.Security.Entities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Nest;
using StarterProject.Tests.Mocks.FakeDatas;

namespace StarterProject.Tests.Mocks.Repositories.Auth
{
    public class MockUserOperationClaimRepository
    {
        public static IUserOperationClaimRepository GetMockUserOperationClaimRepository()
        {
            var operationClaims = OperationClaimFakeData.Data;
            var mockRepo = new Mock<IUserOperationClaimRepository>();

            mockRepo
                .Setup(s => s.GetOperationClaimsByUserIdAsync(It.IsAny<int>()))
                .ReturnsAsync(
                    (int userId) =>
                    {
                        var claims = operationClaims.ToList();
                        return claims;
                    }
                );

            return mockRepo.Object;
        }
    }
}
