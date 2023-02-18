using Application.Features.IndividualCustomers.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.IndividualCustomers.Rules;

public class IndividualCustomerBusinessRules : BaseBusinessRules
{
    private readonly IIndividualCustomerRepository _individualCustomerRepository;

    public IndividualCustomerBusinessRules(IIndividualCustomerRepository individualCustomerRepository)
    {
        _individualCustomerRepository = individualCustomerRepository;
    }

    public async Task IndividualCustomerIdShouldExistWhenSelected(int id)
    {
        IndividualCustomer? result = await _individualCustomerRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException(IndividualCustomerMessages.IndividualCustomerNotExists);
    }

    public Task IndividualCustomerShouldBeExist(IndividualCustomer? individualCustomer)
    {
        if (individualCustomer is null) throw new BusinessException(IndividualCustomerMessages.IndividualCustomerNotExists);
        return Task.CompletedTask;
    }

    public async Task IndividualCustomerNationalIdentityCanNotBeDuplicatedWhenInserted(string nationalIdentity)
    {
        IPaginate<IndividualCustomer> result =
            await _individualCustomerRepository.GetListAsync(c => c.NationalIdentity == nationalIdentity);
        if (result.Items.Any()) throw new BusinessException(IndividualCustomerMessages.IndividualCustomerNationalIdentityAlreadyExists);
    }
}