using Application.Features.Users.Profiles;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Core.Security.Entities;
using Core.Test.Application.Repositories;
using StarterProject.Application.Tests.Mocks.FakeDatas;

namespace StarterProject.Application.Tests.Mocks.Repositories;

public class UserMockRepository : BaseMockRepository<IUserRepository, User, int, MappingProfiles, UserBusinessRules, UserFakeData>
{
    public UserMockRepository(UserFakeData fakeData)
        : base(fakeData) { }
}
