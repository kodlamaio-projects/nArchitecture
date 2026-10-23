using Application.Features.OperationClaims.Constants;
using Application.Features.OperationClaims.Mappers;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Mediator;

namespace Application.Features.OperationClaims.Queries.GetById;

public class GetByIdOperationClaimQuery : IRequest<GetByIdOperationClaimResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [OperationClaimsOperationClaims.Read];

    public class GetByIdOperationClaimQueryHandler : IRequestHandler<GetByIdOperationClaimQuery, GetByIdOperationClaimResponse>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public GetByIdOperationClaimQueryHandler(
            IOperationClaimRepository operationClaimRepository,
            OperationClaimBusinessRules operationClaimBusinessRules
        )
        {
            _operationClaimRepository = operationClaimRepository;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<GetByIdOperationClaimResponse> Handle(
            GetByIdOperationClaimQuery request,
            CancellationToken cancellationToken
        )
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(
                predicate: b => b.Id == request.Id,
                cancellationToken: cancellationToken,
                enableTracking: false
            );
            await _operationClaimBusinessRules.OperationClaimShouldExistWhenSelected(operationClaim);

            GetByIdOperationClaimResponse response = OperationClaimMapper.ToGetByIdResponse(operationClaim!);
            return response;
        }
    }
}
