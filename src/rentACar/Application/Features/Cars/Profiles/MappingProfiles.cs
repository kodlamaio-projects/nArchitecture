using Application.Features.Cars.Commands.Create;
using Application.Features.Cars.Commands.Delete;
using Application.Features.Cars.Commands.DeliverRental;
using Application.Features.Cars.Commands.Maintain;
using Application.Features.Cars.Commands.Update;
using Application.Features.Cars.Queries.GetById;
using Application.Features.Cars.Queries.GetList;
using Application.Features.Cars.Queries.GetListByDynamic;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Cars.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Car, CreateCarCommand>().ReverseMap();
        CreateMap<Car, CreatedCarResponse>().ReverseMap();
        CreateMap<Car, UpdateCarCommand>().ReverseMap();
        CreateMap<Car, UpdatedCarResponse>().ReverseMap();
        CreateMap<Car, DeliveredCarResponse>().ReverseMap();
        CreateMap<Car, MaintainedCarResponse>().ReverseMap();
        CreateMap<Car, DeleteCarCommand>().ReverseMap();
        CreateMap<Car, DeletedCarResponse>().ReverseMap();
        CreateMap<Car, GetByIdCarResponse>().ReverseMap();
        CreateMap<Car, GetListCarListItemDto>().ForMember(c => c.ColorName, opt => opt.MapFrom(c => c.Color.Name))
                                    .ForMember(c => c.ModelName, opt => opt.MapFrom(c => c.Model.Name))
                                    .ForMember(c => c.BrandName, opt => opt.MapFrom(c => c.Model.Brand.Name));
        CreateMap<IPaginate<Car>, GetListResponse<GetListCarListItemDto>>().ReverseMap();
        CreateMap<Car, GetListByDynamicCarListItemDto>().ForMember(c => c.ColorName, opt => opt.MapFrom(c => c.Color.Name))
                                               .ForMember(c => c.ModelName, opt => opt.MapFrom(c => c.Model.Name))
                                               .ForMember(c => c.BrandName, opt => opt.MapFrom(c => c.Model.Brand.Name));
        CreateMap<IPaginate<Car>, GetListResponse<GetListByDynamicCarListItemDto>>().ReverseMap();
    }
}