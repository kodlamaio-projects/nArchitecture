using Application.Features.IndividualCustomers.Dtos;
using Application.Features.IndividualCustomers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.IndividualCustomers.Queries.GetByIdIndividualCustomer;

public class GetByIdIndividualCustomerQuery : IRequest<IndividualCustomerDto>
{
    public int Id { get; set; }

    public class
        GetByIdIndividualCustomerQueryHandler : IRequestHandler<GetByIdIndividualCustomerQuery, IndividualCustomerDto>
    {
        private readonly IIndividualCustomerRepository _individualCustomerRepository;
        private readonly IMapper _mapper;
        private readonly IndividualCustomerBusinessRules _individualCustomerBusinessRules;

        public GetByIdIndividualCustomerQueryHandler(IIndividualCustomerRepository individualCustomerRepository,
                                                     IndividualCustomerBusinessRules individualCustomerBusinessRules,
                                                     IMapper mapper)
        {
            _individualCustomerRepository = individualCustomerRepository;
            _individualCustomerBusinessRules = individualCustomerBusinessRules;
            _mapper = mapper;
        }


        public async Task<IndividualCustomerDto> Handle(GetByIdIndividualCustomerQuery request,
                                                        CancellationToken cancellationToken)
        {
            IndividualCustomer? individualCustomer =
                await _individualCustomerRepository.GetAsync(b => b.Id == request.Id);
            await _individualCustomerBusinessRules.IndividualCustomerShouldBeExist(individualCustomer);
            IndividualCustomerDto individualCustomerDto = _mapper.Map<IndividualCustomerDto>(individualCustomer);
            return individualCustomerDto;
        }
    }
}