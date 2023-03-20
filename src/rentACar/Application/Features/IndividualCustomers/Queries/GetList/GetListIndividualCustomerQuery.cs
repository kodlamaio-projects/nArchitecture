using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.IndividualCustomers.Queries.GetList;

public class GetListIndividualCustomerQuery : IRequest<GetListResponse<GetListIndividualCustomerListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListIndividualCustomerQueryHandler
        : IRequestHandler<GetListIndividualCustomerQuery, GetListResponse<GetListIndividualCustomerListItemDto>>
    {
        private readonly IIndividualCustomerRepository _individualCustomerRepository;
        private readonly IMapper _mapper;

        public GetListIndividualCustomerQueryHandler(IIndividualCustomerRepository individualCustomerRepository, IMapper mapper)
        {
            _individualCustomerRepository = individualCustomerRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListIndividualCustomerListItemDto>> Handle(
            GetListIndividualCustomerQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<IndividualCustomer> individualCustomers = await _individualCustomerRepository.GetListAsync(
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize
            );
            var mappedIndividualCustomerListModel = _mapper.Map<GetListResponse<GetListIndividualCustomerListItemDto>>(individualCustomers);
            return mappedIndividualCustomerListModel;
        }
    }
}
