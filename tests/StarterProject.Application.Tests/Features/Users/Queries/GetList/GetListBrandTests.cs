using Application.Features.Users.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using StarterProject.Application.Tests.Mocks.FakeDatas;
using StarterProject.Application.Tests.Mocks.Repositories;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Application.Features.Users.Queries.GetList.GetListUserQuery;

namespace StarterProject.Application.Tests.Features.Users.Queries.GetList;

public class GetListUserTests : UserMockRepository
{
    private readonly GetListUserQuery _query;
    private readonly GetListUserQueryHandler _handler;

    public GetListUserTests(UserFakeData fakeData, GetListUserQuery query)
        : base(fakeData)
    {
        _query = query;
        _handler = new GetListUserQueryHandler(MockRepository.Object, Mapper);
    }

    [Fact]
    public async Task GetAllUsersShouldSuccessfuly()
    {
        _query.PageRequest = new PageRequest { PageIndex = 0, PageSize = 3 };

        GetListResponse<GetListUserListItemDto> result = await _handler.Handle(_query, CancellationToken.None);

        Assert.Equal(expected: 3, result.Items.Count);
    }
}
