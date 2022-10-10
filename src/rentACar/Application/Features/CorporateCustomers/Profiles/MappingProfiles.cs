using Application.Features.CorporateCustomers.Commands.CreateCorporateCustomer;
using Application.Features.CorporateCustomers.Commands.DeleteCorporateCustomer;
using Application.Features.CorporateCustomers.Commands.UpdateCorporateCustomer;
using Application.Features.CorporateCustomers.Dtos;
using Application.Features.CorporateCustomers.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.CorporateCustomers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CorporateCustomer, CreateCorporateCustomerCommand>().ReverseMap();
        CreateMap<CorporateCustomer, CreatedCorporateCustomerDto>().ReverseMap();
        CreateMap<CorporateCustomer, UpdateCorporateCustomerCommand>().ReverseMap();
        CreateMap<CorporateCustomer, UpdatedCorporateCustomerDto>().ReverseMap();
        CreateMap<CorporateCustomer, DeleteCorporateCustomerCommand>().ReverseMap();
        CreateMap<CorporateCustomer, DeletedCorporateCustomerDto>().ReverseMap();
        CreateMap<CorporateCustomer, CorporateCustomerDto>().ReverseMap();
        CreateMap<CorporateCustomer, CorporateCustomerListDto>().ReverseMap();
        CreateMap<IPaginate<CorporateCustomer>, CorporateCustomerListModel>().ReverseMap();
    }
}