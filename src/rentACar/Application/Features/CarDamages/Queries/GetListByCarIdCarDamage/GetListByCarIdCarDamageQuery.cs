using Application.Features.CarDamages.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CarDamages.Queries.GetListByCarIdCarDamage;

public class GetListByCarIdCarDamageQuery : IRequest<CarDamageListModel>
{
    public int CarId { get; set; }
    public PageRequest PageRequest { get; set; }

    public class GetListByCarIdCarDamageQueryHandler : IRequestHandler<GetListByCarIdCarDamageQuery, CarDamageListModel>
    {
        private readonly ICarDamageRepository _carDamageRepository;
        private readonly IMapper _mapper;

        public GetListByCarIdCarDamageQueryHandler(ICarDamageRepository carDamageRepository, IMapper mapper)
        {
            _carDamageRepository = carDamageRepository;
            _mapper = mapper;
        }

        public async Task<CarDamageListModel> Handle(GetListByCarIdCarDamageQuery request,
                                                     CancellationToken cancellationToken)
        {
            IPaginate<CarDamage> carDamages = await _carDamageRepository.GetListAsync(
                                                  c => c.CarId == request.CarId,
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