using Application.Features.CorporateCustomers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CorporateCustomers.Queries.GetByCustomerId;

public class GetByCustomerIdCorporateCustomerQuery : IRequest<GetByCustomerIdCorporateCustomerResponse>
{
    public int CustomerId { get; set; }

    public class
        GetByIdCorporateCustomerQueryHandler : IRequestHandler<GetByCustomerIdCorporateCustomerQuery,
            GetByCustomerIdCorporateCustomerResponse>
    {
        private readonly ICorporateCustomerRepository _corporateCustomerRepository;
        private readonly IMapper _mapper;
        private readonly CorporateCustomerBusinessRules _corporateCustomerBusinessRules;

        public GetByIdCorporateCustomerQueryHandler(ICorporateCustomerRepository corporateCustomerRepository,
                                                    CorporateCustomerBusinessRules corporateCustomerBusinessRules,
                                                    IMapper mapper)
        {
            _corporateCustomerRepository = corporateCustomerRepository;
            _corporateCustomerBusinessRules = corporateCustomerBusinessRules;
            _mapper = mapper;
        }


        public async Task<GetByCustomerIdCorporateCustomerResponse> Handle(GetByCustomerIdCorporateCustomerQuery request,
                                                       CancellationToken cancellationToken)
        {
            CorporateCustomer? corporateCustomer =
                await _corporateCustomerRepository.GetAsync(b => b.CustomerId == request.CustomerId);
            await _corporateCustomerBusinessRules.CorporateCustomerShouldBeExist(corporateCustomer);
            GetByCustomerIdCorporateCustomerResponse corporateCustomerDto = _mapper.Map<GetByCustomerIdCorporateCustomerResponse>(corporateCustomer);
            return corporateCustomerDto;
        }
    }
}