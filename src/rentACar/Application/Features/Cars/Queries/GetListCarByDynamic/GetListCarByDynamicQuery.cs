using Application.Features.Cars.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Cars.Queries.GetListCarByDynamic;

public class GetListCarByDynamicQuery : IRequest<CarListModel>
{
    public PageRequest PageRequest { get; set; }
    public Dynamic Dynamic { get; set; }

    public class GetListCarByDynamicQueryHandler : IRequestHandler<GetListCarByDynamicQuery, CarListModel>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public GetListCarByDynamicQueryHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<CarListModel> Handle(GetListCarByDynamicQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Car> cars = await _carRepository.GetListByDynamicAsync(
                request.Dynamic,
                include: c => c.Include(c => c.Model)
                    .Include(c => c.Model.Brand)
                    .Include(c => c.Color),
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize, cancellationToken: cancellationToken);
            CarListModel mappedCarListModel = _mapper.Map<CarListModel>(cars);
            return mappedCarListModel;
        }
    }
}