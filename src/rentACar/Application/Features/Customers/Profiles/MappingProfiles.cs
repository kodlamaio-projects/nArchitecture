using Application.Features.Customers.Commands.Create;
using Application.Features.Customers.Commands.Delete;
using Application.Features.Customers.Commands.Update;
using Application.Features.Customers.Queries.GetById;
using Application.Features.Customers.Queries.GetByUserId;
using Application.Features.Customers.Queries.GetList;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Customers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
        CreateMap<Customer, CreatedCustomerResponse>().ReverseMap();
        CreateMap<Customer, UpdateCustomerCommand>().ReverseMap();
        CreateMap<Customer, UpdatedCustomerResponse>().ReverseMap();
        CreateMap<Customer, DeleteCustomerCommand>().ReverseMap();
        CreateMap<Customer, DeletedCustomerResponse>().ReverseMap();
        CreateMap<Customer, GetByIdCustomerResponse>().ReverseMap();
        CreateMap<Customer, GetByUserIdCustomerQuery>().ReverseMap();
        CreateMap<Customer, GetListCustomerListItemDto>().ReverseMap();
        CreateMap<IPaginate<Customer>, GetListResponse<GetListCustomerListItemDto>>().ReverseMap();
    }
}
