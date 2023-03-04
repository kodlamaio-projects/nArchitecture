using Application.Features.FindeksCreditRates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.FindeksCreditRates.Queries.GetByIdFindeksCreditRate;

public class GetByIdFindeksCreditRateQuery : IRequest<GetByIdFindeksCreditRateResponse>
{
    public int Id { get; set; }

    public class
        GetByIdFindeksCreditRateQueryHandler : IRequestHandler<GetByIdFindeksCreditRateQuery, GetByIdFindeksCreditRateResponse>
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


        public async Task<GetByIdFindeksCreditRateResponse> Handle(GetByIdFindeksCreditRateQuery request,
                                                       CancellationToken cancellationToken)
        {
            await _findeksCreditRateBusinessRules.FindeksCreditRateIdShouldExistWhenSelected(request.Id);

            FindeksCreditRate? findeksCreditRate = await _findeksCreditRateRepository.GetAsync(b => b.Id == request.Id);
            GetByIdFindeksCreditRateResponse findeksCreditRateDto = _mapper.Map<GetByIdFindeksCreditRateResponse>(findeksCreditRate);
            return findeksCreditRateDto;
        }
    }
}