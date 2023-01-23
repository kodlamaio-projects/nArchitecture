using Application.Features.Colors.Profiles;
using Application.Features.Colors.Rules;
using Application.Services.Repositories;
using Application.Tests.Mocks.FakeData;
using Core.Test.Application.Repositories;
using Domain.Entities;

namespace Application.Tests.Mocks.Repositories
{
    public class ColorMockRepository : BaseMockRepository<IColorRepository, Color, MappingProfiles, ColorBusinessRules, ColorFakeData>
    {
        public ColorMockRepository(ColorFakeData fakeData) : base(fakeData) { }
    }
}
