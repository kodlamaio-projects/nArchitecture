using Application.Features.RentalBranches.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.RentalBranches.Queries.GetById;

public class GetByIdRentalBranchQuery : IRequest<GetByIdRentalBranchResponse>
{
    public int Id { get; set; }

    public class GetByIdRentalBranchQueryHandler : IRequestHandler<GetByIdRentalBranchQuery, GetByIdRentalBranchResponse>
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


        public async Task<GetByIdRentalBranchResponse> Handle(GetByIdRentalBranchQuery request, CancellationToken cancellationToken)
        {
            await _rentalBranchBusinessRules.RentalBranchIdShouldExistWhenSelected(request.Id);

            RentalBranch? rentalBranch = await _rentalBranchRepository.GetAsync(b => b.Id == request.Id);
            GetByIdRentalBranchResponse rentalBranchDto = _mapper.Map<GetByIdRentalBranchResponse>(rentalBranch);
            return rentalBranchDto;
        }
    }
}