using Application.Features.Users.Mappers;
using Application.Features.Users.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Mediator;
using NArchitecture.Core.Security.Hashing;

namespace Application.Features.Users.Commands.UpdateFromAuth;

public class UpdateUserFromAuthCommand : IRequest<UpdatedUserFromAuthResponse>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string? NewPassword { get; set; }

    public UpdateUserFromAuthCommand()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Password = string.Empty;
    }

    public UpdateUserFromAuthCommand(Guid id, string firstName, string lastName, string password)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
    }

    public class UpdateUserFromAuthCommandHandler : IRequestHandler<UpdateUserFromAuthCommand, UpdatedUserFromAuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly IAuthService _authService;

        public UpdateUserFromAuthCommandHandler(
            IUserRepository userRepository,
            UserBusinessRules userBusinessRules,
            IAuthService authService
        )
        {
            _userRepository = userRepository;
            _userBusinessRules = userBusinessRules;
            _authService = authService;
        }

        public async Task<UpdatedUserFromAuthResponse> Handle(
            UpdateUserFromAuthCommand request,
            CancellationToken cancellationToken
        )
        {
            User? user = await _userRepository.GetAsync(
                predicate: u => u.Id.Equals(request.Id),
                cancellationToken: cancellationToken
            );
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);
            await _userBusinessRules.UserPasswordShouldBeMatched(user: user!, request.Password);
            await _userBusinessRules.UserEmailShouldNotExistsWhenUpdate(user!.Id, user.Email);

            if (request.NewPassword != null && !string.IsNullOrWhiteSpace(request.NewPassword))
            {
                HashingHelper.CreatePasswordHash(
                    request.Password,
                    passwordHash: out byte[] passwordHash,
                    passwordSalt: out byte[] passwordSalt
                );
                user!.PasswordHash = passwordHash;
                user!.PasswordSalt = passwordSalt;
            }

            User updatedUser = await _userRepository.UpdateAsync(user!);

            UpdatedUserFromAuthResponse response = UserMapper.ToUpdatedFromAuthResponse(updatedUser);
            response.AccessToken = await _authService.CreateAccessToken(user!);
            return response;
        }
    }
}
