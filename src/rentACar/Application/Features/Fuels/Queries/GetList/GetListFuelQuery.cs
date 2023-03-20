using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Fuels.Queries.GetList;

public class GetListFuelQuery : IRequest<GetListResponse<GetListFuelListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListFuelQueryHandler : IRequestHandler<GetListFuelQuery, GetListResponse<GetListFuelListItemDto>>
    {
        private readonly IFuelRepository _fuelRepository;
        private readonly IMapper _mapper;

        public GetListFuelQueryHandler(IFuelRepository fuelRepository, IMapper mapper)
        {
            _fuelRepository = fuelRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListFuelListItemDto>> Handle(GetListFuelQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Fuel> fuels = await _fuelRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
            var mappedFuelListModel = _mapper.Map<GetListResponse<GetListFuelListItemDto>>(fuels);
            return mappedFuelListModel;
        }
    }
}
