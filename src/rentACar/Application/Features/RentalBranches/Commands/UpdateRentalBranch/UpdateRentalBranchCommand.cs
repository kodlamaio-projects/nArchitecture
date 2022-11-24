using Application.Features.RentalBranches.Dtos;
using Application.Features.RentalBranches.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.RentalBranches.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.RentalBranches.Commands.UpdateRentalBranch;

public class UpdateRentalBranchCommand : IRequest<UpdatedRentalBranchDto>, ISecuredRequest
{
    public int Id { get; set; }
    public City City { get; set; }

    public string[] Roles => new[] { Admin, RentalBranchAdmin, RentalBranchWrite, RentalBranchUpdate };

    public class UpdateRentalBranchCommandHandler : IRequestHandler<UpdateRentalBranchCommand, UpdatedRentalBranchDto>
    {
        private readonly IRentalBranchRepository _rentalBranchRepository;
        private readonly IMapper _mapper;
        private readonly RentalBranchBusinessRules _rentalBranchBusinessRules;

        public UpdateRentalBranchCommandHandler(IRentalBranchRepository rentalBranchRepository, IMapper mapper,
                                                RentalBranchBusinessRules rentalBranchBusinessRules)
        {
            _rentalBranchRepository = rentalBranchRepository;
            _mapper = mapper;
            _rentalBranchBusinessRules = rentalBranchBusinessRules;
        }

        public async Task<UpdatedRentalBranchDto> Handle(UpdateRentalBranchCommand request,
                                                         CancellationToken cancellationToken)
        {
            RentalBranch mappedRentalBranch = _mapper.Map<RentalBranch>(request);
            RentalBranch updatedRentalBranch = await _rentalBranchRepository.UpdateAsync(mappedRentalBranch);
            UpdatedRentalBranchDto updatedRentalBranchDto = _mapper.Map<UpdatedRentalBranchDto>(updatedRentalBranch);
            return updatedRentalBranchDto;
        }
    }
}