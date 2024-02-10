using Core.Security.Entities;
using Core.Test.Application.FakeData;

namespace StarterProject.Application.Tests.Mocks.FakeDatas
{
    public class RefreshTokenFakeData : BaseFakeData<RefreshToken<int, int>, int>
    {
        public override List<RefreshToken<int, int>> CreateFakeData() =>
            new List<RefreshToken<int, int>>()
            {
                new() { UserId = 1, Token = "abc" }
            };
    }
}
