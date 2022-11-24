using Application.Features.Invoices.Dtos;
using Application.Features.Invoices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Invoices.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Invoices.Commands.CreateInvoice;

public class CreateInvoiceCommand : IRequest<CreatedInvoiceDto>, ISecuredRequest
{
    public int CustomerId { get; set; }
    public string No { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
    public short TotalRentalDate { get; set; }
    public decimal RentalPrice { get; set; }

    public string[] Roles => new[] { Admin, InvoiceAdmin, InvoiceWrite, InvoiceAdd };

    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, CreatedInvoiceDto>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;
        private readonly InvoiceBusinessRules _invoiceBusinessRules;

        public CreateInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IMapper mapper,
                                           InvoiceBusinessRules invoiceBusinessRules)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
            _invoiceBusinessRules = invoiceBusinessRules;
        }

        public async Task<CreatedInvoiceDto> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            Invoice mappedInvoice = _mapper.Map<Invoice>(request);
            Invoice createdInvoice = await _invoiceRepository.AddAsync(mappedInvoice);
            CreatedInvoiceDto createdInvoiceDto = _mapper.Map<CreatedInvoiceDto>(createdInvoice);
            return createdInvoiceDto;
        }
    }
}