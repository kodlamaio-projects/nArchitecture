using NArchitecture.Core.Security.Entities;
using NArchitecture.Core.Test.Application.FakeData;

namespace StarterProject.Application.Tests.Mocks.FakeDatas;

public class RefreshTokenFakeData : BaseFakeData<RefreshToken<int, int>, int>
{
    public override List<RefreshToken<int, int>> CreateFakeData()
    {
        return [new() { UserId = 1, Token = "abc" }];
    }
}
