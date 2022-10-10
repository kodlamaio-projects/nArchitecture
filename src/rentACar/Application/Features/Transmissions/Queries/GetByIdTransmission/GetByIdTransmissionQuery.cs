using Application.Features.Transmissions.Dtos;
using Application.Features.Transmissions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Transmissions.Queries.GetByIdTransmission;

public class GetByIdTransmissionQuery : IRequest<TransmissionDto>
{
    public int Id { get; set; }

    public class GetByIdTransmissionQueryHandler : IRequestHandler<GetByIdTransmissionQuery, TransmissionDto>
    {
        private readonly ITransmissionRepository _transmissionRepository;
        private readonly IMapper _mapper;
        private readonly TransmissionBusinessRules _transmissionBusinessRules;

        public GetByIdTransmissionQueryHandler(ITransmissionRepository transmissionRepository,
                                               TransmissionBusinessRules transmissionBusinessRules, IMapper mapper)
        {
            _transmissionRepository = transmissionRepository;
            _transmissionBusinessRules = transmissionBusinessRules;
            _mapper = mapper;
        }

        public async Task<TransmissionDto> Handle(GetByIdTransmissionQuery request, CancellationToken cancellationToken)
        {
            await _transmissionBusinessRules.TransmissionIdShouldExistWhenSelected(request.Id);

            Transmission? transmission = await _transmissionRepository.GetAsync(t => t.Id == request.Id);
            TransmissionDto transmissionDto = _mapper.Map<TransmissionDto>(transmission);
            return transmissionDto;
        }
    }
}