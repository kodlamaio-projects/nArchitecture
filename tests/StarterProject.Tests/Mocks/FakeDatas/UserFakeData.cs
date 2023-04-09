using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using Core.Test.Application.FakeData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterProject.Tests.Mocks.FakeDatas
{
    public static class UserFakeData
    {
        private static List<User> CreateFakeData()
        {
            byte[] passwordHash,
                passwordSalt;
            HashingHelper.CreatePasswordHash("123456", out passwordHash, out passwordSalt);
            int id = 0;
            var userList = new List<User>()
            {
                new()
                {
                    Id = ++id,
                    AuthenticatorType = AuthenticatorType.None,
                    Email = "halit@kodlama.io",
                    FirstName = "Halit",
                    LastName = "Kalaycı",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                },
                new()
                {
                    Id = ++id,
                    AuthenticatorType = AuthenticatorType.None,
                    Email = "engin@kodlama.io",
                    FirstName = "Engin",
                    LastName = "Demiroğ",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                },
            };
            return userList;
        }

        public static List<User> Data => CreateFakeData();
    }
}
