﻿using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Domain.Security.Entities;
using Core.Security.Hashing;
using MediatR;

namespace Application.Features.Users.Commands.UpdateUserFromAuth;

public class UpdateUserFromAuthCommand : IRequest<UpdatedUserFromAuthDto>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string? NewPassword { get; set; }

    public class UpdateUserFromAuthCommandHandler : IRequestHandler<UpdateUserFromAuthCommand, UpdatedUserFromAuthDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly IAuthService _authService;

        public UpdateUserFromAuthCommandHandler(IUserRepository userRepository, IMapper mapper,
                                                UserBusinessRules userBusinessRules, IAuthService authService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
            _authService = authService;
        }

        public async Task<UpdatedUserFromAuthDto> Handle(UpdateUserFromAuthCommand request,
                                                         CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(u => u.Id == request.Id);
            await _userBusinessRules.UserShouldBeExist(user);
            await _userBusinessRules.UserPasswordShouldBeMatch(user, request.Password);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            if (request.NewPassword is not null && !string.IsNullOrWhiteSpace(request.NewPassword))
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            User updatedUser = await _userRepository.UpdateAsync(user);
            UpdatedUserFromAuthDto updatedUserFromAuthDto = _mapper.Map<UpdatedUserFromAuthDto>(updatedUser);
            updatedUserFromAuthDto.AccessToken = await _authService.CreateAccessToken(user);
            return updatedUserFromAuthDto;
        }
    }
}