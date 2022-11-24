using Application.Features.Fuels.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Fuels.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Fuels.Commands.UpdateFuel;

public class UpdateFuelCommand : IRequest<UpdatedFuelDto>, ISecuredRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, FuelAdmin, FuelWrite, FuelDelete };

    public class UpdateFuelCommandHandler : IRequestHandler<UpdateFuelCommand, UpdatedFuelDto>
    {
        private IFuelRepository _fuelRepository { get; }
        private IMapper _mapper { get; }

        public UpdateFuelCommandHandler(IFuelRepository fuelRepository, IMapper mapper)
        {
            _fuelRepository = fuelRepository;
            _mapper = mapper;
        }

        public async Task<UpdatedFuelDto> Handle(UpdateFuelCommand request, CancellationToken cancellationToken)
        {
            Fuel mappedFuel = _mapper.Map<Fuel>(request);
            Fuel updatedFuel = await _fuelRepository.UpdateAsync(mappedFuel);
            UpdatedFuelDto updatedFuelDto = _mapper.Map<UpdatedFuelDto>(updatedFuel);
            return updatedFuelDto;
        }
    }
}