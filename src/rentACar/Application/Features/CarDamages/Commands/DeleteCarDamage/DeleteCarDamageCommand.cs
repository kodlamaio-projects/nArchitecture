using Application.Features.CarDamages.Dtos;
using Application.Features.CarDamages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.CarDamages.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.CarDamages.Commands.DeleteCarDamage;

public class DeleteCarDamageCommand : IRequest<DeletedCarDamageDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, CarDamageDelete };

    public class DeleteCarDamageCommandHandler : IRequestHandler<DeleteCarDamageCommand, DeletedCarDamageDto>
    {
        private readonly ICarDamageRepository _carDamageRepository;
        private readonly IMapper _mapper;
        private readonly CarDamageBusinessRules _carDamageBusinessRules;

        public DeleteCarDamageCommandHandler(ICarDamageRepository carDamageRepository, IMapper mapper,
                                             CarDamageBusinessRules carDamageBusinessRules)
        {
            _carDamageRepository = carDamageRepository;
            _mapper = mapper;
            _carDamageBusinessRules = carDamageBusinessRules;
        }

        public async Task<DeletedCarDamageDto> Handle(DeleteCarDamageCommand request,
                                                      CancellationToken cancellationToken)
        {
            await _carDamageBusinessRules.CarDamageIdShouldExistWhenSelected(request.Id);

            CarDamage mappedCarDamage = _mapper.Map<CarDamage>(request);
            CarDamage deletedCarDamage = await _carDamageRepository.DeleteAsync(mappedCarDamage);
            DeletedCarDamageDto deletedCarDamageDto = _mapper.Map<DeletedCarDamageDto>(deletedCarDamage);
            return deletedCarDamageDto;
        }
    }
}