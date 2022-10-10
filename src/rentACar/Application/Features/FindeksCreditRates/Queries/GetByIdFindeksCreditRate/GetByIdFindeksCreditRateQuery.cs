using Application.Features.FindeksCreditRates.Dtos;
using Application.Features.FindeksCreditRates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.FindeksCreditRates.Queries.GetByIdFindeksCreditRate;

public class GetByIdFindeksCreditRateQuery : IRequest<FindeksCreditRateDto>
{
    public int Id { get; set; }

    public class
        GetByIdFindeksCreditRateQueryHandler : IRequestHandler<GetByIdFindeksCreditRateQuery, FindeksCreditRateDto>
    {
        private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;
        private readonly IMapper _mapper;
        private readonly FindeksCreditRateBusinessRules _findeksCreditRateBusinessRules;

        public GetByIdFindeksCreditRateQueryHandler(IFindeksCreditRateRepository findeksCreditRateRepository,
                                                    FindeksCreditRateBusinessRules findeksCreditRateBusinessRules,
                                                    IMapper mapper)
        {
            _findeksCreditRateRepository = findeksCreditRateRepository;
            _findeksCreditRateBusinessRules = findeksCreditRateBusinessRules;
            _mapper = mapper;
        }


        public async Task<FindeksCreditRateDto> Handle(GetByIdFindeksCreditRateQuery request,
                                                       CancellationToken cancellationToken)
        {
            await _findeksCreditRateBusinessRules.FindeksCreditRateIdShouldExistWhenSelected(request.Id);

            FindeksCreditRate? findeksCreditRate = await _findeksCreditRateRepository.GetAsync(b => b.Id == request.Id);
            FindeksCreditRateDto findeksCreditRateDto = _mapper.Map<FindeksCreditRateDto>(findeksCreditRate);
            return findeksCreditRateDto;
        }
    }
}