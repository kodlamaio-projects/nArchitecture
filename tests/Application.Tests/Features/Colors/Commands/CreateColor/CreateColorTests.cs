using Application.Features.Colors.Commands.CreateColor;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Test.Application.Constants;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Application.Features.Colors.Commands.CreateColor.CreateColorCommand;

namespace Application.Tests.Features.Colors.Commands.CreateColor
{
    public class CreateColorTests : ColorMockRepository
    {
        private readonly CreateColorCommandValidator _validator;
        private readonly CreateColorCommand _command;
        private readonly CreateColorCommandHandler _handler;
        public CreateColorTests(ColorFakeData fakeData, CreateColorCommandValidator validator, CreateColorCommand command) : base(fakeData)
        {
            _validator = validator;
            _command = command;
            _handler = new CreateColorCommandHandler(MockRepository.Object, Mapper, BusinessRules);
        }

        [Fact]
        public void ColorNameEmptyShouldReturnError()
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
        public async Task CreateColorShouldSuccessfully()
        {
            _command.Name = "Pink";
            var result = await _handler.Handle(_command, CancellationToken.None);
            Assert.Equal("Pink", result.Name);
        }

        [Fact]
        public async Task DuplicatedColorNameShouldReturnError()
        {
            _command.Name = "Blue";
            await Assert.ThrowsAsync<BusinessException>(async () => await _handler.Handle(_command, CancellationToken.None));
        }
    }
}