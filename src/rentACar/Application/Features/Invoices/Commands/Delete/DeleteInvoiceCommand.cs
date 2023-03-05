using Application.Features.Invoices.Constants;
using Application.Features.Invoices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Invoices.Constants.InvoicesOperationClaims;

namespace Application.Features.Invoices.Commands.Delete;

public class DeleteInvoiceCommand : IRequest<DeletedInvoiceResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, InvoicesOperationClaims.Delete };

    public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, DeletedInvoiceResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;
        private readonly InvoiceBusinessRules _invoiceBusinessRules;

        public DeleteInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IMapper mapper, InvoiceBusinessRules invoiceBusinessRules)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
            _invoiceBusinessRules = invoiceBusinessRules;
        }

        public async Task<DeletedInvoiceResponse> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            await _invoiceBusinessRules.InvoiceIdShouldExistWhenSelected(request.Id);

            Invoice mappedInvoice = _mapper.Map<Invoice>(request);
            Invoice deletedInvoice = await _invoiceRepository.DeleteAsync(mappedInvoice);
            DeletedInvoiceResponse deletedInvoiceDto = _mapper.Map<DeletedInvoiceResponse>(deletedInvoice);
            return deletedInvoiceDto;
        }
    }
}
