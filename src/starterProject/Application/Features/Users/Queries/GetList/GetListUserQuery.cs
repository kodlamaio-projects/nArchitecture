using Application.Features.Users.Constants;
using Application.Features.Users.Mappers;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Mediator;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Users.Queries.GetList;

public class GetListUserQuery : IRequest<GetListResponse<GetListUserListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [UsersOperationClaims.Read];

    public GetListUserQuery()
    {
        PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
    }

    public GetListUserQuery(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }

    public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, GetListResponse<GetListUserListItemDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetListUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetListResponse<GetListUserListItemDto>> Handle(
            GetListUserQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<User> users = await _userRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                enableTracking: false,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListUserListItemDto> response = UserMapper.ToGetListResponse(users);
            return response;
        }
    }
}
