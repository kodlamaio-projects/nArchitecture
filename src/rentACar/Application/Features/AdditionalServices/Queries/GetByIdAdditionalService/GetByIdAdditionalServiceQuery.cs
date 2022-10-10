using Application.Features.AdditionalServices.Dtos;
using Application.Features.AdditionalServices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.AdditionalServices.Queries.GetByIdAdditionalService;

public class GetByIdAdditionalServiceQuery : IRequest<AdditionalServiceDto>
{
    public int Id { get; set; }

    public class
        GetByIdAdditionalServiceQueryHandler : IRequestHandler<GetByIdAdditionalServiceQuery, AdditionalServiceDto>
    {
        private readonly IAdditionalServiceRepository _additionalServiceRepository;
        private readonly IMapper _mapper;
        private readonly AdditionalServiceBusinessRules _additionalServiceBusinessRules;

        public GetByIdAdditionalServiceQueryHandler(IAdditionalServiceRepository additionalServiceRepository,
                                                    IMapper mapper,
                                                    AdditionalServiceBusinessRules additionalServiceBusinessRules)
        {
            _additionalServiceRepository = additionalServiceRepository;
            _mapper = mapper;
            _additionalServiceBusinessRules = additionalServiceBusinessRules;
        }


        public async Task<AdditionalServiceDto> Handle(GetByIdAdditionalServiceQuery request,
                                                       CancellationToken cancellationToken)
        {
            await _additionalServiceBusinessRules.AdditionalServiceIdShouldExistWhenSelected(request.Id);

            AdditionalService? additionalService = await _additionalServiceRepository.GetAsync(b => b.Id == request.Id);
            AdditionalServiceDto additionalServiceDto = _mapper.Map<AdditionalServiceDto>(additionalService);
            return additionalServiceDto;
        }
    }
}