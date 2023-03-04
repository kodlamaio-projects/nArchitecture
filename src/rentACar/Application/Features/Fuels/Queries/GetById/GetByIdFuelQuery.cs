using Application.Features.Fuels.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Fuels.Queries.GetById;

public class GetByIdFuelQuery : IRequest<GetByIdFuelResponse>
{
    public int Id { get; set; }

    public class GetByIdFuelQueryHandler : IRequestHandler<GetByIdFuelQuery, GetByIdFuelResponse>
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

        public async Task<GetByIdFuelResponse> Handle(GetByIdFuelQuery request, CancellationToken cancellationToken)
        {
            await _fuelBusinessRules.FuelIdShouldExistWhenSelected(request.Id);

            Fuel? fuel = await _fuelRepository.GetAsync(f => f.Id == request.Id);
            GetByIdFuelResponse fuelDto = _mapper.Map<GetByIdFuelResponse>(fuel);
            return fuelDto;
        }
    }
}