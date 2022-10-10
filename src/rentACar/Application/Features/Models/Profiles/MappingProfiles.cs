using Application.Features.Models.Commands.CreateModel;
using Application.Features.Models.Commands.DeleteModel;
using Application.Features.Models.Commands.UpdateModel;
using Application.Features.Models.Dtos;
using Application.Features.Models.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Models.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Model, CreateModelCommand>().ReverseMap();
        CreateMap<Model, CreatedModelDto>().ReverseMap();
        CreateMap<Model, UpdateModelCommand>().ReverseMap();
        CreateMap<Model, UpdatedModelDto>().ReverseMap();
        CreateMap<Model, DeleteModelCommand>().ReverseMap();
        CreateMap<Model, DeletedModelDto>().ReverseMap();
        CreateMap<Model, ModelDto>().ReverseMap();
        CreateMap<Model, ModelListDto>().ForMember(c => c.BrandName, opt => opt.MapFrom(c => c.Brand.Name))
                                        .ForMember(c => c.FuelName, opt => opt.MapFrom(c => c.Fuel.Name))
                                        .ForMember(c => c.TransmissionName,
                                                   opt => opt.MapFrom(c => c.Transmission.Name));
        CreateMap<IPaginate<Model>, ModelListModel>().ReverseMap();
    }
}