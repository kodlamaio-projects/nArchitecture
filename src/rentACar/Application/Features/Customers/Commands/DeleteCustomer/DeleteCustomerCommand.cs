using Application.Features.Customers.Dtos;
using Application.Features.Customers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Customers.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommand : IRequest<DeletedCustomerDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, CustomerDelete };

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, DeletedCustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly CustomerBusinessRules _customerBusinessRules;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper,
                                            CustomerBusinessRules customerBusinessRules)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _customerBusinessRules = customerBusinessRules;
        }

        public async Task<DeletedCustomerDto> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await _customerBusinessRules.CustomerIdShouldExist(request.Id);

            Customer mappedCustomer = _mapper.Map<Customer>(request);
            Customer deletedCustomer = await _customerRepository.DeleteAsync(mappedCustomer);
            DeletedCustomerDto deletedCustomerDto = _mapper.Map<DeletedCustomerDto>(deletedCustomer);
            return deletedCustomerDto;
        }
    }
}