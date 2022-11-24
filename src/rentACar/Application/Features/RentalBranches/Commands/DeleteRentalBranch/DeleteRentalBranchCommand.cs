using Application.Features.RentalBranches.Dtos;
using Application.Features.RentalBranches.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.RentalBranches.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.RentalBranches.Commands.DeleteRentalBranch;

public class DeleteRentalBranchCommand : IRequest<DeletedRentalBranchDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, RentalBranchAdmin, RentalBranchWrite, RentalBranchDelete };

    public class DeleteRentalBranchCommandHandler : IRequestHandler<DeleteRentalBranchCommand, DeletedRentalBranchDto>
    {
        private readonly IRentalBranchRepository _rentalBranchRepository;
        private readonly IMapper _mapper;
        private readonly RentalBranchBusinessRules _rentalBranchBusinessRules;

        public DeleteRentalBranchCommandHandler(IRentalBranchRepository rentalBranchRepository, IMapper mapper,
                                                RentalBranchBusinessRules rentalBranchBusinessRules)
        {
            _rentalBranchRepository = rentalBranchRepository;
            _mapper = mapper;
            _rentalBranchBusinessRules = rentalBranchBusinessRules;
        }

        public async Task<DeletedRentalBranchDto> Handle(DeleteRentalBranchCommand request,
                                                         CancellationToken cancellationToken)
        {
            await _rentalBranchBusinessRules.RentalBranchIdShouldExistWhenSelected(request.Id);

            RentalBranch mappedRentalBranch = _mapper.Map<RentalBranch>(request);
            RentalBranch deletedRentalBranch = await _rentalBranchRepository.DeleteAsync(mappedRentalBranch);
            DeletedRentalBranchDto deletedRentalBranchDto = _mapper.Map<DeletedRentalBranchDto>(deletedRentalBranch);
            return deletedRentalBranchDto;
        }
    }
}