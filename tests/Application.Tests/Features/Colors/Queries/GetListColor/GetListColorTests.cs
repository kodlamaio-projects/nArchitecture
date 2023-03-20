using System.Threading;
using System.Threading.Tasks;
using Application.Features.Colors.Queries.GetList;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Xunit;
using static Application.Features.Colors.Queries.GetList.GetListColorQuery;

namespace Application.Tests.Features.Colors.Queries.GetListColor;

public class GetListColorTests : ColorMockRepository
{
    private readonly GetListColorQuery _query;
    private readonly GetListColorQueryHandler _handler;

    public GetListColorTests(ColorFakeData fakeData, GetListColorQuery query)
        : base(fakeData)
    {
        _query = query;
        _handler = new GetListColorQueryHandler(MockRepository.Object, Mapper);
    }

    [Fact]
    public async Task GetAllColorsShouldSuccessfuly()
    {
        _query.PageRequest = new PageRequest { Page = 0, PageSize = 3 };
        GetListResponse<GetListColorListItemDto> result = await _handler.Handle(_query, CancellationToken.None);
        Assert.Equal(expected: 2, result.Items.Count);
    }
}
