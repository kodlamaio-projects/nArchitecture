using Application.Features.Colors.Queries.GetListColor;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Application.Requests;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Application.Features.Colors.Queries.GetListColor.GetListColorQuery;

namespace Application.Tests.Features.Colors.Queries.GetListColor
{
    public class GetListColorTests : ColorMockRepository
    {
        private readonly GetListColorQuery _query;
        private readonly GetListColorQueryHandler _handler;
        public GetListColorTests(ColorFakeData fakeData, GetListColorQuery query) : base(fakeData)
        {
            _query = query;
            _handler = new(MockRepository.Object, Mapper);
        }

        [Fact]
        public async Task GetAllColorsShouldSuccessfuly()
        {
            _query.PageRequest = new PageRequest
            {
                Page = 0,
                PageSize = 3
            };
            var result = await _handler.Handle(_query, CancellationToken.None);
            Assert.Equal(2, result.Items.Count);
        }
    }
}