using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Models.Queries.GetList;

public class GetListModelQuery : IRequest<GetListResponse<GetListModelListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public bool BypassCache { get; set; }
    public string CacheKey => $"GetListModels({PageRequest.Page},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetModels";
    public TimeSpan? SlidingExpiration { get; set; }

    public class GetListModelQueryHandler : IRequestHandler<GetListModelQuery, GetListResponse<GetListModelListItemDto>>
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;

        public GetListModelQueryHandler(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListModelListItemDto>> Handle(GetListModelQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Model> models = await _modelRepository.GetListAsync(
                include: c => c.Include(c => c.Brand).Include(c => c.Fuel).Include(c => c.Transmission),
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize
            );
            var mappedModelListModel = _mapper.Map<GetListResponse<GetListModelListItemDto>>(models);
            return mappedModelListModel;
        }
    }
}
