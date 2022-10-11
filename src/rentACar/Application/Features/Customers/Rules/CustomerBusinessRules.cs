using Application.Features.Customers.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Features.Customers.Rules;

public class CustomerBusinessRules
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerBusinessRules(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task CustomerIdShouldExist(int id)
    {
        Customer? result = await _customerRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException(CustomerExceptionMessages.CustomerNotExistsMessage);
    }

    public Task CustomerShouldBeExist(Customer? customer)
    {
        if (customer is null) throw new BusinessException(CustomerExceptionMessages.CustomerDoNotExistsMessage);
        return Task.CompletedTask;
    }
}