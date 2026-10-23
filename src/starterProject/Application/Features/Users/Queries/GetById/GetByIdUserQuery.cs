using Application.Features.Users.Constants;
using Application.Features.Users.Mappers;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Mediator;

namespace Application.Features.Users.Queries.GetById;

public class GetByIdUserQuery : IRequest<GetByIdUserResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [UsersOperationClaims.Read];

    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, GetByIdUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserBusinessRules _userBusinessRules;

        public GetByIdUserQueryHandler(IUserRepository userRepository, UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<GetByIdUserResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(
                predicate: b => b.Id.Equals(request.Id),
                enableTracking: false,
                cancellationToken: cancellationToken
            );
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

            GetByIdUserResponse response = UserMapper.ToGetByIdResponse(user!);
            return response;
        }
    }
}
