using Application.Features.FindeksCreditRates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.FindeksCreditRates.Queries.GetByCustomerIdFindeksCreditRate;

public class GetByCustomerIdFindeksCreditRateQuery : IRequest<GetByCustomerIdFindeksCreditRateResponse>
{
    public int CustomerId { get; set; }

    public class GetByIdFindeksCreditRateQueryHandler
        : IRequestHandler<GetByCustomerIdFindeksCreditRateQuery, GetByCustomerIdFindeksCreditRateResponse>
    {
        private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;
        private readonly IMapper _mapper;
        private readonly FindeksCreditRateBusinessRules _findeksCreditRateBusinessRules;

        public GetByIdFindeksCreditRateQueryHandler(
            IFindeksCreditRateRepository findeksCreditRateRepository,
            FindeksCreditRateBusinessRules findeksCreditRateBusinessRules,
            IMapper mapper
        )
        {
            _findeksCreditRateRepository = findeksCreditRateRepository;
            _findeksCreditRateBusinessRules = findeksCreditRateBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetByCustomerIdFindeksCreditRateResponse> Handle(
            GetByCustomerIdFindeksCreditRateQuery request,
            CancellationToken cancellationToken
        )
        {
            FindeksCreditRate? findeksCreditRate = await _findeksCreditRateRepository.GetAsync(b => b.CustomerId == request.CustomerId);
            await _findeksCreditRateBusinessRules.FindeksCreditShouldBeExist(findeksCreditRate);

            GetByCustomerIdFindeksCreditRateResponse? findeksCreditRateDto = _mapper.Map<GetByCustomerIdFindeksCreditRateResponse>(
                findeksCreditRate
            );
            return findeksCreditRateDto;
        }
    }
}
