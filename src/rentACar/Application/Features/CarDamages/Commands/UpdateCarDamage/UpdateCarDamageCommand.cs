using Application.Features.CarDamages.Dtos;
using Application.Features.CarDamages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.CarDamages.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.CarDamages.Commands.UpdateCarDamage;

public class UpdateCarDamageCommand : IRequest<UpdatedCarDamageDto>, ISecuredRequest
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public string DamageDescription { get; set; }
    public bool IsFixed { get; set; }

    public string[] Roles => new[] { Admin, CarDamageAdmin, CarDamageWrite, CarDamageUpdate };

    public class UpdateCarDamageCommandHandler : IRequestHandler<UpdateCarDamageCommand, UpdatedCarDamageDto>
    {
        private readonly ICarDamageRepository _carDamageRepository;
        private readonly IMapper _mapper;
        private readonly CarDamageBusinessRules _carDamageBusinessRules;

        public UpdateCarDamageCommandHandler(ICarDamageRepository carDamageRepository, IMapper mapper,
                                             CarDamageBusinessRules carDamageBusinessRules)
        {
            _carDamageRepository = carDamageRepository;
            _mapper = mapper;
            _carDamageBusinessRules = carDamageBusinessRules;
        }

        public async Task<UpdatedCarDamageDto> Handle(UpdateCarDamageCommand request,
                                                      CancellationToken cancellationToken)
        {
            CarDamage mappedCarDamage = _mapper.Map<CarDamage>(request);
            CarDamage updatedCarDamage = await _carDamageRepository.UpdateAsync(mappedCarDamage);
            UpdatedCarDamageDto updatedCarDamageDto = _mapper.Map<UpdatedCarDamageDto>(updatedCarDamage);
            return updatedCarDamageDto;
        }
    }
}