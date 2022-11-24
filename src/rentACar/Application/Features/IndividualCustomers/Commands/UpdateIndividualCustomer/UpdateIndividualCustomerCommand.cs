using Application.Features.IndividualCustomers.Dtos;
using Application.Features.IndividualCustomers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.IndividualCustomers.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.IndividualCustomers.Commands.UpdateIndividualCustomer;

public class UpdateIndividualCustomerCommand : IRequest<UpdatedIndividualCustomerDto>, ISecuredRequest
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalIdentity { get; set; }

    public string[] Roles => new[] { Admin, IndividualCustomerAdmin, IndividualCustomerWrite, IndividualCustomerUpdate };

    public class
        UpdateIndividualCustomerCommandHandler : IRequestHandler<UpdateIndividualCustomerCommand,
            UpdatedIndividualCustomerDto>
    {
        private readonly IIndividualCustomerRepository _individualCustomerRepository;
        private readonly IMapper _mapper;
        private readonly IndividualCustomerBusinessRules _individualCustomerBusinessRules;

        public UpdateIndividualCustomerCommandHandler(IIndividualCustomerRepository individualCustomerRepository,
                                                      IMapper mapper,
                                                      IndividualCustomerBusinessRules individualCustomerBusinessRules)
        {
            _individualCustomerRepository = individualCustomerRepository;
            _mapper = mapper;
            _individualCustomerBusinessRules = individualCustomerBusinessRules;
        }

        public async Task<UpdatedIndividualCustomerDto> Handle(UpdateIndividualCustomerCommand request,
                                                               CancellationToken cancellationToken)
        {
            await _individualCustomerBusinessRules.IndividualCustomerNationalIdentityCanNotBeDuplicatedWhenInserted(
                request.NationalIdentity);

            IndividualCustomer mappedIndividualCustomer = _mapper.Map<IndividualCustomer>(request);
            IndividualCustomer updatedIndividualCustomer =
                await _individualCustomerRepository.UpdateAsync(mappedIndividualCustomer);
            UpdatedIndividualCustomerDto updatedIndividualCustomerDto =
                _mapper.Map<UpdatedIndividualCustomerDto>(updatedIndividualCustomer);
            return updatedIndividualCustomerDto;
        }
    }
}