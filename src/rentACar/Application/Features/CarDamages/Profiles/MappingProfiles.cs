using Application.Features.CarDamages.Commands.Create;
using Application.Features.CarDamages.Commands.Delete;
using Application.Features.CarDamages.Commands.Update;
using Application.Features.CarDamages.Queries.GetById;
using Application.Features.CarDamages.Queries.GetList;
using Application.Features.CarDamages.Queries.GetListByCarId;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.CarDamages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CarDamage, CreateCarDamageCommand>().ReverseMap();
        CreateMap<CarDamage, CreatedCarDamageResponse>().ReverseMap();
        CreateMap<CarDamage, UpdateCarDamageCommand>().ReverseMap();
        CreateMap<CarDamage, UpdatedCarDamageResponse>().ReverseMap();
        CreateMap<CarDamage, DeleteCarDamageCommand>().ReverseMap();
        CreateMap<CarDamage, DeletedCarDamageResponse>().ReverseMap();
        CreateMap<CarDamage, GetByIdCarDamageResponse>().ReverseMap();
        CreateMap<CarDamage, GetListCarDamageListItemDto>()
            .ForMember(destinationMember: c => c.CarModelBrandName, memberOptions: opt => opt.MapFrom(c => c.Car.Model.Brand.Name))
            .ForMember(destinationMember: c => c.CarModelName, memberOptions: opt => opt.MapFrom(c => c.Car.Model.Name))
            .ForMember(destinationMember: c => c.CarModelYear, memberOptions: opt => opt.MapFrom(c => c.Car.ModelYear))
            .ForMember(destinationMember: c => c.CarPlate, memberOptions: opt => opt.MapFrom(c => c.Car.Plate))
            .ReverseMap();
        CreateMap<IPaginate<CarDamage>, GetListResponse<GetListCarDamageListItemDto>>().ReverseMap();
        CreateMap<CarDamage, GetListByCarIdCarDamageListItemDto>()
            .ForMember(destinationMember: c => c.CarModelBrandName, memberOptions: opt => opt.MapFrom(c => c.Car.Model.Brand.Name))
            .ForMember(destinationMember: c => c.CarModelName, memberOptions: opt => opt.MapFrom(c => c.Car.Model.Name))
            .ForMember(destinationMember: c => c.CarModelYear, memberOptions: opt => opt.MapFrom(c => c.Car.ModelYear))
            .ForMember(destinationMember: c => c.CarPlate, memberOptions: opt => opt.MapFrom(c => c.Car.Plate))
            .ReverseMap();
        CreateMap<IPaginate<CarDamage>, GetListResponse<GetListByCarIdCarDamageListItemDto>>().ReverseMap();
    }
}
