using Application.Features.Customers.Dtos;
using Application.Features.Customers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Customers.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest<CreatedCustomerDto>, ISecuredRequest
{
    public int UserId { get; set; }

    public string[] Roles => new[] { Admin, CustomerAdd };

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreatedCustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly CustomerBusinessRules _customerBusinessRules;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper,
                                            CustomerBusinessRules customerBusinessRules)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _customerBusinessRules = customerBusinessRules;
        }

        public async Task<CreatedCustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer mappedCustomer = _mapper.Map<Customer>(request);
            Customer createdCustomer = await _customerRepository.AddAsync(mappedCustomer);
            CreatedCustomerDto createdCustomerDto = _mapper.Map<CreatedCustomerDto>(createdCustomer);
            return createdCustomerDto;
        }
    }
}