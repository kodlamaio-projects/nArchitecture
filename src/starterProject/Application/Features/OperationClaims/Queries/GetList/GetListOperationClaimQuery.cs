using Application.Features.OperationClaims.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetList;

public class GetListOperationClaimQuery : IRequest<GetListResponse<GetListOperationClaimListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [OperationClaimsOperationClaims.Read];

    public GetListOperationClaimQuery()
    {
        PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
    }

    public GetListOperationClaimQuery(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }

    public class GetListOperationClaimQueryHandler
        : IRequestHandler<GetListOperationClaimQuery, GetListResponse<GetListOperationClaimListItemDto>>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;

        public GetListOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListOperationClaimListItemDto>> Handle(
            GetListOperationClaimQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<OperationClaim<int, int>> operationClaims = await _operationClaimRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListOperationClaimListItemDto> response = _mapper.Map<GetListResponse<GetListOperationClaimListItemDto>>(
                operationClaims
            );
            return response;
        }
    }
}
