using Application.Features.OperationClaims.Constants;
using Application.Features.OperationClaims.Mappers;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Mediator;
using static Application.Features.OperationClaims.Constants.OperationClaimsOperationClaims;

namespace Application.Features.OperationClaims.Commands.Create;

public class CreateOperationClaimCommand : IRequest<CreatedOperationClaimResponse>, ISecuredRequest
{
    public string Name { get; set; }

    public CreateOperationClaimCommand()
    {
        Name = string.Empty;
    }

    public CreateOperationClaimCommand(string name)
    {
        Name = name;
    }

    public string[] Roles => new[] { Admin, Write, OperationClaimsOperationClaims.Create };

    public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreatedOperationClaimResponse>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public CreateOperationClaimCommandHandler(
            IOperationClaimRepository operationClaimRepository,
            OperationClaimBusinessRules operationClaimBusinessRules
        )
        {
            _operationClaimRepository = operationClaimRepository;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<CreatedOperationClaimResponse> Handle(
            CreateOperationClaimCommand request,
            CancellationToken cancellationToken
        )
        {
            await _operationClaimBusinessRules.OperationClaimNameShouldNotExistWhenCreating(request.Name);
            OperationClaim mappedOperationClaim = OperationClaimMapper.ToEntity(request);

            OperationClaim createdOperationClaim = await _operationClaimRepository.AddAsync(mappedOperationClaim);

            CreatedOperationClaimResponse response = OperationClaimMapper.ToCreatedResponse(createdOperationClaim);
            return response;
        }
    }
}
