using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.RentalBranches.Queries.GetList;

public class GetListRentalBranchQuery : IRequest<GetListResponse<GetListRentalBranchListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListRentalBranchQueryHandler : IRequestHandler<GetListRentalBranchQuery, GetListResponse<GetListRentalBranchListItemDto>>
    {
        private readonly IRentalBranchRepository _rentalBranchRepository;
        private readonly IMapper _mapper;

        public GetListRentalBranchQueryHandler(IRentalBranchRepository rentalBranchRepository, IMapper mapper)
        {
            _rentalBranchRepository = rentalBranchRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListRentalBranchListItemDto>> Handle(GetListRentalBranchQuery request,
                                                        CancellationToken cancellationToken)
        {
            IPaginate<RentalBranch> rentalBranchs = await _rentalBranchRepository.GetListAsync(
                                                        index: request.PageRequest.Page,
                                                        size: request.PageRequest.PageSize);
            var mappedRentalBranchListModel = _mapper.Map<GetListResponse<GetListRentalBranchListItemDto>>(rentalBranchs);
            return mappedRentalBranchListModel;
        }
    }
}