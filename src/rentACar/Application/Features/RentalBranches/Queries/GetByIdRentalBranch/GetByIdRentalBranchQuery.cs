using Application.Features.RentalBranches.Dtos;
using Application.Features.RentalBranches.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.RentalBranches.Queries.GetByIdRentalBranch;

public class GetByIdRentalBranchQuery : IRequest<RentalBranchDto>
{
    public int Id { get; set; }

    public class GetByIdRentalBranchQueryHandler : IRequestHandler<GetByIdRentalBranchQuery, RentalBranchDto>
    {
        private readonly IRentalBranchRepository _rentalBranchRepository;
        private readonly IMapper _mapper;
        private readonly RentalBranchBusinessRules _rentalBranchBusinessRules;

        public GetByIdRentalBranchQueryHandler(IRentalBranchRepository rentalBranchRepository, IMapper mapper,
                                               RentalBranchBusinessRules rentalBranchBusinessRules)
        {
            _rentalBranchRepository = rentalBranchRepository;
            _mapper = mapper;
            _rentalBranchBusinessRules = rentalBranchBusinessRules;
        }


        public async Task<RentalBranchDto> Handle(GetByIdRentalBranchQuery request, CancellationToken cancellationToken)
        {
            await _rentalBranchBusinessRules.RentalBranchIdShouldExistWhenSelected(request.Id);

            RentalBranch? rentalBranch = await _rentalBranchRepository.GetAsync(b => b.Id == request.Id);
            RentalBranchDto rentalBranchDto = _mapper.Map<RentalBranchDto>(rentalBranch);
            return rentalBranchDto;
        }
    }
}