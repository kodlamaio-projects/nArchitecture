using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Test.Application.Repositories;
using StarterProject.Application.Tests.Mocks.FakeDatas;

namespace StarterProject.Application.Tests.Mocks.Repositories;

public class UserMockRepository : BaseMockRepository<IUserRepository, User, Guid, UserBusinessRules, UserFakeData>
{
    public UserMockRepository(UserFakeData fakeData)
        : base(fakeData) { }
}
