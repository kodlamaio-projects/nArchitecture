using Application.Features.CorporateCustomers.Dtos;
using Application.Features.CorporateCustomers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CorporateCustomers.Queries.GetByIdCorporateCustomer;

public class GetByIdCorporateCustomerQuery : IRequest<CorporateCustomerDto>
{
    public int Id { get; set; }

    public class
        GetByIdCorporateCustomerQueryHandler : IRequestHandler<GetByIdCorporateCustomerQuery, CorporateCustomerDto>
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


        public async Task<CorporateCustomerDto> Handle(GetByIdCorporateCustomerQuery request,
                                                       CancellationToken cancellationToken)
        {
            CorporateCustomer? corporateCustomer =
                await _corporateCustomerRepository.GetAsync(b => b.Id == request.Id);
            await _corporateCustomerBusinessRules.CorporateCustomerShouldBeExist(corporateCustomer);
            CorporateCustomerDto corporateCustomerDto = _mapper.Map<CorporateCustomerDto>(corporateCustomer);
            return corporateCustomerDto;
        }
    }
}