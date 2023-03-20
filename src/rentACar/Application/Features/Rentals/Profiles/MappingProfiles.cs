using Application.Features.Rentals.Commands.Create;
using Application.Features.Rentals.Commands.Delete;
using Application.Features.Rentals.Commands.PickUp;
using Application.Features.Rentals.Commands.Update;
using Application.Features.Rentals.Queries.GetById;
using Application.Features.Rentals.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Rentals.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Rental, CreateRentalCommand>().ReverseMap();
        CreateMap<Rental, CreatedRentalResponse>().ReverseMap();
        CreateMap<Rental, UpdateRentalCommand>().ReverseMap();
        CreateMap<Rental, PickUpRentalResponse>().ReverseMap();
        CreateMap<Rental, DeleteRentalCommand>().ReverseMap();
        CreateMap<Rental, DeletedRentalResponse>().ReverseMap();
        CreateMap<Rental, GetByIdRentalResponse>();
        CreateMap<Rental, GetListRentalListItemDto>()
            .ForMember(destinationMember: r => r.CarModelBrandName, memberOptions: opt => opt.MapFrom(r => r.Car.Model.Brand.Name))
            .ForMember(destinationMember: r => r.CarModelName, memberOptions: opt => opt.MapFrom(r => r.Car.Model.Name))
            .ForMember(
                destinationMember: r => r.CustomerFullName,
                memberOptions: opt =>
                    opt.MapFrom(
                        r =>
                            r.Customer.IndividualCustomer != null
                                ? $"{r.Customer.IndividualCustomer.FirstName} {r.Customer.IndividualCustomer.FirstName}"
                                : r.Customer.CorporateCustomer.CompanyName
                    )
            )
            .ReverseMap();
        CreateMap<IPaginate<Rental>, GetListResponse<GetListRentalListItemDto>>().ReverseMap();
    }
}
