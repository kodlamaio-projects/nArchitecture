using Application.Features.UserOperationClaims.Constants;
using Application.Features.UserOperationClaims.Mappers;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Mediator;
using static Application.Features.UserOperationClaims.Constants.UserOperationClaimsOperationClaims;

namespace Application.Features.UserOperationClaims.Commands.Delete;

public class DeleteUserOperationClaimCommand : IRequest<DeletedUserOperationClaimResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, UserOperationClaimsOperationClaims.Delete };

    public class DeleteUserOperationClaimCommandHandler
        : IRequestHandler<DeleteUserOperationClaimCommand, DeletedUserOperationClaimResponse>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public DeleteUserOperationClaimCommandHandler(
            IUserOperationClaimRepository userOperationClaimRepository,
            UserOperationClaimBusinessRules userOperationClaimBusinessRules
        )
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }

        public async Task<DeletedUserOperationClaimResponse> Handle(
            DeleteUserOperationClaimCommand request,
            CancellationToken cancellationToken
        )
        {
            UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(
                predicate: uoc => uoc.Id.Equals(request.Id),
                cancellationToken: cancellationToken
            );
            await _userOperationClaimBusinessRules.UserOperationClaimShouldExistWhenSelected(userOperationClaim);

            await _userOperationClaimRepository.DeleteAsync(userOperationClaim!);

            DeletedUserOperationClaimResponse response = UserOperationClaimMapper.ToDeletedResponse(userOperationClaim!);
            return response;
        }
    }
}
