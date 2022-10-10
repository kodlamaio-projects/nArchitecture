using Application.Features.Invoices.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Invoices.Queries.GetListByCustomerInvoice;

public class GetListByCustomerInvoiceQuery : IRequest<InvoiceListModel>
{
    public int CustomerId { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public class GetListInvoiceByCustomerQueryHandler : IRequestHandler<GetListByCustomerInvoiceQuery, InvoiceListModel>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public GetListInvoiceByCustomerQueryHandler(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        public async Task<InvoiceListModel> Handle(GetListByCustomerInvoiceQuery request,
                                                   CancellationToken cancellationToken)
        {
            IPaginate<Invoice> invoices = await _invoiceRepository.GetListAsync(i => i.CustomerId == request.CustomerId,
                                              index: request.Page,
                                              size: request.PageSize);
            InvoiceListModel mappedInvoiceListModel = _mapper.Map<InvoiceListModel>(invoices);
            return mappedInvoiceListModel;
        }
    }
}