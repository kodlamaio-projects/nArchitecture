using Application.Features.IndividualCustomers.Constants;
using Application.Features.IndividualCustomers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.IndividualCustomers.Constants.IndividualCustomersOperationClaims;

namespace Application.Features.IndividualCustomers.Commands.Update;

public class UpdateIndividualCustomerCommand : IRequest<UpdatedIndividualCustomerResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalIdentity { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, IndividualCustomersOperationClaims.Update };

    public class UpdateIndividualCustomerCommandHandler
        : IRequestHandler<UpdateIndividualCustomerCommand, UpdatedIndividualCustomerResponse>
    {
        private readonly IIndividualCustomerRepository _individualCustomerRepository;
        private readonly IMapper _mapper;
        private readonly IndividualCustomerBusinessRules _individualCustomerBusinessRules;

        public UpdateIndividualCustomerCommandHandler(
            IIndividualCustomerRepository individualCustomerRepository,
            IMapper mapper,
            IndividualCustomerBusinessRules individualCustomerBusinessRules
        )
        {
            _individualCustomerRepository = individualCustomerRepository;
            _mapper = mapper;
            _individualCustomerBusinessRules = individualCustomerBusinessRules;
        }

        public async Task<UpdatedIndividualCustomerResponse> Handle(
            UpdateIndividualCustomerCommand request,
            CancellationToken cancellationToken
        )
        {
            await _individualCustomerBusinessRules.IndividualCustomerNationalIdentityCanNotBeDuplicatedWhenInserted(
                request.NationalIdentity
            );

            IndividualCustomer mappedIndividualCustomer = _mapper.Map<IndividualCustomer>(request);
            IndividualCustomer updatedIndividualCustomer = await _individualCustomerRepository.UpdateAsync(mappedIndividualCustomer);
            UpdatedIndividualCustomerResponse updatedIndividualCustomerDto = _mapper.Map<UpdatedIndividualCustomerResponse>(
                updatedIndividualCustomer
            );
            return updatedIndividualCustomerDto;
        }
    }
}
