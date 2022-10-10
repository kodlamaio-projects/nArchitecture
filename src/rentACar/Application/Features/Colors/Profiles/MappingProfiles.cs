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
        CreateMap<Color, CreateSpeedCommand>().ReverseMap();
        CreateMap<Color, CreatedSpeedDto>().ReverseMap();
        CreateMap<Color, UpdateColorCommand>().ReverseMap();
        CreateMap<Color, UpdatedSpeedDto>().ReverseMap();
        CreateMap<Color, DeleteColorCommand>().ReverseMap();
        CreateMap<Color, DeletedSpeedDto>().ReverseMap();
        CreateMap<Color, SpeedDto>().ReverseMap();
        CreateMap<Color, SpeedListDto>().ReverseMap();
        CreateMap<IPaginate<Color>, SpeedListModel>().ReverseMap();
    }
}