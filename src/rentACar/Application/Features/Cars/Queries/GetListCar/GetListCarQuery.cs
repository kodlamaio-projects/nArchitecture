using Application.Features.Cars.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Cars.Queries.GetListCar;

public class GetListCarQuery : IRequest<CarListModel>
{
    public PageRequest PageRequest { get; set; }

    public class GetListCarQueryHandler : IRequestHandler<GetListCarQuery, CarListModel>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public GetListCarQueryHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<CarListModel> Handle(GetListCarQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Car> cars = await _carRepository.GetListAsync(c => c.CarState != CarState.Maintenance,
                                                                    include:
                                                                    c => c.Include(c => c.Model)
                                                                          .Include(c => c.Model.Brand)
                                                                          .Include(c => c.Color),
                                                                    index: request.PageRequest.Page,
                                                                    size: request.PageRequest.PageSize);
            CarListModel mappedCarListModel = _mapper.Map<CarListModel>(cars);
            return mappedCarListModel;
        }
    }
}