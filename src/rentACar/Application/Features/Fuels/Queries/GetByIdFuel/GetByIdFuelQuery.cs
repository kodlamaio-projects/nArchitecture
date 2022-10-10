using Application.Features.Fuels.Dtos;
using Application.Features.Fuels.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Fuels.Queries.GetByIdFuel;

public class GetByIdFuelQuery : IRequest<FuelDto>
{
    public int Id { get; set; }

    public class GetByIdFuelQueryHandler : IRequestHandler<GetByIdFuelQuery, FuelDto>
    {
        private readonly IFuelRepository _fuelRepository;
        private readonly IMapper _mapper;
        private readonly FuelBusinessRules _fuelBusinessRules;

        public GetByIdFuelQueryHandler(IFuelRepository fuelRepository, FuelBusinessRules fuelBusinessRules,
                                       IMapper mapper)
        {
            _fuelRepository = fuelRepository;
            _fuelBusinessRules = fuelBusinessRules;
            _mapper = mapper;
        }

        public async Task<FuelDto> Handle(GetByIdFuelQuery request, CancellationToken cancellationToken)
        {
            await _fuelBusinessRules.FuelIdShouldExistWhenSelected(request.Id);

            Fuel? fuel = await _fuelRepository.GetAsync(f => f.Id == request.Id);
            FuelDto fuelDto = _mapper.Map<FuelDto>(fuel);
            return fuelDto;
        }
    }
}