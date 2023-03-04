using Application.Features.Fuels.Constants;
using Application.Features.Fuels.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Fuels.Constants.FuelsOperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Fuels.Commands.Create;

public class CreateFuelCommand : IRequest<CreatedFuelResponse>, ISecuredRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, FuelsOperationClaims.Admin, Write, Add };

    public class CreateFuelCommandHandler : IRequestHandler<CreateFuelCommand, CreatedFuelResponse>
    {
        private readonly IFuelRepository _fuelRepository;
        private readonly IMapper _mapper;
        private readonly FuelBusinessRules _fuelBusinessRules;

        public CreateFuelCommandHandler(IFuelRepository fuelRepository, IMapper mapper,
                                        FuelBusinessRules fuelBusinessRules)
        {
            _fuelRepository = fuelRepository;
            _mapper = mapper;
            _fuelBusinessRules = fuelBusinessRules;
        }

        public async Task<CreatedFuelResponse> Handle(CreateFuelCommand request, CancellationToken cancellationToken)
        {
            await _fuelBusinessRules.FuelNameCanNotBeDuplicatedWhenInserted(request.Name);

            Fuel mappedFuel = _mapper.Map<Fuel>(request);
            Fuel createdFuel = await _fuelRepository.AddAsync(mappedFuel);
            CreatedFuelResponse createdFuelDto = _mapper.Map<CreatedFuelResponse>(createdFuel);
            return createdFuelDto;
        }
    }
}