using Application.Features.CorporateCustomers.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.CorporateCustomers.Queries.GetListCorporateCustomer;

public class GetListCorporateCustomerQuery : IRequest<CorporateCustomerListModel>
{
    public PageRequest PageRequest { get; set; }

    public class
        GetListCorporateCustomerQueryHandler : IRequestHandler<GetListCorporateCustomerQuery,
            CorporateCustomerListModel>
    {
        private readonly ICorporateCustomerRepository _corporateCustomerRepository;
        private readonly IMapper _mapper;

        public GetListCorporateCustomerQueryHandler(ICorporateCustomerRepository corporateCustomerRepository,
                                                    IMapper mapper)
        {
            _corporateCustomerRepository = corporateCustomerRepository;
            _mapper = mapper;
        }

        public async Task<CorporateCustomerListModel> Handle(GetListCorporateCustomerQuery request,
                                                             CancellationToken cancellationToken)
        {
            IPaginate<CorporateCustomer> corporateCustomers = await _corporateCustomerRepository.GetListAsync(
                                                                  index: request.PageRequest.Page,
                                                                  size: request.PageRequest.PageSize);
            CorporateCustomerListModel mappedCorporateCustomerListModel =
                _mapper.Map<CorporateCustomerListModel>(corporateCustomers);
            return mappedCorporateCustomerListModel;
        }
    }
}