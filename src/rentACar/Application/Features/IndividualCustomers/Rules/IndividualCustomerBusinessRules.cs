using Application.Features.IndividualCustomers.Constants;
using Application.Features.IndividualCustomers.Dtos;
using Application.Services.PersonService;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.IndividualCustomers.Rules;

public class IndividualCustomerBusinessRules
{
    private readonly IIndividualCustomerRepository _individualCustomerRepository;
    private readonly IPersonService _personService;
    private IMapper _mapper;

    public IndividualCustomerBusinessRules(IIndividualCustomerRepository individualCustomerRepository,
        IPersonService personService, IMapper mapper)
    {
        _individualCustomerRepository = individualCustomerRepository;
        _personService = personService;
        _mapper = mapper;
    }

    public async Task IndividualCustomerIdShouldExistWhenSelected(int id)
    {
        IndividualCustomer? result = await _individualCustomerRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException(IndividualCustomerMessages.IndividualCustomerNotExists);
    }

    public Task IndividualCustomerShouldBeExist(IndividualCustomer? individualCustomer)
    {
        if (individualCustomer is null)
            throw new BusinessException(IndividualCustomerMessages.IndividualCustomerNotExists);
        return Task.CompletedTask;
    }

    public async Task IndividualCustomerNationalIdentityCanNotBeDuplicatedWhenInserted(string nationalIdentity)
    {
        IPaginate<IndividualCustomer> result =
            await _individualCustomerRepository.GetListAsync(c => c.NationalIdentity == nationalIdentity);
        if (result.Items.Any())
            throw new BusinessException(IndividualCustomerMessages.IndividualCustomerNationalIdentityAlreadyExists);
    }

    public async Task IndividualCustomerNationalIdentityMustVerifyWhenInserted(IndividualCustomer individualCustomer)
    {
        CitizenDto citizenDto = _mapper.Map<CitizenDto>(individualCustomer);
        
        var result = await _personService.VerifyNationalId(citizenDto);

        if (!result)
            throw new BusinessException(IndividualCustomerMessages.IndividualCustomerNotVerify);
    }
}