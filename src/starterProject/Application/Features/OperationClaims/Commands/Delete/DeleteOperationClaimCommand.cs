using Application.Features.OperationClaims.Constants;
using Application.Features.OperationClaims.Mappers;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Mediator;
using static Application.Features.OperationClaims.Constants.OperationClaimsOperationClaims;

namespace Application.Features.OperationClaims.Commands.Delete;

public class DeleteOperationClaimCommand : IRequest<DeletedOperationClaimResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, OperationClaimsOperationClaims.Delete };

    public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeletedOperationClaimResponse>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public DeleteOperationClaimCommandHandler(
            IOperationClaimRepository operationClaimRepository,
            OperationClaimBusinessRules operationClaimBusinessRules
        )
        {
            _operationClaimRepository = operationClaimRepository;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<DeletedOperationClaimResponse> Handle(
            DeleteOperationClaimCommand request,
            CancellationToken cancellationToken
        )
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(
                predicate: oc => oc.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _operationClaimBusinessRules.OperationClaimShouldExistWhenSelected(operationClaim);

            await _operationClaimRepository.DeleteAsync(entity: operationClaim!);

            DeletedOperationClaimResponse response = OperationClaimMapper.ToDeletedResponse(operationClaim!);
            return response;
        }
    }
}
