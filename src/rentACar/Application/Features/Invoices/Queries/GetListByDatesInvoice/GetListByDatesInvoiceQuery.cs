using Application.Features.Invoices.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Invoices.Queries.GetListByDatesInvoice;

public class GetListByDatesInvoiceQuery : IRequest<InvoiceListModel>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public class GetListInvoiceByDatesQueryHandler : IRequestHandler<GetListByDatesInvoiceQuery, InvoiceListModel>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public GetListInvoiceByDatesQueryHandler(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        public async Task<InvoiceListModel> Handle(GetListByDatesInvoiceQuery request,
                                                   CancellationToken cancellationToken)
        {
            IPaginate<Invoice> invoices = await _invoiceRepository.GetListAsync(
                                              i => i.CreatedDate >= request.StartDate &&
                                                   i.CreatedDate <= request.EndDate,
                                              include: i =>
                                                  i.Include(i => i.Customer).Include(i => i.Customer.IndividualCustomer)
                                                   .Include(i => i.Customer.CorporateCustomer),
                                              index: request.Page, size: request.PageSize);
            InvoiceListModel mappedInvoices = _mapper.Map<InvoiceListModel>(invoices);
            return mappedInvoices;
        }
    }
}