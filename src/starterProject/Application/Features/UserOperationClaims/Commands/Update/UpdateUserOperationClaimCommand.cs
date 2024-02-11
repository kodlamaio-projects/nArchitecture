using Application.Features.UserOperationClaims.Constants;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Security.Entities;
using static Application.Features.UserOperationClaims.Constants.UserOperationClaimsOperationClaims;

namespace Application.Features.UserOperationClaims.Commands.Update;

public class UpdateUserOperationClaimCommand : IRequest<UpdatedUserOperationClaimResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }

    public string[] Roles => new[] { Admin, Write, UserOperationClaimsOperationClaims.Update };

    public class UpdateUserOperationClaimCommandHandler
        : IRequestHandler<UpdateUserOperationClaimCommand, UpdatedUserOperationClaimResponse>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public UpdateUserOperationClaimCommandHandler(
            IUserOperationClaimRepository userOperationClaimRepository,
            IMapper mapper,
            UserOperationClaimBusinessRules userOperationClaimBusinessRules
        )
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }

        public async Task<UpdatedUserOperationClaimResponse> Handle(
            UpdateUserOperationClaimCommand request,
            CancellationToken cancellationToken
        )
        {
            UserOperationClaim<int, int>? userOperationClaim = await _userOperationClaimRepository.GetAsync(
                predicate: uoc => uoc.Id == request.Id,
                enableTracking: false,
                cancellationToken: cancellationToken
            );
            await _userOperationClaimBusinessRules.UserOperationClaimShouldExistWhenSelected(userOperationClaim);
            await _userOperationClaimBusinessRules.UserShouldNotHasOperationClaimAlreadyWhenUpdated(
                request.Id,
                request.UserId,
                request.OperationClaimId
            );
            UserOperationClaim<int, int> mappedUserOperationClaim = _mapper.Map(request, destination: userOperationClaim!);

            UserOperationClaim<int, int> updatedUserOperationClaim = await _userOperationClaimRepository.UpdateAsync(
                mappedUserOperationClaim
            );

            UpdatedUserOperationClaimResponse updatedUserOperationClaimDto = _mapper.Map<UpdatedUserOperationClaimResponse>(
                updatedUserOperationClaim
            );
            return updatedUserOperationClaimDto;
        }
    }
}
