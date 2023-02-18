using Application.Features.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Models.Queries.GetListModelByDynamic;

public class GetListModelByDynamicQuery : IRequest<ModelListModel>
{
    public PageRequest PageRequest { get; set; }
    public Dynamic Dynamic { get; set; }

    public class GetListModelByDynamicQueryHandler : IRequestHandler<GetListModelByDynamicQuery, ModelListModel>
    {
        private readonly IMapper _mapper;
        private readonly IModelRepository _modelRepository;

        public GetListModelByDynamicQueryHandler(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<ModelListModel> Handle(GetListModelByDynamicQuery request,
            CancellationToken cancellationToken)
        {
            IPaginate<Model> models = await _modelRepository.GetListByDynamicAsync(
                request.Dynamic,
                include: c => c.Include(c => c.Brand)
                    .Include(c => c.Fuel)
                    .Include(c => c.Transmission),
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize, cancellationToken: cancellationToken);
            ModelListModel mappedModelListModel = _mapper.Map<ModelListModel>(models);
            return mappedModelListModel;
        }
    }
}