using Application.Features.CorporateCustomers.Dtos;
using Application.Features.CorporateCustomers.Rules;
using Application.Services.FindeksCreditRateService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.CorporateCustomers.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.CorporateCustomers.Commands.CreateCorporateCustomer;

public class CreateCorporateCustomerCommand : IRequest<CreatedCorporateCustomerDto>, ISecuredRequest
{
    public int CustomerId { get; set; }
    public string CompanyName { get; set; }
    public string TaxNo { get; set; }

    public string[] Roles => new[] { Admin, CorporateCustomerAdmin, CorporateCustomerWrite, CorporateCustomerAdd };

    public class
        CreateCorporateCustomerCommandHandler : IRequestHandler<CreateCorporateCustomerCommand,
            CreatedCorporateCustomerDto>
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

        public async Task<CreatedCorporateCustomerDto> Handle(CreateCorporateCustomerCommand request,
                                                              CancellationToken cancellationToken)
        {
            await _corporateCustomerBusinessRules.CorporateCustomerTaxNoCanNotBeDuplicatedWhenInserted(request.TaxNo);

            CorporateCustomer mappedCorporateCustomer = _mapper.Map<CorporateCustomer>(request);
            CorporateCustomer createdCorporateCustomer =
                await _corporateCustomerRepository.AddAsync(mappedCorporateCustomer);

            await _findeksCreditRateService.Add(new FindeksCreditRate
            { CustomerId = createdCorporateCustomer.CustomerId });

            CreatedCorporateCustomerDto createdCorporateCustomerDto =
                _mapper.Map<CreatedCorporateCustomerDto>(createdCorporateCustomer);
            return createdCorporateCustomerDto;
        }
    }
}