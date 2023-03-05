using Application.Features.Brands.Profiles;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using Application.Tests.Mocks.FakeData;
using Core.Test.Application.Repositories;
using Domain.Entities;

namespace Application.Tests.Mocks.Repositories;

public class BrandMockRepository : BaseMockRepository<IBrandRepository, Brand, MappingProfiles, BrandBusinessRules, BrandFakeData>
{
    public BrandMockRepository(BrandFakeData fakeData)
        : base(fakeData) { }
}
