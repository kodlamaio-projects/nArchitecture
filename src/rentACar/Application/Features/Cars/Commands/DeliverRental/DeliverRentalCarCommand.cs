using Application.Features.Cars.Constants;
using Application.Features.Cars.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.Cars.Constants.CarsOperationClaims;

namespace Application.Features.Cars.Commands.DeliverRental;

public class DeliverRentalCarCommand : IRequest<DeliveredCarResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, CarsOperationClaims.Update };

    public class DeliverRentalCarCommandHandler : IRequestHandler<DeliverRentalCarCommand, DeliveredCarResponse>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly CarBusinessRules _carBusinessRules;

        public DeliverRentalCarCommandHandler(ICarRepository carRepository, CarBusinessRules carBusinessRules, IMapper mapper)
        {
            _carRepository = carRepository;
            _carBusinessRules = carBusinessRules;
            _mapper = mapper;
        }

        public async Task<DeliveredCarResponse> Handle(DeliverRentalCarCommand request, CancellationToken cancellationToken)
        {
            await _carBusinessRules.CarCanNotBeRentWhenIsInMaintenance(request.Id);
            await _carBusinessRules.CarCanNotBeMaintainWhenIsRented(request.Id);

            Car? updatedCar = await _carRepository.GetAsync(c => c.Id == request.Id);
            updatedCar.CarState = CarState.Rented;
            await _carRepository.UpdateAsync(updatedCar);
            DeliveredCarResponse? updatedCarDto = _mapper.Map<DeliveredCarResponse>(updatedCar);
            return updatedCarDto;
        }
    }
}
