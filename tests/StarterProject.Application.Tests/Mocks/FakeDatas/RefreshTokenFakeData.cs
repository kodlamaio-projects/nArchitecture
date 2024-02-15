using Domain.Entities;
using NArchitecture.Core.Test.Application.FakeData;

namespace StarterProject.Application.Tests.Mocks.FakeDatas;

public class RefreshTokenFakeData : BaseFakeData<RefreshToken, Guid>
{
    public override List<RefreshToken> CreateFakeData()
    {
        return [new() { UserId = UserFakeData.Ids[0], Token = "abc" }];
    }
}
