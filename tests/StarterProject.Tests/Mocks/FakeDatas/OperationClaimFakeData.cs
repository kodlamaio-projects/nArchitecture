using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterProject.Tests.Mocks.FakeDatas
{
    public static class OperationClaimFakeData
    {
        private static List<OperationClaim> CreateFakeData()
        {
            return new List<OperationClaim>()
            {
                new() { Id = 1, Name = "Admin" },
                new() { Id = 2, Name = "Example.Create" },
                new() { Id = 3, Name = "Example.Delete" },
                new() { Id = 4, Name = "Example.Update" },
                new() { Id = 5, Name = "Moderator" },
            };
        }

        public static List<OperationClaim> Data => CreateFakeData();
    }
}
