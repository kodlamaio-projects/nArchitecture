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
            .ForMember(c => c.CarModelBrandName, opt => opt.MapFrom(c => c.Car.Model.Brand.Name))
            .ForMember(c => c.CarModelName, opt => opt.MapFrom(c => c.Car.Model.Name))
            .ForMember(c => c.CarModelYear, opt => opt.MapFrom(c => c.Car.ModelYear))
            .ForMember(c => c.CarPlate, opt => opt.MapFrom(c => c.Car.Plate))
            .ReverseMap();
        CreateMap<IPaginate<CarDamage>, GetListResponse<GetListCarDamageListItemDto>>().ReverseMap();
        CreateMap<CarDamage, GetListByCarIdCarDamageListItemDto>()
            .ForMember(c => c.CarModelBrandName, opt => opt.MapFrom(c => c.Car.Model.Brand.Name))
            .ForMember(c => c.CarModelName, opt => opt.MapFrom(c => c.Car.Model.Name))
            .ForMember(c => c.CarModelYear, opt => opt.MapFrom(c => c.Car.ModelYear))
            .ForMember(c => c.CarPlate, opt => opt.MapFrom(c => c.Car.Plate))
            .ReverseMap();
        CreateMap<IPaginate<CarDamage>, GetListResponse<GetListByCarIdCarDamageListItemDto>>().ReverseMap();
    }
}