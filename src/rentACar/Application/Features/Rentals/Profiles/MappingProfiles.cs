using Application.Features.Rentals.Commands.CreateRental;
using Application.Features.Rentals.Commands.DeleteRental;
using Application.Features.Rentals.Commands.UpdateRental;
using Application.Features.Rentals.Dtos;
using Application.Features.Rentals.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Rentals.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Rental, CreateRentalCommand>().ReverseMap();
        CreateMap<Rental, CreatedRentalDto>().ReverseMap();
        CreateMap<Rental, UpdateRentalCommand>().ReverseMap();
        CreateMap<Rental, UpdatedRentalDto>().ReverseMap();
        CreateMap<Rental, DeleteRentalCommand>().ReverseMap();
        CreateMap<Rental, DeletedRentalDto>().ReverseMap();
        CreateMap<Rental, RentalDto>();
        CreateMap<Rental, RentalListDto>()
            .ForMember(r => r.CarModelBrandName, opt => opt.MapFrom(r => r.Car.Model.Brand.Name))
            .ForMember(r => r.CarModelName, opt => opt.MapFrom(r => r.Car.Model.Name))
            .ForMember(r => r.CustomerFullName,
                       opt => opt.MapFrom(
                           r =>
                               r.Customer.IndividualCustomer != null
                                   ? $"{r.Customer.IndividualCustomer.FirstName} {r.Customer.IndividualCustomer.FirstName}"
                                   : r.Customer.CorporateCustomer.CompanyName))
            .ReverseMap();
        CreateMap<IPaginate<Rental>, RentalListModel>().ReverseMap();
    }
}