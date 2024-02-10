using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Users.Commands.Create;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Test.Application.Constants;
using FluentValidation.Results;
using StarterProject.Application.Tests.Mocks.FakeDatas;
using StarterProject.Application.Tests.Mocks.Repositories;
using Xunit;
using static Application.Features.Users.Commands.Create.CreateUserCommand;

namespace StarterProject.Application.Tests.Features.Users.Commands.Create;

public class CreateUserTests : UserMockRepository
{
    private readonly CreateUserCommandValidator _validator;
    private readonly CreateUserCommand _command;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserTests(UserFakeData fakeData, CreateUserCommandValidator validator, CreateUserCommand command)
        : base(fakeData)
    {
        _command = command;
        _validator = validator;
        _handler = new CreateUserCommandHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    [Fact]
    public void UserEmailEmptyShouldReturnError()
    {
        _command.Email = string.Empty;

        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.FirstOrDefault(x => x.PropertyName == "Email" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator);

        Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
    }

    [Fact]
    public void UserEmailNotMatchEmailRuleShouldReturnError()
    {
        _command.FirstName = "First";
        _command.LastName = "Last";
        _command.Email = "NotEmailFormat";
        _command.Password = "password";

        ValidationFailure? result = _validator
            .Validate(_command)
            .Errors.FirstOrDefault(x => x.PropertyName == "Email" && x.ErrorCode == ValidationErrorCodes.EmailValidator);

        Assert.Equal(ValidationErrorCodes.EmailValidator, result?.ErrorCode);
    }

    [Fact]
    public async Task CreateShouldSuccessfully()
    {
        _command.FirstName = "First";
        _command.LastName = "Last";
        _command.Email = "test@email.com";
        _command.Password = "password";

        CreatedUserResponse result = await _handler.Handle(_command, CancellationToken.None);

        Assert.Equal(expected: "test@email.com", result.Email);
    }

    [Fact]
    public async Task DuplicatedUserEmailShouldReturnError()
    {
        _command.FirstName = "First";
        _command.LastName = "Last";
        _command.Email = "example@kodlama.io";
        _command.Password = "password";

        async Task Action() => await _handler.Handle(_command, CancellationToken.None);

        await Assert.ThrowsAsync<BusinessException>(Action);
    }
}
