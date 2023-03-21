using Application.Features.UserOperationClaims.Constants;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using static Application.Features.UserOperationClaims.Constants.UserOperationClaimsOperationClaims;

namespace Application.Features.UserOperationClaims.Commands.Delete;

public class DeleteUserOperationClaimCommand : IRequest<DeletedUserOperationClaimResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, UserOperationClaimsOperationClaims.Delete };

    public class DeleteUserOperationClaimCommandHandler
        : IRequestHandler<DeleteUserOperationClaimCommand, DeletedUserOperationClaimResponse>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public DeleteUserOperationClaimCommandHandler(
            IUserOperationClaimRepository userOperationClaimRepository,
            IMapper mapper,
            UserOperationClaimBusinessRules userOperationClaimBusinessRules
        )
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }

        public async Task<DeletedUserOperationClaimResponse> Handle(
            DeleteUserOperationClaimCommand request,
            CancellationToken cancellationToken
        )
        {
            await _userOperationClaimBusinessRules.UserOperationClaimIdShouldExistWhenSelected(request.Id);

            UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);
            UserOperationClaim deletedUserOperationClaim = await _userOperationClaimRepository.DeleteAsync(mappedUserOperationClaim);
            DeletedUserOperationClaimResponse deletedUserOperationClaimDto = _mapper.Map<DeletedUserOperationClaimResponse>(
                deletedUserOperationClaim
            );
            return deletedUserOperationClaimDto;
        }
    }
}
