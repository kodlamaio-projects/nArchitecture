using Application.Features.Cars.Dtos;
using Application.Features.Cars.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Cars.Queries.GetByIdCar;

public class GetByIdCarQuery : IRequest<CarDto>
{
    public int Id { get; set; }

    public class GetByIdCarQueryHandler : IRequestHandler<GetByIdCarQuery, CarDto>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly CarBusinessRules _carBusinessRules;

        public GetByIdCarQueryHandler(ICarRepository carRepository, CarBusinessRules carBusinessRules, IMapper mapper)
        {
            _carRepository = carRepository;
            _carBusinessRules = carBusinessRules;
            _mapper = mapper;
        }


        public async Task<CarDto> Handle(GetByIdCarQuery request, CancellationToken cancellationToken)
        {
            await _carBusinessRules.CarIdShouldExistWhenSelected(request.Id);

            Car? car = await _carRepository.GetAsync(c => c.Id == request.Id);
            CarDto carDto = _mapper.Map<CarDto>(car);
            return carDto;
        }
    }
}