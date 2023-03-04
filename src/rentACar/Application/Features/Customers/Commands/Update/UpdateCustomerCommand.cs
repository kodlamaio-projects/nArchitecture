using Application.Features.Customers.Constants;
using Application.Features.Customers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Customers.Constants.CustomersOperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Customers.Commands.Update;

public class UpdateCustomerCommand : IRequest<UpdatedCustomerResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, CustomersOperationClaims.Admin, Write, CustomersOperationClaims.Update };

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, UpdatedCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly CustomerBusinessRules _customerBusinessRules;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper,
                                            CustomerBusinessRules customerBusinessRules)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _customerBusinessRules = customerBusinessRules;
        }

        public async Task<UpdatedCustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer mappedCustomer = _mapper.Map<Customer>(request);
            Customer updatedCustomer = await _customerRepository.UpdateAsync(mappedCustomer);
            UpdatedCustomerResponse updatedCustomerDto = _mapper.Map<UpdatedCustomerResponse>(updatedCustomer);
            return updatedCustomerDto;
        }
    }
}