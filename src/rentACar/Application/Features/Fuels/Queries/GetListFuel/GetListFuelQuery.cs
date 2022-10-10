using Application.Features.Fuels.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Fuels.Queries.GetListFuel;

public class GetListFuelQuery : IRequest<FuelListModel>
{
    public PageRequest PageRequest { get; set; }

    public class GetListFuelQueryHandler : IRequestHandler<GetListFuelQuery, FuelListModel>
    {
        private readonly IFuelRepository _fuelRepository;
        private readonly IMapper _mapper;

        public GetListFuelQueryHandler(IFuelRepository fuelRepository, IMapper mapper)
        {
            _fuelRepository = fuelRepository;
            _mapper = mapper;
        }

        public async Task<FuelListModel> Handle(GetListFuelQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Fuel> fuels = await _fuelRepository.GetListAsync(index: request.PageRequest.Page,
                                                                       size: request.PageRequest.PageSize);
            FuelListModel mappedFuelListModel = _mapper.Map<FuelListModel>(fuels);
            return mappedFuelListModel;
        }
    }
}