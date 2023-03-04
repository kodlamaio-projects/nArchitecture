using Application.Features.Brands.Commands.Delete;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using System.Threading;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Xunit;
using static Application.Features.Brands.Commands.Delete.DeleteBrandCommand;

namespace Application.Tests.Features.Brands.Commands.DeleteBrand
{
    public class DeleteBrandTests : BrandMockRepository
    {
        private readonly DeleteBrandCommandHandler _handler;
        private readonly DeleteBrandCommand _command;

        public DeleteBrandTests(BrandFakeData fakeData, DeleteBrandCommand command) : base(fakeData)
        {
            _command = command;
            _handler = new(MockRepository.Object, Mapper, BusinessRules);
        }

        [Fact]
        public async Task DeleteShouldSuccessfully()
        {
            _command.Id = 1;
            var result = await _handler.Handle(_command, CancellationToken.None);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task BrandIdNotExistsShouldReturnError()
        {
            _command.Id = 6;
            await Assert.ThrowsAsync<BusinessException>(async () => await _handler.Handle(_command, CancellationToken.None));
        }
    }
}
