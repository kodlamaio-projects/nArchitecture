using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transmissions.Queries.GetList;

public class GetListTransmissionQuery : IRequest<GetListResponse<GetListTransmissionListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTransmissionQueryHandler
        : IRequestHandler<GetListTransmissionQuery, GetListResponse<GetListTransmissionListItemDto>>
    {
        private readonly ITransmissionRepository _transmissionRepository;
        private readonly IMapper _mapper;

        public GetListTransmissionQueryHandler(ITransmissionRepository transmissionRepository, IMapper mapper)
        {
            _transmissionRepository = transmissionRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListTransmissionListItemDto>> Handle(
            GetListTransmissionQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Transmission> transmissions = await _transmissionRepository.GetListAsync(
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize
            );
            var mappedTransmissionListModel = _mapper.Map<GetListResponse<GetListTransmissionListItemDto>>(transmissions);
            return mappedTransmissionListModel;
        }
    }
}
