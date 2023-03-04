using Application.Features.Fuels.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Fuels.Constants.FuelsOperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Fuels.Commands.Update;

public class UpdateFuelCommand : IRequest<UpdatedFuelResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, FuelsOperationClaims.Admin, Write, FuelsOperationClaims.Delete };

    public class UpdateFuelCommandHandler : IRequestHandler<UpdateFuelCommand, UpdatedFuelResponse>
    {
        private IFuelRepository _fuelRepository { get; }
        private IMapper _mapper { get; }

        public UpdateFuelCommandHandler(IFuelRepository fuelRepository, IMapper mapper)
        {
            _fuelRepository = fuelRepository;
            _mapper = mapper;
        }

        public async Task<UpdatedFuelResponse> Handle(UpdateFuelCommand request, CancellationToken cancellationToken)
        {
            Fuel mappedFuel = _mapper.Map<Fuel>(request);
            Fuel updatedFuel = await _fuelRepository.UpdateAsync(mappedFuel);
            UpdatedFuelResponse updatedFuelDto = _mapper.Map<UpdatedFuelResponse>(updatedFuel);
            return updatedFuelDto;
        }
    }
}