using Application.Services.Repositories;
using Moq;
using StarterProject.Application.Tests.Mocks.FakeDatas;

namespace StarterProject.Application.Tests.Mocks.Repositories.Auth;

public class MockUserOperationClaimRepository
{
    private readonly OperationClaimFakeData _operationClaimFakeData;

    public MockUserOperationClaimRepository(OperationClaimFakeData operationClaimFakeData)
    {
        _operationClaimFakeData = operationClaimFakeData;
    }

    public IUserOperationClaimRepository GetMockUserOperationClaimRepository()
    {
        List<Domain.Entities.OperationClaim> operationClaims = _operationClaimFakeData.Data;
        var mockRepo = new Mock<IUserOperationClaimRepository>();

        mockRepo
            .Setup(s => s.GetOperationClaimsByUserIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(
                (Guid userId) =>
                {
                    var claims = operationClaims.ToList();
                    return claims;
                }
            );

        return mockRepo.Object;
    }
}
