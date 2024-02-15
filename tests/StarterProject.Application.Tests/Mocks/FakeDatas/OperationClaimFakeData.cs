using Domain.Entities;
using NArchitecture.Core.Test.Application.FakeData;

namespace StarterProject.Application.Tests.Mocks.FakeDatas;

public class OperationClaimFakeData : BaseFakeData<OperationClaim, int>
{
    public override List<OperationClaim> CreateFakeData()
    {
        return
        [
            new() { Id = 1, Name = "Admin" },
            new() { Id = 2, Name = "Example.Create" },
            new() { Id = 3, Name = "Example.Delete" },
            new() { Id = 4, Name = "Example.Update" },
            new() { Id = 5, Name = "Moderator" },
        ];
    }
}
