using Application.Features.CorporateCustomers.Dtos;
using Application.Features.CorporateCustomers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.CorporateCustomers.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.CorporateCustomers.Commands.DeleteCorporateCustomer;

public class DeleteCorporateCustomerCommand : IRequest<DeletedCorporateCustomerDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, CorporateCustomersDelete };

    public class DeleteCorporateCustomerCommandHandler : IRequestHandler<DeleteCorporateCustomerCommand,
        DeletedCorporateCustomerDto>
    {
        private readonly ICorporateCustomerRepository _corporateCustomerRepository;
        private readonly IMapper _mapper;
        private readonly CorporateCustomerBusinessRules _corporateCustomerBusinessRules;

        public DeleteCorporateCustomerCommandHandler(ICorporateCustomerRepository corporateCustomerRepository,
                                                     IMapper mapper,
                                                     CorporateCustomerBusinessRules corporateCustomerBusinessRules)
        {
            _corporateCustomerRepository = corporateCustomerRepository;
            _mapper = mapper;
            _corporateCustomerBusinessRules = corporateCustomerBusinessRules;
        }

        public async Task<DeletedCorporateCustomerDto> Handle(DeleteCorporateCustomerCommand request,
                                                              CancellationToken cancellationToken)
        {
            await _corporateCustomerBusinessRules.CorporateCustomerIdShouldExistWhenSelected(request.Id);

            CorporateCustomer mappedCorporateCustomer = _mapper.Map<CorporateCustomer>(request);
            CorporateCustomer deletedCorporateCustomer =
                await _corporateCustomerRepository.DeleteAsync(mappedCorporateCustomer);
            DeletedCorporateCustomerDto deletedCorporateCustomerDto =
                _mapper.Map<DeletedCorporateCustomerDto>(deletedCorporateCustomer);
            return deletedCorporateCustomerDto;
        }
    }
}