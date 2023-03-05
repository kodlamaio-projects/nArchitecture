using Application.Features.Cars.Constants;
using Application.Features.Cars.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.Cars.Constants.CarsOperationClaims;

namespace Application.Features.Cars.Commands.Maintain;

public class MaintainCarCommand : IRequest<MaintainedCarResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, CarsOperationClaims.Update };

    public class MaintainCarCommandHandler : IRequestHandler<MaintainCarCommand, MaintainedCarResponse>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly CarBusinessRules _carBusinessRules;

        public MaintainCarCommandHandler(ICarRepository carRepository, CarBusinessRules carBusinessRules, IMapper mapper)
        {
            _carRepository = carRepository;
            _carBusinessRules = carBusinessRules;
            _mapper = mapper;
        }

        public async Task<MaintainedCarResponse> Handle(MaintainCarCommand request, CancellationToken cancellationToken)
        {
            await _carBusinessRules.CarIdShouldExistWhenSelected(request.Id);
            await _carBusinessRules.CarCanNotBeMaintainWhenIsRented(request.Id);

            Car? updatedCar = await _carRepository.GetAsync(c => c.Id == request.Id);
            updatedCar.CarState = CarState.Maintenance;
            await _carRepository.UpdateAsync(updatedCar);
            MaintainedCarResponse? updatedCarDto = _mapper.Map<MaintainedCarResponse>(updatedCar);
            return updatedCarDto;
        }
    }
}
