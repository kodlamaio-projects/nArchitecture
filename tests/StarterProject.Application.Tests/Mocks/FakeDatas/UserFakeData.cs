using System;
using System.Collections.Generic;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Test.Application.FakeData;

namespace StarterProject.Application.Tests.Mocks.FakeDatas;

public class UserFakeData : BaseFakeData<User<int, int>, int>
{
    public override List<User<int, int>> CreateFakeData()
    {
        byte[] passwordHash,
            passwordSalt;
        HashingHelper.CreatePasswordHash("123456", out passwordHash, out passwordSalt);

        int id = 0;
        List<User<int, int>> data =
            new()
            {
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
            };
        return data;
    }
}
