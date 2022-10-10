using Application.Features.Cars.Commands.CreateCar;
using Application.Features.Cars.Commands.DeleteCar;
using Application.Features.Cars.Commands.UpdateCar;
using Application.Features.Cars.Dtos;
using Application.Features.Cars.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Cars.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Car, CreateCarCommand>().ReverseMap();
        CreateMap<Car, CreatedCarDto>().ReverseMap();
        CreateMap<Car, UpdateCarCommand>().ReverseMap();
        CreateMap<Car, UpdatedCarDto>().ReverseMap();
        CreateMap<Car, DeleteCarCommand>().ReverseMap();
        CreateMap<Car, DeletedCarDto>().ReverseMap();
        CreateMap<Car, CarDto>().ReverseMap();
        CreateMap<Car, CarListDto>().ForMember(c => c.ColorName, opt => opt.MapFrom(c => c.Color.Name))
                                    .ForMember(c => c.ModelName, opt => opt.MapFrom(c => c.Model.Name))
                                    .ForMember(c => c.BrandName, opt => opt.MapFrom(c => c.Model.Brand.Name));
        CreateMap<IPaginate<Car>, CarListModel>().ReverseMap();
    }
}