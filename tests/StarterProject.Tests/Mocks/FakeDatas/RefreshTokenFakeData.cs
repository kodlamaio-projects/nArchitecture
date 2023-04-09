using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterProject.Tests.Mocks.FakeDatas
{
    public static class RefreshTokenFakeData
    {
        private static List<RefreshToken> CreateData()
        {
            List<RefreshToken> tokens = new List<RefreshToken>()
            {
                new() { UserId = 1, Token = "abc" }
            };
            return tokens;
        }

        public static List<RefreshToken> Data => CreateData();
    }
}
