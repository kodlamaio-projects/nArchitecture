using Application.Features.Transmissions.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transmissions.Queries.GetListTransmission;

public class GetListTransmissionQuery : IRequest<TransmissionListModel>
{
    public PageRequest PageRequest { get; set; }

    public class GetListTransmissionQueryHandler : IRequestHandler<GetListTransmissionQuery, TransmissionListModel>
    {
        private readonly ITransmissionRepository _transmissionRepository;
        private readonly IMapper _mapper;

        public GetListTransmissionQueryHandler(ITransmissionRepository transmissionRepository, IMapper mapper)
        {
            _transmissionRepository = transmissionRepository;
            _mapper = mapper;
        }

        public async Task<TransmissionListModel> Handle(GetListTransmissionQuery request,
                                                        CancellationToken cancellationToken)
        {
            IPaginate<Transmission> transmissions = await _transmissionRepository.GetListAsync(
                                                        index: request.PageRequest.Page,
                                                        size: request.PageRequest.PageSize);
            TransmissionListModel mappedTransmissionListModel = _mapper.Map<TransmissionListModel>(transmissions);
            return mappedTransmissionListModel;
        }
    }
}