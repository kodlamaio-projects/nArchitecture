using Application.Features.Brands.Commands.UpdateBrand;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.Test.Application.Constants;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Xunit;
using static Application.Features.Brands.Commands.UpdateBrand.UpdateBrandCommand;

namespace Application.Tests.Features.Brands.Commands.UpdateBrand
{
    public class UpdateBrandTests : BrandMockRepository
    {
        private readonly UpdateBrandCommandValidator _validator;
        private readonly UpdateBrandCommand _command;
        private readonly UpdateBrandCommandHandler _handler;

        public UpdateBrandTests(BrandFakeData fakeData, UpdateBrandCommandValidator validator, UpdateBrandCommand command) : base(fakeData)
        {
            _validator = validator;
            _command = command;
            _handler = new(MockRepository.Object, Mapper, BusinessRules);
        }

        [Fact]
        public void BrandNameEmptyShouldReturnError()
        {
            _command.Name = string.Empty;
            var result = _validator.Validate(_command).Errors.Where(x => x.PropertyName == "Name" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator).FirstOrDefault();
            Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
        }

        [Fact]
        public void BrandNameNotMatchMinLenghtRuleShouldReturnError()
        {
            _command.Name = "S";
            var result = _validator.Validate(_command).Errors.Where(x => x.PropertyName == "Name" && x.ErrorCode == ValidationErrorCodes.MinimumLengthValidator).FirstOrDefault();
            Assert.Equal(ValidationErrorCodes.MinimumLengthValidator, result?.ErrorCode);
        }

        [Fact]
        public async Task UpdateShouldSuccessfully()
        {
            _command.Id = 1;
            _command.Name = "Opel";
            var result = await _handler.Handle(_command, CancellationToken.None);
            Assert.Equal("Opel", result.Name);
        }

        [Fact]
        public async Task BrandIdNotExistsShouldReturnError()
        {
            _command.Id = 6;
            _command.Name = "Opel";
            await Assert.ThrowsAsync<BusinessException>(async () => await _handler.Handle(_command, CancellationToken.None));
        }
    }
}
