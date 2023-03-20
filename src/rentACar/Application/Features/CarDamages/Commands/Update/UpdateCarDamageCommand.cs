using Application.Features.CarDamages.Constants;
using Application.Features.CarDamages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.CarDamages.Constants.CarDamagesOperationClaims;

namespace Application.Features.CarDamages.Commands.Update;

public class UpdateCarDamageCommand : IRequest<UpdatedCarDamageResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public string DamageDescription { get; set; }
    public bool IsFixed { get; set; }

    public string[] Roles => new[] { Admin, Write, CarDamagesOperationClaims.Update };

    public class UpdateCarDamageCommandHandler : IRequestHandler<UpdateCarDamageCommand, UpdatedCarDamageResponse>
    {
        private readonly ICarDamageRepository _carDamageRepository;
        private readonly IMapper _mapper;
        private readonly CarDamageBusinessRules _carDamageBusinessRules;

        public UpdateCarDamageCommandHandler(
            ICarDamageRepository carDamageRepository,
            IMapper mapper,
            CarDamageBusinessRules carDamageBusinessRules
        )
        {
            _carDamageRepository = carDamageRepository;
            _mapper = mapper;
            _carDamageBusinessRules = carDamageBusinessRules;
        }

        public async Task<UpdatedCarDamageResponse> Handle(UpdateCarDamageCommand request, CancellationToken cancellationToken)
        {
            CarDamage mappedCarDamage = _mapper.Map<CarDamage>(request);
            CarDamage updatedCarDamage = await _carDamageRepository.UpdateAsync(mappedCarDamage);
            UpdatedCarDamageResponse updatedCarDamageDto = _mapper.Map<UpdatedCarDamageResponse>(updatedCarDamage);
            return updatedCarDamageDto;
        }
    }
}
