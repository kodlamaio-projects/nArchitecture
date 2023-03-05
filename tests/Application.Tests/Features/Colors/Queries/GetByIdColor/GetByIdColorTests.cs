using System.Threading;
using System.Threading.Tasks;
using Application.Features.Colors.Queries.GetById;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Xunit;
using static Application.Features.Colors.Queries.GetById.GetByIdColorQuery;

namespace Application.Tests.Features.Colors.Queries.GetByIdColor;

public class GetByIdColorTests : ColorMockRepository
{
    private readonly GetByIdColorQuery _query;
    private readonly GetByIdColorQueryHandler _handler;

    public GetByIdColorTests(ColorFakeData fakeData, GetByIdColorQuery query)
        : base(fakeData)
    {
        _query = query;
        _handler = new GetByIdColorQueryHandler(MockRepository.Object, BusinessRules, Mapper);
    }

    [Fact]
    public async Task GetByIdColorShouldSuccessfully()
    {
        _query.Id = 1;
        GetByIdColorResponse result = await _handler.Handle(_query, CancellationToken.None);
        Assert.Equal(expected: "Red", result.Name);
    }

    [Fact]
    public async Task ColorIdNotExistsShouldReturnError()
    {
        _query.Id = 6;
        await Assert.ThrowsAsync<BusinessException>(async () => await _handler.Handle(_query, CancellationToken.None));
    }
}
