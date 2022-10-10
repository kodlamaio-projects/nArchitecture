using Application.Features.IndividualCustomers.Commands.CreateIndividualCustomer;
using Application.Features.IndividualCustomers.Commands.DeleteIndividualCustomer;
using Application.Features.IndividualCustomers.Commands.UpdateIndividualCustomer;
using Application.Features.IndividualCustomers.Dtos;
using Application.Features.IndividualCustomers.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.IndividualCustomers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<IndividualCustomer, CreateIndividualCustomerCommand>().ReverseMap();
        CreateMap<IndividualCustomer, CreatedIndividualCustomerDto>().ReverseMap();
        CreateMap<IndividualCustomer, UpdateIndividualCustomerCommand>().ReverseMap();
        CreateMap<IndividualCustomer, UpdatedIndividualCustomerDto>().ReverseMap();
        CreateMap<IndividualCustomer, DeleteIndividualCustomerCommand>().ReverseMap();
        CreateMap<IndividualCustomer, DeletedIndividualCustomerDto>().ReverseMap();
        CreateMap<IndividualCustomer, IndividualCustomerDto>().ReverseMap();
        CreateMap<IndividualCustomer, IndividualCustomerListDto>().ReverseMap();
        CreateMap<IPaginate<IndividualCustomer>, IndividualCustomerListModel>().ReverseMap();
    }
}