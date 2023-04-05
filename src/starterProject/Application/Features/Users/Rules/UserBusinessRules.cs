using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using Core.Security.Hashing;

namespace Application.Features.Users.Rules;

public class UserBusinessRules : BaseBusinessRules
{
    private readonly IUserRepository _userRepository;

    public UserBusinessRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task UserShouldBeExistWhenSelected(User? user)
    {
        if (user == null)
            throw new BusinessException(AuthMessages.UserDontExists);
        return Task.CompletedTask;
    }

    public async Task UserIdShouldBeExistWhenSelected(int id)
    {
        User? user = await _userRepository.GetAsync(predicate: u => u.Id == id, enableTracking: false);
        await UserShouldBeExistWhenSelected(user);
    }

    public Task UserPasswordShouldBeMatch(User user, string password)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            throw new BusinessException(AuthMessages.PasswordDontMatch);
        return Task.CompletedTask;
    }

    public async Task UserEmailShouldNotExist(string email)
    {
        User? user = await _userRepository.GetAsync(predicate: u => u.Email == email, enableTracking: false);
        if (user != null)
            throw new BusinessException(AuthMessages.UserMailAlreadyExists);
    }
}
