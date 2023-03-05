using Application.Features.RentalBranches.Constants;
using Application.Features.RentalBranches.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.RentalBranches.Constants.RentalBranchesOperationClaims;

namespace Application.Features.RentalBranches.Commands.Delete;

public class DeleteRentalBranchCommand : IRequest<DeletedRentalBranchResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, RentalBranchesOperationClaims.Delete };

    public class DeleteRentalBranchCommandHandler : IRequestHandler<DeleteRentalBranchCommand, DeletedRentalBranchResponse>
    {
        private readonly IRentalBranchRepository _rentalBranchRepository;
        private readonly IMapper _mapper;
        private readonly RentalBranchBusinessRules _rentalBranchBusinessRules;

        public DeleteRentalBranchCommandHandler(
            IRentalBranchRepository rentalBranchRepository,
            IMapper mapper,
            RentalBranchBusinessRules rentalBranchBusinessRules
        )
        {
            _rentalBranchRepository = rentalBranchRepository;
            _mapper = mapper;
            _rentalBranchBusinessRules = rentalBranchBusinessRules;
        }

        public async Task<DeletedRentalBranchResponse> Handle(DeleteRentalBranchCommand request, CancellationToken cancellationToken)
        {
            await _rentalBranchBusinessRules.RentalBranchIdShouldExistWhenSelected(request.Id);

            RentalBranch mappedRentalBranch = _mapper.Map<RentalBranch>(request);
            RentalBranch deletedRentalBranch = await _rentalBranchRepository.DeleteAsync(mappedRentalBranch);
            DeletedRentalBranchResponse deletedRentalBranchDto = _mapper.Map<DeletedRentalBranchResponse>(deletedRentalBranch);
            return deletedRentalBranchDto;
        }
    }
}
