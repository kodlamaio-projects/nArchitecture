using Application.Features.IndividualCustomers.Constants;
using Application.Features.IndividualCustomers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.IndividualCustomers.Constants.IndividualCustomersOperationClaims;

namespace Application.Features.IndividualCustomers.Commands.Delete;

public class DeleteIndividualCustomerCommand : IRequest<DeletedIndividualCustomerResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, IndividualCustomersOperationClaims.Delete };

    public class DeleteIndividualCustomerCommandHandler
        : IRequestHandler<DeleteIndividualCustomerCommand, DeletedIndividualCustomerResponse>
    {
        private readonly IIndividualCustomerRepository _individualCustomerRepository;
        private readonly IMapper _mapper;
        private readonly IndividualCustomerBusinessRules _individualCustomerBusinessRules;

        public DeleteIndividualCustomerCommandHandler(
            IIndividualCustomerRepository individualCustomerRepository,
            IMapper mapper,
            IndividualCustomerBusinessRules individualCustomerBusinessRules
        )
        {
            _individualCustomerRepository = individualCustomerRepository;
            _mapper = mapper;
            _individualCustomerBusinessRules = individualCustomerBusinessRules;
        }

        public async Task<DeletedIndividualCustomerResponse> Handle(
            DeleteIndividualCustomerCommand request,
            CancellationToken cancellationToken
        )
        {
            await _individualCustomerBusinessRules.IndividualCustomerIdShouldExistWhenSelected(request.Id);

            IndividualCustomer mappedIndividualCustomer = _mapper.Map<IndividualCustomer>(request);
            IndividualCustomer deletedIndividualCustomer = await _individualCustomerRepository.DeleteAsync(mappedIndividualCustomer);
            DeletedIndividualCustomerResponse deletedIndividualCustomerDto = _mapper.Map<DeletedIndividualCustomerResponse>(
                deletedIndividualCustomer
            );
            return deletedIndividualCustomerDto;
        }
    }
}
