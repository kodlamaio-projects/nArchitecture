using NArchitecture.Core.Security.Entities;
using NArchitecture.Core.Security.Hashing;
using NArchitecture.Core.Test.Application.FakeData;

namespace StarterProject.Application.Tests.Mocks.FakeDatas;

public class UserFakeData : BaseFakeData<User<int, int>, int>
{
    public override List<User<int, int>> CreateFakeData()
    {
        HashingHelper.CreatePasswordHash("123456", out byte[] passwordHash, out byte[] passwordSalt);

        int id = 0;
        List<User<int, int>> data =
        [
            new User<int, int>
            {
                Id = ++id,
                Email = "example@kodlama.io",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            },
            new User<int, int>
            {
                Id = ++id,
                Email = "example2@kodlama.io",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            },
            new User<int, int>
            {
                Id = ++id,
                Email = "example3@kodlama.io",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            }
        ];
        return data;
    }
}
