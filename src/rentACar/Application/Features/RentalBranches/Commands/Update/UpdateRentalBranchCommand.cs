using Application.Features.RentalBranches.Constants;
using Application.Features.RentalBranches.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.RentalBranches.Constants.RentalBranchesOperationClaims;

namespace Application.Features.RentalBranches.Commands.Update;

public class UpdateRentalBranchCommand : IRequest<UpdatedRentalBranchResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public City City { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, RentalBranchesOperationClaims.Update };

    public class UpdateRentalBranchCommandHandler : IRequestHandler<UpdateRentalBranchCommand, UpdatedRentalBranchResponse>
    {
        private readonly IRentalBranchRepository _rentalBranchRepository;
        private readonly IMapper _mapper;
        private readonly RentalBranchBusinessRules _rentalBranchBusinessRules;

        public UpdateRentalBranchCommandHandler(
            IRentalBranchRepository rentalBranchRepository,
            IMapper mapper,
            RentalBranchBusinessRules rentalBranchBusinessRules
        )
        {
            _rentalBranchRepository = rentalBranchRepository;
            _mapper = mapper;
            _rentalBranchBusinessRules = rentalBranchBusinessRules;
        }

        public async Task<UpdatedRentalBranchResponse> Handle(UpdateRentalBranchCommand request, CancellationToken cancellationToken)
        {
            RentalBranch mappedRentalBranch = _mapper.Map<RentalBranch>(request);
            RentalBranch updatedRentalBranch = await _rentalBranchRepository.UpdateAsync(mappedRentalBranch);
            UpdatedRentalBranchResponse updatedRentalBranchDto = _mapper.Map<UpdatedRentalBranchResponse>(updatedRentalBranch);
            return updatedRentalBranchDto;
        }
    }
}
