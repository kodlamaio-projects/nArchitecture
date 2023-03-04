using Application.Features.AdditionalServices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.AdditionalServices.Queries.GetById;

public class GetByIdAdditionalServiceQuery : IRequest<GetByIdAdditionalServiceResponse>
{
    public int Id { get; set; }

    public class
        GetByIdAdditionalServiceQueryHandler : IRequestHandler<GetByIdAdditionalServiceQuery, GetByIdAdditionalServiceResponse>
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


        public async Task<GetByIdAdditionalServiceResponse> Handle(GetByIdAdditionalServiceQuery request,
                                                       CancellationToken cancellationToken)
        {
            await _additionalServiceBusinessRules.AdditionalServiceIdShouldExistWhenSelected(request.Id);

            AdditionalService? additionalService = await _additionalServiceRepository.GetAsync(b => b.Id == request.Id);
            GetByIdAdditionalServiceResponse additionalServiceDto = _mapper.Map<GetByIdAdditionalServiceResponse>(additionalService);
            return additionalServiceDto;
        }
    }
}