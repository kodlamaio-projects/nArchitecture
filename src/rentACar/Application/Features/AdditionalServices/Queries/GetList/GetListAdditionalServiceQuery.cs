using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.AdditionalServices.Queries.GetList;

public class GetListAdditionalServiceQuery : IRequest<GetListResponse<GetListAdditionalServiceListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListAdditionalServiceQueryHandler
        : IRequestHandler<GetListAdditionalServiceQuery, GetListResponse<GetListAdditionalServiceListItemDto>>
    {
        private readonly IAdditionalServiceRepository _additionalServiceRepository;
        private readonly IMapper _mapper;

        public GetListAdditionalServiceQueryHandler(IAdditionalServiceRepository additionalServiceRepository, IMapper mapper)
        {
            _additionalServiceRepository = additionalServiceRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListAdditionalServiceListItemDto>> Handle(
            GetListAdditionalServiceQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<AdditionalService> additionalServices = await _additionalServiceRepository.GetListAsync(
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize
            );
            var mappedAdditionalServiceListModel = _mapper.Map<GetListResponse<GetListAdditionalServiceListItemDto>>(additionalServices);
            return mappedAdditionalServiceListModel;
        }
    }
}
