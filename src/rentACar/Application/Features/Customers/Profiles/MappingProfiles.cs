using Application.Features.Customers.Commands.CreateCustomer;
using Application.Features.Customers.Commands.DeleteCustomer;
using Application.Features.Customers.Commands.UpdateCustomer;
using Application.Features.Customers.Dtos;
using Application.Features.Customers.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Customers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
        CreateMap<Customer, CreatedCustomerDto>().ReverseMap();
        CreateMap<Customer, UpdateCustomerCommand>().ReverseMap();
        CreateMap<Customer, UpdatedCustomerDto>().ReverseMap();
        CreateMap<Customer, DeleteCustomerCommand>().ReverseMap();
        CreateMap<Customer, DeletedCustomerDto>().ReverseMap();
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<Customer, CustomerListDto>().ReverseMap();
        CreateMap<IPaginate<Customer>, CustomerListModel>().ReverseMap();
    }
}