using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Cars.Queries.GetList;

public class GetListCarQuery : IRequest<GetListResponse<GetListCarListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListCarQueryHandler : IRequestHandler<GetListCarQuery, GetListResponse<GetListCarListItemDto>>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public GetListCarQueryHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCarListItemDto>> Handle(GetListCarQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Car> cars = await _carRepository.GetListAsync(c => c.CarState != CarState.Maintenance,
                                                                    include:
                                                                    c => c.Include(c => c.Model)
                                                                          .Include(c => c.Model.Brand)
                                                                          .Include(c => c.Color),
                                                                    index: request.PageRequest.Page,
                                                                    size: request.PageRequest.PageSize);
            var mappedCarListModel = _mapper.Map<GetListResponse<GetListCarListItemDto>>(cars);
            return mappedCarListModel;
        }
    }
}