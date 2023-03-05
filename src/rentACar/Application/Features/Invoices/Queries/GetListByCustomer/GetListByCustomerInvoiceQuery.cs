using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Invoices.Queries.GetListByCustomer;

public class GetListByCustomerInvoiceQuery : IRequest<GetListResponse<GetListByCustomerInvoiceListItemDto>>
{
    public int CustomerId { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public class GetListInvoiceByCustomerQueryHandler
        : IRequestHandler<GetListByCustomerInvoiceQuery, GetListResponse<GetListByCustomerInvoiceListItemDto>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public GetListInvoiceByCustomerQueryHandler(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByCustomerInvoiceListItemDto>> Handle(
            GetListByCustomerInvoiceQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Invoice> invoices = await _invoiceRepository.GetListAsync(
                predicate: i => i.CustomerId == request.CustomerId,
                index: request.Page,
                size: request.PageSize
            );
            var mappedInvoiceListModel = _mapper.Map<GetListResponse<GetListByCustomerInvoiceListItemDto>>(invoices);
            return mappedInvoiceListModel;
        }
    }
}
