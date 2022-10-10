using Application.Features.Colors.Commands.CreateColor;
using Application.Features.Colors.Commands.DeleteColor;
using Application.Features.Colors.Commands.UpdateColor;
using Application.Features.Colors.Dtos;
using Application.Features.Colors.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Colors.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Color, CreateColorCommand>().ReverseMap();
        CreateMap<Color, CreatedColorDto>().ReverseMap();
        CreateMap<Color, UpdateColorCommand>().ReverseMap();
        CreateMap<Color, UpdatedColorDto>().ReverseMap();
        CreateMap<Color, DeleteColorCommand>().ReverseMap();
        CreateMap<Color, DeletedColorDto>().ReverseMap();
        CreateMap<Color, ColorDto>().ReverseMap();
        CreateMap<Color, ColorListDto>().ReverseMap();
        CreateMap<IPaginate<Color>, ColorListModel>().ReverseMap();
    }
}