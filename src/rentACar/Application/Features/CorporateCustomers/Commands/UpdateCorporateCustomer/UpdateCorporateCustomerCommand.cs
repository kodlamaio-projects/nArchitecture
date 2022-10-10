using Application.Features.CorporateCustomers.Dtos;
using Application.Features.CorporateCustomers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.CorporateCustomers.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.CorporateCustomers.Commands.UpdateCorporateCustomer;

public class UpdateCorporateCustomerCommand : IRequest<UpdatedCorporateCustomerDto>, ISecuredRequest
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CompanyName { get; set; }
    public string TaxNo { get; set; }

    public string[] Roles => new[] { Admin, CorporateCustomersUpdate };

    public class UpdateCorporateCustomerCommandHandler : IRequestHandler<UpdateCorporateCustomerCommand,
        UpdatedCorporateCustomerDto>
    {
        private readonly ICorporateCustomerRepository _corporateCustomerRepository;
        private readonly IMapper _mapper;
        private readonly CorporateCustomerBusinessRules _corporateCustomerBusinessRules;

        public UpdateCorporateCustomerCommandHandler(ICorporateCustomerRepository corporateCustomerRepository,
                                                     IMapper mapper,
                                                     CorporateCustomerBusinessRules corporateCustomerBusinessRules)
        {
            _corporateCustomerRepository = corporateCustomerRepository;
            _mapper = mapper;
            _corporateCustomerBusinessRules = corporateCustomerBusinessRules;
        }

        public async Task<UpdatedCorporateCustomerDto> Handle(UpdateCorporateCustomerCommand request,
                                                              CancellationToken cancellationToken)
        {
            await _corporateCustomerBusinessRules.CorporateCustomerTaxNoCanNotBeDuplicatedWhenInserted(request.TaxNo);

            CorporateCustomer mappedCorporateCustomer = _mapper.Map<CorporateCustomer>(request);
            CorporateCustomer updatedCorporateCustomer =
                await _corporateCustomerRepository.UpdateAsync(mappedCorporateCustomer);
            UpdatedCorporateCustomerDto updatedCorporateCustomerDto =
                _mapper.Map<UpdatedCorporateCustomerDto>(updatedCorporateCustomer);
            return updatedCorporateCustomerDto;
        }
    }
}