using Application.Features.Cars.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.Cars.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Cars.Commands.CreateCar;

public class CreateCarCommand : IRequest<CreatedCarDto>, ISecuredRequest
{
    public int ColorId { get; set; }
    public int ModelId { get; set; }
    public int RentalBranchId { get; set; }
    public CarState CarState { get; set; }
    public int Kilometer { get; set; }
    public short ModelYear { get; set; }
    public string Plate { get; set; }
    public short MinFindeksCreditRate { get; set; }

    public string[] Roles => new[] { Admin, CarAdmin, CarWrite, CarAdd };


    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, CreatedCarDto>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CreateCarCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<CreatedCarDto> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            Car mappedCar = _mapper.Map<Car>(request);
            Car createdCar = await _carRepository.AddAsync(mappedCar);
            CreatedCarDto createdCarDto = _mapper.Map<CreatedCarDto>(createdCar);
            return createdCarDto;
        }
    }
}