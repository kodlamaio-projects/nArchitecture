using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Test.Application.FakeData;
using System;
using System.Collections.Generic;

namespace StarterProject.Application.Tests.Mocks.FakeDatas;

public class UserFakeData : BaseFakeData<User, int>
{
    public override List<User> CreateFakeData()
    {
        byte[] passwordHash,
            passwordSalt;
        HashingHelper.CreatePasswordHash("123456", out passwordHash, out passwordSalt);

        int id = 0;
        List<User> data =
            new()
            {
                new User
                {
                    Id = ++id,
                    FirstName = "Engin",
                    LastName = "Demiroğ",
                    Email = "example@email.com",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new User
                {
                    Id = ++id,
                    FirstName = "Ahmet",
                    LastName = "Çetinkaya",
                    Email = "example2@email.com",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new User
                {
                    Id = ++id,
                    FirstName = "Halit",
                    LastName = "Kalayci",
                    Email = "halit@kodlama.io",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            };
        return data;
    }
}
