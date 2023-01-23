using Application.Features.Colors.Commands.UpdateColor;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Test.Application.Constants;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Application.Features.Colors.Commands.UpdateColor.UpdateColorCommand;

namespace Application.Tests.Features.Colors.Commands.UpdateColor
{
    public class UpdateColorTests : ColorMockRepository
    {
        private readonly UpdateColorCommandValidator _validator;
        private readonly UpdateColorCommand _command;
        private readonly UpdateColorCommandHandler _handler;
        public UpdateColorTests(ColorFakeData fakeData, UpdateColorCommandValidator validator, UpdateColorCommand command) : base(fakeData)
        {
            _validator = validator;
            _command = command;
            _handler = new(MockRepository.Object, Mapper, BusinessRules);
        }

        [Fact]
        public void ColordNameEmptyShouldReturnError()
        {
            _command.Name = string.Empty;
            var result = _validator.Validate(_command).Errors.Where(x => x.PropertyName == "Name" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator).FirstOrDefault();
            Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
        }

        [Fact]
        public void ColorNameNotMatchMinLenghtRuleShouldReturnError()
        {
            _command.Name = "P";
            var result = _validator.Validate(_command).Errors.Where(x => x.PropertyName == "Name" && x.ErrorCode == ValidationErrorCodes.MinimumLengthValidator).FirstOrDefault();
            Assert.Equal(ValidationErrorCodes.MinimumLengthValidator, result?.ErrorCode);
        }

        [Fact]
        public async Task UpdateColorShouldSuccessfully()
        {
            _command.Id = 1;
            _command.Name = "Pink";
            var result = await _handler.Handle(_command, CancellationToken.None);
            Assert.Equal("Pink", result.Name);
        }

        [Fact]
        public async Task ColorIdNotExistsShouldReturnError()
        {
            _command.Id = 6;
            _command.Name = "Pink";
            await Assert.ThrowsAsync<BusinessException>(async () => await _handler.Handle(_command, CancellationToken.None));
        }
    }
}