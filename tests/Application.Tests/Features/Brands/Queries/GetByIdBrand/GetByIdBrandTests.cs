using Application.Features.Brands.Queries.GetByIdBrand;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Application.Features.Brands.Queries.GetByIdBrand.GetByIdBrandQuery;

namespace Application.Tests.Features.Brands.Queries.GetByIdBrand
{
    public class GetByIdBrandTests : BrandMockRepository
    {
        private readonly GetByIdBrandQuery _query;
        private readonly GetByIdBrandQueryHandler _handler;

        public GetByIdBrandTests(BrandFakeData fakeData, GetByIdBrandQuery query) : base(fakeData)
        {
            _query = query;
            _handler = new GetByIdBrandQueryHandler(MockRepository.Object, BusinessRules, Mapper);
        }

        [Fact]
        public async Task GetByIdBrandShouldSuccessfully()
        {
            _query.Id = 1;
            var result = await _handler.Handle(_query, CancellationToken.None);
            Assert.Equal("Mercedes", result.Name);
        }

        [Fact]
        public async Task BrandIdNotExistsShouldReturnError()
        {
            _query.Id = 6;
            await Assert.ThrowsAsync<BusinessException>(async () => await _handler.Handle(_query, CancellationToken.None));
        }

    }
}
