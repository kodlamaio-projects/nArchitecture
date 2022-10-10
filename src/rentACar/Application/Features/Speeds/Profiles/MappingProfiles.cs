using Application.Features.Speeds.Commands.CreateSpeed;
using Application.Features.Speeds.Commands.DeleteSpeed;
using Application.Features.Speeds.Commands.UpdateSpeed;
using Application.Features.Speeds.Dtos;
using Application.Features.Speeds.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Speeds.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Speed, CreateSpeedCommand>().ReverseMap();
        CreateMap<Speed, CreatedSpeedDto>().ReverseMap();
        CreateMap<Speed, UpdateSpeedCommand>().ReverseMap();
        CreateMap<Speed, UpdatedSpeedDto>().ReverseMap();
        CreateMap<Speed, DeleteSpeedCommand>().ReverseMap();
        CreateMap<Speed, DeletedSpeedDto>().ReverseMap();
        CreateMap<Speed, SpeedDto>().ReverseMap();
        CreateMap<Speed, SpeedListDto>().ReverseMap();
        CreateMap<IPaginate<Speed>, SpeedListModel>().ReverseMap();
    }
}