using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Invoices.Queries.GetListByDates;

public class GetListByDatesInvoiceQuery : IRequest<GetListResponse<GetListByDatesInvoiceListItemDto>>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public class GetListInvoiceByDatesQueryHandler
        : IRequestHandler<GetListByDatesInvoiceQuery, GetListResponse<GetListByDatesInvoiceListItemDto>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public GetListInvoiceByDatesQueryHandler(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByDatesInvoiceListItemDto>> Handle(
            GetListByDatesInvoiceQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Invoice> invoices = await _invoiceRepository.GetListAsync(
                predicate: i => i.CreatedDate >= request.StartDate && i.CreatedDate <= request.EndDate,
                include: i =>
                    i.Include(i => i.Customer).Include(i => i.Customer.IndividualCustomer).Include(i => i.Customer.CorporateCustomer),
                index: request.Page,
                size: request.PageSize
            );
            var mappedInvoices = _mapper.Map<GetListResponse<GetListByDatesInvoiceListItemDto>>(invoices);
            return mappedInvoices;
        }
    }
}
