using Application.Features.OperationClaims.Constants;
using Application.Features.OperationClaims.Mappers;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Mediator;
using static Application.Features.OperationClaims.Constants.OperationClaimsOperationClaims;

namespace Application.Features.OperationClaims.Commands.Update;

public class UpdateOperationClaimCommand : IRequest<UpdatedOperationClaimResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public UpdateOperationClaimCommand()
    {
        Name = string.Empty;
    }

    public UpdateOperationClaimCommand(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public string[] Roles => new[] { Admin, Write, OperationClaimsOperationClaims.Update };

    public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdatedOperationClaimResponse>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public UpdateOperationClaimCommandHandler(
            IOperationClaimRepository operationClaimRepository,
            OperationClaimBusinessRules operationClaimBusinessRules
        )
        {
            _operationClaimRepository = operationClaimRepository;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<UpdatedOperationClaimResponse> Handle(
            UpdateOperationClaimCommand request,
            CancellationToken cancellationToken
        )
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(
                predicate: oc => oc.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _operationClaimBusinessRules.OperationClaimShouldExistWhenSelected(operationClaim);
            await _operationClaimBusinessRules.OperationClaimNameShouldNotExistWhenUpdating(request.Id, request.Name);
            OperationClaimMapper.UpdateEntity(request, operationClaim!);

            OperationClaim updatedOperationClaim = await _operationClaimRepository.UpdateAsync(operationClaim!);

            UpdatedOperationClaimResponse response = OperationClaimMapper.ToUpdatedResponse(updatedOperationClaim);
            return response;
        }
    }
}
