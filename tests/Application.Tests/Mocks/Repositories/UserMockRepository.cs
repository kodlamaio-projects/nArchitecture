using Application.Features.Users.Profiles;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Application.Tests.Mocks.FakeData;
using Core.Security.Entities;
using Core.Test.Application.Repositories;

namespace Application.Tests.Mocks.Repositories;

public class UserMockRepository : BaseMockRepository<IUserRepository, User, int, MappingProfiles, UserBusinessRules, UserFakeData>
{
    public UserMockRepository(UserFakeData fakeData)
        : base(fakeData) { }
}
