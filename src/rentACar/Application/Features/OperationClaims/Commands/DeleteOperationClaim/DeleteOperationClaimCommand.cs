using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using static Application.Features.OperationClaims.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.OperationClaims.Commands.DeleteOperationClaim;

public class DeleteOperationClaimCommand : IRequest<DeletedOperationClaimDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, OperationClaimDelete };

    public class
        DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeletedOperationClaimDto>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public DeleteOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper,
                                                  OperationClaimBusinessRules operationClaimBusinessRules)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<DeletedOperationClaimDto> Handle(DeleteOperationClaimCommand request,
                                                           CancellationToken cancellationToken)
        {
            await _operationClaimBusinessRules.OperationClaimIdShouldExistWhenSelected(request.Id);

            OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(request);
            OperationClaim deletedOperationClaim = await _operationClaimRepository.DeleteAsync(mappedOperationClaim);
            DeletedOperationClaimDto deletedOperationClaimDto =
                _mapper.Map<DeletedOperationClaimDto>(deletedOperationClaim);
            return deletedOperationClaimDto;
        }
    }
}