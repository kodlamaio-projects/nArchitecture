using Application.Features.Users.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;

namespace Application.Features.Users.Rules;

public class UserBusinessRules
{
    private readonly IUserRepository _userRepository;

    public UserBusinessRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task UserIdShouldExistWhenSelected(int id)
    {
        User? result = await _userRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException(UserExceptionMessages.UserNotExistsMessage);
    }

    public Task UserShouldBeExist(User? user)
    {
        if (user is null) throw new BusinessException(UserExceptionMessages.UserNotExistsMessage);
        return Task.CompletedTask;
    }

    public Task UserPasswordShouldBeMatch(User user, string password)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            throw new BusinessException(UserExceptionMessages.PasswordDontMatchMessage);
        return Task.CompletedTask;
    }
}