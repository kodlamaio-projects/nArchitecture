using Application.Features.Invoices.Dtos;
using Application.Features.Invoices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Invoices.Queries.GetByIdInvoice;

public class GetByIdInvoiceQuery : IRequest<InvoiceDto>
{
    public int Id { get; set; }

    public class GetByIdInvoiceQueryHandler : IRequestHandler<GetByIdInvoiceQuery, InvoiceDto>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;
        private readonly InvoiceBusinessRules _invoiceBusinessRules;

        public GetByIdInvoiceQueryHandler(IInvoiceRepository invoiceRepository,
                                          InvoiceBusinessRules invoiceBusinessRules, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceBusinessRules = invoiceBusinessRules;
            _mapper = mapper;
        }


        public async Task<InvoiceDto> Handle(GetByIdInvoiceQuery request, CancellationToken cancellationToken)
        {
            await _invoiceBusinessRules.InvoiceIdShouldExistWhenSelected(request.Id);

            Invoice? invoice = await _invoiceRepository.GetAsync(b => b.Id == request.Id);
            InvoiceDto invoiceDto = _mapper.Map<InvoiceDto>(invoice);
            return invoiceDto;
        }
    }
}