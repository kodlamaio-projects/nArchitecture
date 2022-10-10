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
        if (result == null) throw new BusinessException("User not exists.");
    }

    public Task UserShouldBeExist(User? user)
    {
        if (user is null) throw new BusinessException("User not exists.");
        return Task.CompletedTask;
    }

    public Task UserPasswordShouldBeMatch(User user, string password)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            throw new BusinessException("Password don't match.");
        return Task.CompletedTask;
    }
}