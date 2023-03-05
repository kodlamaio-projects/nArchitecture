using Application.Features.IndividualCustomers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.IndividualCustomers.Queries.GetByCustomerId;

public class GetByCustomerIdIndividualCustomerQuery : IRequest<GetByCustomerIdIndividualCustomerResponse>
{
    public int CustomerId { get; set; }

    public class GetByCustomerIdIndividualCustomerHandler
        : IRequestHandler<GetByCustomerIdIndividualCustomerQuery, GetByCustomerIdIndividualCustomerResponse>
    {
        private readonly IIndividualCustomerRepository _individualCustomerRepository;
        private readonly IMapper _mapper;
        private readonly IndividualCustomerBusinessRules _individualCustomerBusinessRules;

        public GetByCustomerIdIndividualCustomerHandler(
            IIndividualCustomerRepository individualCustomerRepository,
            IndividualCustomerBusinessRules individualCustomerBusinessRules,
            IMapper mapper
        )
        {
            _individualCustomerRepository = individualCustomerRepository;
            _individualCustomerBusinessRules = individualCustomerBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetByCustomerIdIndividualCustomerResponse> Handle(
            GetByCustomerIdIndividualCustomerQuery request,
            CancellationToken cancellationToken
        )
        {
            IndividualCustomer? individualCustomer = await _individualCustomerRepository.GetAsync(b => b.CustomerId == request.CustomerId);
            await _individualCustomerBusinessRules.IndividualCustomerShouldBeExist(individualCustomer);

            GetByCustomerIdIndividualCustomerResponse? individualCustomerDto = _mapper.Map<GetByCustomerIdIndividualCustomerResponse>(
                individualCustomer
            );
            return individualCustomerDto;
        }
    }
}
