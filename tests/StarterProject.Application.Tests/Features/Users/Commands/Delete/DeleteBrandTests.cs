using System.Threading;
using System.Threading.Tasks;
using Application.Features.Users.Commands.Delete;
using Core.CrossCuttingConcerns.Exceptions.Types;
using StarterProject.Application.Tests.Mocks.FakeDatas;
using StarterProject.Application.Tests.Mocks.Repositories;
using Xunit;
using static Application.Features.Users.Commands.Delete.DeleteUserCommand;

namespace StarterProject.Application.Tests.Features.Users.Commands.Delete;

public class DeleteUserTests : UserMockRepository
{
    private readonly DeleteUserCommandHandler _handler;
    private readonly DeleteUserCommand _command;

    public DeleteUserTests(UserFakeData fakeData, DeleteUserCommand command)
        : base(fakeData)
    {
        _command = command;
        _handler = new DeleteUserCommandHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    [Fact]
    public async Task DeleteShouldSuccessfully()
    {
        _command.Id = 1;
        DeletedUserResponse result = await _handler.Handle(_command, CancellationToken.None);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UserIdNotExistsShouldReturnError()
    {
        _command.Id = 6;

        async Task Action() => await _handler.Handle(_command, CancellationToken.None);

        await Assert.ThrowsAsync<BusinessException>(Action);
    }
}
