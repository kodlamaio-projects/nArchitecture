using Application.Features.CorporateCustomers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CorporateCustomers.Queries.GetById;

public class GetByIdCorporateCustomerQuery : IRequest<GetByIdCorporateCustomerResponse>
{
    public int Id { get; set; }

    public class
        GetByIdCorporateCustomerQueryHandler : IRequestHandler<GetByIdCorporateCustomerQuery, GetByIdCorporateCustomerResponse>
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


        public async Task<GetByIdCorporateCustomerResponse> Handle(GetByIdCorporateCustomerQuery request,
                                                       CancellationToken cancellationToken)
        {
            CorporateCustomer? corporateCustomer =
                await _corporateCustomerRepository.GetAsync(b => b.Id == request.Id);
            await _corporateCustomerBusinessRules.CorporateCustomerShouldBeExist(corporateCustomer);
            GetByIdCorporateCustomerResponse corporateCustomerDto = _mapper.Map<GetByIdCorporateCustomerResponse>(corporateCustomer);
            return corporateCustomerDto;
        }
    }
}