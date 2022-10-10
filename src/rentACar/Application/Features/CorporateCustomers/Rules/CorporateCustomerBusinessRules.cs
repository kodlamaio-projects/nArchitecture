using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.CorporateCustomers.Rules;

public class CorporateCustomerBusinessRules
{
    private readonly ICorporateCustomerRepository _corporateCustomerRepository;

    public CorporateCustomerBusinessRules(ICorporateCustomerRepository corporateCustomerRepository)
    {
        _corporateCustomerRepository = corporateCustomerRepository;
    }

    public async Task CorporateCustomerIdShouldExistWhenSelected(int id)
    {
        CorporateCustomer? result = await _corporateCustomerRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new NotFoundException("CorporateCustomer not exists.");
    }

    public Task CorporateCustomerShouldBeExist(CorporateCustomer corporateCustomer)
    {
        if (corporateCustomer is null) throw new NotFoundException("CorporateCustomer not exists.");
        return Task.CompletedTask;
    }

    public async Task CorporateCustomerTaxNoCanNotBeDuplicatedWhenInserted(string taxNo)
    {
        IPaginate<CorporateCustomer> result = await _corporateCustomerRepository.GetListAsync(c => c.TaxNo == taxNo);
        if (result.Items.Any()) throw new BadRequestException("Corporate customer tax no already exists.");
    }
}