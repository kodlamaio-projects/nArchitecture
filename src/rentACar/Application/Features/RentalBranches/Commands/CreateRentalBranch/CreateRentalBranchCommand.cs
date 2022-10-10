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

namespace Application.Features.RentalBranches.Commands.CreateRentalBranch;

public class CreateRentalBranchCommand : IRequest<CreatedRentalBranchDto>, ISecuredRequest
{
    public City City { get; set; }

    public string[] Roles => new[] { Admin, RentalBranchAdd };

    public class CreateRentalBranchCommandHandler : IRequestHandler<CreateRentalBranchCommand, CreatedRentalBranchDto>
    {
        private readonly IRentalBranchRepository _rentalBranchRepository;
        private readonly IMapper _mapper;
        private readonly RentalBranchBusinessRules _rentalBranchBusinessRules;

        public CreateRentalBranchCommandHandler(IRentalBranchRepository rentalBranchRepository, IMapper mapper,
                                                RentalBranchBusinessRules rentalBranchBusinessRules)
        {
            _rentalBranchRepository = rentalBranchRepository;
            _mapper = mapper;
            _rentalBranchBusinessRules = rentalBranchBusinessRules;
        }

        public async Task<CreatedRentalBranchDto> Handle(CreateRentalBranchCommand request,
                                                         CancellationToken cancellationToken)
        {
            RentalBranch mappedRentalBranch = _mapper.Map<RentalBranch>(request);
            RentalBranch createdRentalBranch = await _rentalBranchRepository.AddAsync(mappedRentalBranch);
            CreatedRentalBranchDto createdRentalBranchDto = _mapper.Map<CreatedRentalBranchDto>(createdRentalBranch);
            return createdRentalBranchDto;
        }
    }
}