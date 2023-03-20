using Application.Features.CarDamages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.CarDamages.Constants.CarDamagesOperationClaims;

namespace Application.Features.CarDamages.Commands.Create;

public class CreateCarDamageCommand : IRequest<CreatedCarDamageResponse>, ISecuredRequest
{
    public int CarId { get; set; }
    public string DamageDescription { get; set; }

    public string[] Roles => new[] { Admin, Write, Add };

    public class CreateCarDamageCommandHandler : IRequestHandler<CreateCarDamageCommand, CreatedCarDamageResponse>
    {
        private readonly ICarDamageRepository _carDamageRepository;
        private readonly IMapper _mapper;
        private readonly CarDamageBusinessRules _carDamageBusinessRules;

        public CreateCarDamageCommandHandler(
            ICarDamageRepository carDamageRepository,
            IMapper mapper,
            CarDamageBusinessRules carDamageBusinessRules
        )
        {
            _carDamageRepository = carDamageRepository;
            _mapper = mapper;
            _carDamageBusinessRules = carDamageBusinessRules;
        }

        public async Task<CreatedCarDamageResponse> Handle(CreateCarDamageCommand request, CancellationToken cancellationToken)
        {
            CarDamage mappedCarDamage = _mapper.Map<CarDamage>(request);
            CarDamage createdCarDamage = await _carDamageRepository.AddAsync(mappedCarDamage);
            CreatedCarDamageResponse createdCarDamageResponse = _mapper.Map<CreatedCarDamageResponse>(createdCarDamage);
            return createdCarDamageResponse;
        }
    }
}
