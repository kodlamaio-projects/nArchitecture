using Application.Features.CorporateCustomers.Constants;
using Application.Features.CorporateCustomers.Rules;
using Application.Services.FindeksCreditRateService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.CorporateCustomers.Constants.CorporateCustomersOperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.CorporateCustomers.Commands.Create;

public class CreateCorporateCustomerCommand : IRequest<CreatedCorporateCustomerResponse>, ISecuredRequest
{
    public int CustomerId { get; set; }
    public string CompanyName { get; set; }
    public string TaxNo { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, CorporateCustomersOperationClaims.Admin, Write, Add };

    public class
        CreateCorporateCustomerCommandHandler : IRequestHandler<CreateCorporateCustomerCommand,
            CreatedCorporateCustomerResponse>
    {
        private readonly ICorporateCustomerRepository _corporateCustomerRepository;
        private readonly IMapper _mapper;
        private readonly CorporateCustomerBusinessRules _corporateCustomerBusinessRules;
        private readonly IFindeksCreditRateService _findeksCreditRateService;

        public CreateCorporateCustomerCommandHandler(ICorporateCustomerRepository corporateCustomerRepository,
                                                     IMapper mapper,
                                                     CorporateCustomerBusinessRules corporateCustomerBusinessRules,
                                                     IFindeksCreditRateService findeksCreditRateService)
        {
            _corporateCustomerRepository = corporateCustomerRepository;
            _mapper = mapper;
            _corporateCustomerBusinessRules = corporateCustomerBusinessRules;
            _findeksCreditRateService = findeksCreditRateService;
        }

        public async Task<CreatedCorporateCustomerResponse> Handle(CreateCorporateCustomerCommand request,
                                                              CancellationToken cancellationToken)
        {
            await _corporateCustomerBusinessRules.CorporateCustomerTaxNoCanNotBeDuplicatedWhenInserted(request.TaxNo);

            CorporateCustomer mappedCorporateCustomer = _mapper.Map<CorporateCustomer>(request);
            CorporateCustomer createdCorporateCustomer =
                await _corporateCustomerRepository.AddAsync(mappedCorporateCustomer);

            await _findeksCreditRateService.Add(new FindeksCreditRate
            { CustomerId = createdCorporateCustomer.CustomerId });

            CreatedCorporateCustomerResponse createdCorporateCustomerDto =
                _mapper.Map<CreatedCorporateCustomerResponse>(createdCorporateCustomer);
            return createdCorporateCustomerDto;
        }
    }
}