using Application.Features.Cars.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.Cars.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;


namespace Application.Features.Cars.Commands.UpdateCar;

public class UpdateCarCommand : IRequest<UpdatedCarDto>, ISecuredRequest
{
    public int Id { get; set; }
    public int ColorId { get; set; }
    public int ModelId { get; set; }
    public int RentalBranchId { get; set; }
    public CarState CarState { get; set; }
    public int Kilometer { get; set; }
    public short ModelYear { get; set; }
    public string Plate { get; set; }
    public short MinFindeksCreditRate { get; set; }

    public string[] Roles => new[] { Admin, CarAdmin, CarWrite, CarUpdate };

    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, UpdatedCarDto>
    {
        private ICarRepository _carRepository { get; }
        private IMapper _mapper { get; }

        public UpdateCarCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<UpdatedCarDto> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            Car mappedCar = _mapper.Map<Car>(request);
            Car updatedCar = await _carRepository.UpdateAsync(mappedCar);
            UpdatedCarDto updatedCarDto = _mapper.Map<UpdatedCarDto>(updatedCar);
            return updatedCarDto;
        }
    }
}