using Application.Features.CorporateCustomers.Queries.GetByCustomerId;
using Application.Features.IndividualCustomers.Commands.Create;
using Application.Features.IndividualCustomers.Commands.Delete;
using Application.Features.IndividualCustomers.Commands.Update;
using Application.Features.IndividualCustomers.Queries.GetById;
using Application.Features.IndividualCustomers.Queries.GetList;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.IndividualCustomers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<IndividualCustomer, CreateIndividualCustomerCommand>().ReverseMap();
        CreateMap<IndividualCustomer, CreatedIndividualCustomerResponse>().ReverseMap();
        CreateMap<IndividualCustomer, UpdateIndividualCustomerCommand>().ReverseMap();
        CreateMap<IndividualCustomer, UpdatedIndividualCustomerResponse>().ReverseMap();
        CreateMap<IndividualCustomer, DeleteIndividualCustomerCommand>().ReverseMap();
        CreateMap<IndividualCustomer, DeletedIndividualCustomerResponse>().ReverseMap();
        CreateMap<IndividualCustomer, GetByIdIndividualCustomerResponse>().ReverseMap();
        CreateMap<IndividualCustomer, GetByCustomerIdCorporateCustomerResponse>().ReverseMap();
        CreateMap<IndividualCustomer, GetListIndividualCustomerListItemDto>().ReverseMap();
        CreateMap<IPaginate<IndividualCustomer>, GetListResponse<GetListIndividualCustomerListItemDto>>().ReverseMap();
    }
}
