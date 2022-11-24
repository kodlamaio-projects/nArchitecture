using Application.Features.Fuels.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Fuels.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Fuels.Commands.DeleteFuel;

public class DeleteFuelCommand : IRequest<DeletedFuelDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, FuelAdmin, FuelWrite, FuelDelete };

    public class DeleteFuelCommandHandler : IRequestHandler<DeleteFuelCommand, DeletedFuelDto>
    {
        private readonly IFuelRepository _fuelRepository;
        private readonly IMapper _mapper;

        public DeleteFuelCommandHandler(IFuelRepository fuelRepository, IMapper mapper)
        {
            _fuelRepository = fuelRepository;
            _mapper = mapper;
        }

        public async Task<DeletedFuelDto> Handle(DeleteFuelCommand request, CancellationToken cancellationToken)
        {
            Fuel mappedFuel = _mapper.Map<Fuel>(request);
            Fuel deletedFuel = await _fuelRepository.DeleteAsync(mappedFuel);
            DeletedFuelDto deletedFuelDto = _mapper.Map<DeletedFuelDto>(deletedFuel);
            return deletedFuelDto;
        }
    }
}