using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using static Application.Features.Users.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommand : IRequest<DeletedUserDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, UserAdmin, UserWrite, UserDelete };

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeletedUserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;

        public DeleteUserCommandHandler(IUserRepository userRepository, IMapper mapper,
                                        UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<DeletedUserDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.UserIdShouldExistWhenSelected(request.Id);

            User mappedUser = _mapper.Map<User>(request);
            User deletedUser = await _userRepository.DeleteAsync(mappedUser);
            DeletedUserDto deletedUserDto = _mapper.Map<DeletedUserDto>(deletedUser);
            return deletedUserDto;
        }
    }
}