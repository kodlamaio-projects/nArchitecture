using Application.Features.Cars.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Cars.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Cars.Commands.DeleteCar;

public class DeleteCarCommand : IRequest<DeletedCarDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, CarDelete };

    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, DeletedCarDto>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public DeleteCarCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<DeletedCarDto> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            Car mappedCar = _mapper.Map<Car>(request);
            Car deletedCar = await _carRepository.DeleteAsync(mappedCar);
            DeletedCarDto deletedCarDto = _mapper.Map<DeletedCarDto>(deletedCar);
            return deletedCarDto;
        }
    }
}