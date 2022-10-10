using Application.Features.AdditionalServices.Commands.CreateAdditionalService;
using Application.Features.AdditionalServices.Commands.DeleteAdditionalService;
using Application.Features.AdditionalServices.Commands.UpdateAdditionalService;
using Application.Features.AdditionalServices.Dtos;
using Application.Features.AdditionalServices.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.AdditionalServices.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AdditionalService, CreateAdditionalServiceCommand>().ReverseMap();
        CreateMap<AdditionalService, CreatedAdditionalServiceDto>().ReverseMap();
        CreateMap<AdditionalService, UpdateAdditionalServiceCommand>().ReverseMap();
        CreateMap<AdditionalService, UpdatedAdditionalServiceDto>().ReverseMap();
        CreateMap<AdditionalService, DeleteAdditionalServiceCommand>().ReverseMap();
        CreateMap<AdditionalService, DeletedAdditionalServiceDto>().ReverseMap();
        CreateMap<AdditionalService, AdditionalServiceDto>().ReverseMap();
        CreateMap<AdditionalService, AdditionalServiceListDto>().ReverseMap();
        CreateMap<IPaginate<AdditionalService>, AdditionalServiceListModel>().ReverseMap();
    }
}