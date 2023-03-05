using Application.Features.Cars.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Cars.Constants.CarsOperationClaims;

namespace Application.Features.Cars.Commands.Delete;

public class DeleteCarCommand : IRequest<DeletedCarResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, CarsOperationClaims.Delete };

    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, DeletedCarResponse>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public DeleteCarCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<DeletedCarResponse> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            Car mappedCar = _mapper.Map<Car>(request);
            Car deletedCar = await _carRepository.DeleteAsync(mappedCar);
            DeletedCarResponse deletedCarDto = _mapper.Map<DeletedCarResponse>(deletedCar);
            return deletedCarDto;
        }
    }
}
