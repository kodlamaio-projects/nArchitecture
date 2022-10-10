using Application.Features.CarDamages.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CarDamages.Queries.GetListCarDamage;

public class GetListCarDamageQuery : IRequest<CarDamageListModel>
{
    public PageRequest PageRequest { get; set; }

    public class GetListCarDamageQueryHandler : IRequestHandler<GetListCarDamageQuery, CarDamageListModel>
    {
        private readonly ICarDamageRepository _carDamageRepository;
        private readonly IMapper _mapper;

        public GetListCarDamageQueryHandler(ICarDamageRepository carDamageRepository, IMapper mapper)
        {
            _carDamageRepository = carDamageRepository;
            _mapper = mapper;
        }

        public async Task<CarDamageListModel> Handle(GetListCarDamageQuery request, CancellationToken cancellationToken)
        {
            IPaginate<CarDamage> carDamages = await _carDamageRepository.GetListAsync(
                                                  include: c => c.Include(c => c.Car)
                                                                 .Include(c => c.Car.Model)
                                                                 .Include(c => c.Car.Model.Brand),
                                                  index: request.PageRequest.Page,
                                                  size: request.PageRequest.PageSize);
            CarDamageListModel mappedCarDamageListModel = _mapper.Map<CarDamageListModel>(carDamages);
            return mappedCarDamageListModel;
        }
    }
}