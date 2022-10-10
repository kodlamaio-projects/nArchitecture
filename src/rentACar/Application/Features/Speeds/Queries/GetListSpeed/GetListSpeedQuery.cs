using Application.Features.Speeds.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Speeds.Queries.GetListSpeed;

public class GetListSpeedQuery : IRequest<SpeedListModel>
{
    public PageRequest PageRequest { get; set; }

    public class GetListSpeedQueryHandler : IRequestHandler<GetListSpeedQuery, SpeedListModel>
    {
        private readonly ISpeedRepository _speedRepository;
        private readonly IMapper _mapper;

        public GetListSpeedQueryHandler(ISpeedRepository speedRepository, IMapper mapper)
        {
            _speedRepository = speedRepository;
            _mapper = mapper;
        }

        public async Task<SpeedListModel> Handle(GetListSpeedQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Speed> speeds = await _speedRepository.GetListAsync(index: request.PageRequest.Page,
                                                                          size: request.PageRequest.PageSize);
            SpeedListModel mappedSpeedListModel = _mapper.Map<SpeedListModel>(speeds);
            return mappedSpeedListModel;
        }
    }
}