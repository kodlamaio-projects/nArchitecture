using Application.Features.Brands.Commands.Create;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Test.Application.Constants;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Application.Features.Brands.Commands.Create.CreateBrandCommand;

namespace Application.Tests.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandTests : BrandMockRepository
    {
        private readonly CreateBrandCommandValidator _validator;
        private readonly CreateBrandCommand _command;
        private readonly CreateBrandCommandHandler _handler;

        public CreateBrandTests(BrandFakeData fakeData, CreateBrandCommandValidator validator, CreateBrandCommand command) : base(fakeData)
        {
            _command = command;
            _validator = validator;
            _handler = new CreateBrandCommandHandler(MockRepository.Object, Mapper, BusinessRules);
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
        public async Task CreateShouldSuccessfully()
        {
            _command.Name = "Audi";
            var result = await _handler.Handle(_command, CancellationToken.None);
            Assert.Equal("Audi", result.Name);
        }

        [Fact]
        public async Task DuplicatedBrandNameShouldReturnError()
        {
            _command.Name = "BMW";
            await Assert.ThrowsAsync<BusinessException>(async () => await _handler.Handle(_command, CancellationToken.None));
        }
    }
}
