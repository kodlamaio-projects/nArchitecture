using Application.Features.Models.Commands.Create;
using Application.Features.Models.Commands.Delete;
using Application.Features.Models.Commands.Update;
using Application.Features.Models.Queries.GetById;
using Application.Features.Models.Queries.GetList;
using Application.Features.Models.Queries.GetListByDynamic;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Models.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Model, CreateModelCommand>().ReverseMap();
        CreateMap<Model, CreatedModelResponse>().ReverseMap();
        CreateMap<Model, UpdateModelCommand>().ReverseMap();
        CreateMap<Model, UpdatedModelResponse>().ReverseMap();
        CreateMap<Model, DeleteModelCommand>().ReverseMap();
        CreateMap<Model, DeletedModelResponse>().ReverseMap();
        CreateMap<Model, GetByIdModelResponse>().ReverseMap();
        CreateMap<Model, GetListModelListItemDto>().ForMember(c => c.BrandName, opt => opt.MapFrom(c => c.Brand.Name))
                                        .ForMember(c => c.FuelName, opt => opt.MapFrom(c => c.Fuel.Name))
                                        .ForMember(c => c.TransmissionName,
                                                   opt => opt.MapFrom(c => c.Transmission.Name));
        CreateMap<IPaginate<Model>, GetListResponse<GetListModelListItemDto>>().ReverseMap();
        CreateMap<Model, GetListByDynamicModelListItemDto>().ForMember(c => c.BrandName, opt => opt.MapFrom(c => c.Brand.Name))
                                                            .ForMember(c => c.FuelName, opt => opt.MapFrom(c => c.Fuel.Name))
                                                            .ForMember(c => c.TransmissionName,
                                                                       opt => opt.MapFrom(c => c.Transmission.Name));
        CreateMap<IPaginate<Model>, GetListResponse<GetListByDynamicModelListItemDto>>().ReverseMap();
    }
}