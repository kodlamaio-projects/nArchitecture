using Core.Security.Entities;
using Core.Test.Application.FakeData;

namespace StarterProject.Application.Tests.Mocks.FakeDatas
{
    public class RefreshTokenFakeData : BaseFakeData<RefreshToken, int>
    {
        public override List<RefreshToken> CreateFakeData() =>
            new List<RefreshToken>()
            {
                new() { UserId = 1, Token = "abc" }
            };
    }
}
