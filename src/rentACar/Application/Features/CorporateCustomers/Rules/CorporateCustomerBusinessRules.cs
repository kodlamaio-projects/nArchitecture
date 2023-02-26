using Application.Features.CorporateCustomers.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.CorporateCustomers.Rules;

public class CorporateCustomerBusinessRules : BaseBusinessRules
{
    private readonly ICorporateCustomerRepository _corporateCustomerRepository;

    public CorporateCustomerBusinessRules(ICorporateCustomerRepository corporateCustomerRepository)
    {
        _corporateCustomerRepository = corporateCustomerRepository;
    }

    public async Task CorporateCustomerIdShouldExistWhenSelected(int id)
    {
        CorporateCustomer? result = await _corporateCustomerRepository.GetAsync(b => b.Id == id, enableTracking: false);
        if (result == null) throw new BusinessException(CorporateCustomersMessages.CorporateCustomerNotExists);
    }

    public Task CorporateCustomerShouldBeExist(CorporateCustomer corporateCustomer)
    {
        if (corporateCustomer is null) throw new BusinessException(CorporateCustomersMessages.CorporateCustomerNotExists);
        return Task.CompletedTask;
    }

    public async Task CorporateCustomerTaxNoCanNotBeDuplicatedWhenInserted(string taxNo)
    {
        IPaginate<CorporateCustomer> result = await _corporateCustomerRepository.GetListAsync(c => c.TaxNo == taxNo, enableTracking: false);
        if (result.Items.Any()) throw new BusinessException(CorporateCustomersMessages.CorporateCustomerTaxNoAlreadyExists);
    }
}