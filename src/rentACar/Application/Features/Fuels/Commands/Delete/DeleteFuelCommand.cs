using Application.Features.Fuels.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Fuels.Constants.FuelsOperationClaims;

namespace Application.Features.Fuels.Commands.Delete;

public class DeleteFuelCommand : IRequest<DeletedFuelResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, FuelsOperationClaims.Delete };

    public class DeleteFuelCommandHandler : IRequestHandler<DeleteFuelCommand, DeletedFuelResponse>
    {
        private readonly IFuelRepository _fuelRepository;
        private readonly IMapper _mapper;

        public DeleteFuelCommandHandler(IFuelRepository fuelRepository, IMapper mapper)
        {
            _fuelRepository = fuelRepository;
            _mapper = mapper;
        }

        public async Task<DeletedFuelResponse> Handle(DeleteFuelCommand request, CancellationToken cancellationToken)
        {
            Fuel mappedFuel = _mapper.Map<Fuel>(request);
            Fuel deletedFuel = await _fuelRepository.DeleteAsync(mappedFuel);
            DeletedFuelResponse deletedFuelDto = _mapper.Map<DeletedFuelResponse>(deletedFuel);
            return deletedFuelDto;
        }
    }
}
