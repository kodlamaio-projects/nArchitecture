using Application.Features.CorporateCustomers.Dtos;
using Application.Features.CorporateCustomers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CorporateCustomers.Queries.GetByCustomerIdCorporateCustomer;

public class GetByCustomerIdCorporateCustomerQuery : IRequest<CorporateCustomerDto>
{
    public int CustomerId { get; set; }

    public class
        GetByIdCorporateCustomerQueryHandler : IRequestHandler<GetByCustomerIdCorporateCustomerQuery,
            CorporateCustomerDto>
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


        public async Task<CorporateCustomerDto> Handle(GetByCustomerIdCorporateCustomerQuery request,
                                                       CancellationToken cancellationToken)
        {
            CorporateCustomer? corporateCustomer =
                await _corporateCustomerRepository.GetAsync(b => b.CustomerId == request.CustomerId);
            await _corporateCustomerBusinessRules.CorporateCustomerShouldBeExist(corporateCustomer);
            CorporateCustomerDto corporateCustomerDto = _mapper.Map<CorporateCustomerDto>(corporateCustomer);
            return corporateCustomerDto;
        }
    }
}