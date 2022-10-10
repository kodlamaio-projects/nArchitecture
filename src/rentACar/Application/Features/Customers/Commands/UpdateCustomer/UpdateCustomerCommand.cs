using Application.Features.Customers.Dtos;
using Application.Features.Customers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Customers.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommand : IRequest<UpdatedCustomerDto>, ISecuredRequest
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public string[] Roles => new[] { Admin, CustomerUpdate };

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, UpdatedCustomerDto>
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

        public async Task<UpdatedCustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer mappedCustomer = _mapper.Map<Customer>(request);
            Customer updatedCustomer = await _customerRepository.UpdateAsync(mappedCustomer);
            UpdatedCustomerDto updatedCustomerDto = _mapper.Map<UpdatedCustomerDto>(updatedCustomer);
            return updatedCustomerDto;
        }
    }
}