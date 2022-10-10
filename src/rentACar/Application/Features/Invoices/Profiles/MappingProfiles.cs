using Application.Features.Invoices.Commands.CreateInvoice;
using Application.Features.Invoices.Commands.DeleteInvoice;
using Application.Features.Invoices.Commands.UpdateInvoice;
using Application.Features.Invoices.Dtos;
using Application.Features.Invoices.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Invoices.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Invoice, CreateInvoiceCommand>().ReverseMap();
        CreateMap<Invoice, CreatedInvoiceDto>().ReverseMap();
        CreateMap<Invoice, UpdateInvoiceCommand>().ReverseMap();
        CreateMap<Invoice, UpdatedInvoiceDto>().ReverseMap();
        CreateMap<Invoice, DeleteInvoiceCommand>().ReverseMap();
        CreateMap<Invoice, DeletedInvoiceDto>().ReverseMap();
        CreateMap<Invoice, InvoiceListDto>()
            .ForMember(i => i.CustomerName,
                       opt => opt.MapFrom(i => i.Customer.IndividualCustomer != null
                                                   ? $"{i.Customer.IndividualCustomer.FirstName} {i.Customer.IndividualCustomer.LastName}"
                                                   : i.Customer.CorporateCustomer.CompanyName))
            .ReverseMap();
        CreateMap<IPaginate<Invoice>, InvoiceListModel>().ReverseMap();
    }
}