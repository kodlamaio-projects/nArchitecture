using Application.Features.Customers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Customers.Constants.CustomersOperationClaims;

namespace Application.Features.Customers.Commands.Create;

public class CreateCustomerCommand : IRequest<CreatedCustomerResponse>, ISecuredRequest
{
    public int UserId { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, Add };

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreatedCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly CustomerBusinessRules _customerBusinessRules;

        public CreateCustomerCommandHandler(
            ICustomerRepository customerRepository,
            IMapper mapper,
            CustomerBusinessRules customerBusinessRules
        )
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _customerBusinessRules = customerBusinessRules;
        }

        public async Task<CreatedCustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer mappedCustomer = _mapper.Map<Customer>(request);
            Customer createdCustomer = await _customerRepository.AddAsync(mappedCustomer);
            CreatedCustomerResponse createdCustomerDto = _mapper.Map<CreatedCustomerResponse>(createdCustomer);
            return createdCustomerDto;
        }
    }
}
