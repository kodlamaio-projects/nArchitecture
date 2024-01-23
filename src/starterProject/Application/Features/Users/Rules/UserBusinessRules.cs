using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Application.Services.TranslateService;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using Core.Security.Hashing;

namespace Application.Features.Users.Rules;

public class UserBusinessRules : BaseBusinessRules
{
    private readonly IUserRepository _userRepository;
    private readonly ITranslateService _translateService;

    public UserBusinessRules(
        IUserRepository userRepository,
        ITranslateService translateService
        )
    {
        _userRepository = userRepository;
        _translateService = translateService;
    }
    public override async Task ThrowBusinessException(string message)
    {
        string translatedMessage = await _translateService.TranslateAsync(message);
        await base.ThrowBusinessException(translatedMessage);
    }

    public async Task UserShouldBeExistsWhenSelected(User? user)
    {
        if (user == null)
            await ThrowBusinessException(AuthMessages.UserDontExists);
    }

    public async Task UserIdShouldBeExistsWhenSelected(int id)
    {
        bool doesExist = await _userRepository.AnyAsync(predicate: u => u.Id == id, enableTracking: false);
        if (doesExist)
            await ThrowBusinessException(AuthMessages.UserDontExists);
    }

    public async Task UserPasswordShouldBeMatched(User user, string password)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            await ThrowBusinessException(AuthMessages.PasswordDontMatch);
    }

    public async Task UserEmailShouldNotExistsWhenInsert(string email)
    {
        bool doesExists = await _userRepository.AnyAsync(predicate: u => u.Email == email, enableTracking: false);
        if (doesExists)
            await ThrowBusinessException(AuthMessages.UserMailAlreadyExists);
    }

    public async Task UserEmailShouldNotExistsWhenUpdate(int id, string email)
    {
        bool doesExists = await _userRepository.AnyAsync(predicate: u => u.Id != id && u.Email == email, enableTracking: false);
        if (doesExists)
            await ThrowBusinessException(AuthMessages.UserMailAlreadyExists);
    }
}
