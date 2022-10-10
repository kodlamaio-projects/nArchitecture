using Application.Features.UserOperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Queries.GetListUserOperationClaim;

public class GetListUserOperationClaimQuery : IRequest<UserOperationClaimListModel>
{
    public PageRequest PageRequest { get; set; }

    public class
        GetListUserOperationClaimQueryHandler : IRequestHandler<GetListUserOperationClaimQuery,
            UserOperationClaimListModel>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;

        public GetListUserOperationClaimQueryHandler(IUserOperationClaimRepository userOperationClaimRepository,
                                                     IMapper mapper)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
        }

        public async Task<UserOperationClaimListModel> Handle(GetListUserOperationClaimQuery request,
                                                              CancellationToken cancellationToken)
        {
            IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(
                                                                    index: request.PageRequest.Page,
                                                                    size: request.PageRequest.PageSize);
            UserOperationClaimListModel mappedUserOperationClaimListModel =
                _mapper.Map<UserOperationClaimListModel>(userOperationClaims);
            return mappedUserOperationClaimListModel;
        }
    }
}