using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.CorporateCustomers.Queries.GetList;

public class GetListCorporateCustomerQuery : IRequest<GetListResponse<GetListCorporateCustomerListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListCorporateCustomerQueryHandler
        : IRequestHandler<GetListCorporateCustomerQuery, GetListResponse<GetListCorporateCustomerListItemDto>>
    {
        private readonly ICorporateCustomerRepository _corporateCustomerRepository;
        private readonly IMapper _mapper;

        public GetListCorporateCustomerQueryHandler(ICorporateCustomerRepository corporateCustomerRepository, IMapper mapper)
        {
            _corporateCustomerRepository = corporateCustomerRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCorporateCustomerListItemDto>> Handle(
            GetListCorporateCustomerQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<CorporateCustomer> corporateCustomers = await _corporateCustomerRepository.GetListAsync(
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize
            );
            var mappedCorporateCustomerListModel = _mapper.Map<GetListResponse<GetListCorporateCustomerListItemDto>>(corporateCustomers);
            return mappedCorporateCustomerListModel;
        }
    }
}
