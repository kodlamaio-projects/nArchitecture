using Application.Features.FindeksCreditRates.Commands.CreateFindeksCreditRate;
using Application.Features.FindeksCreditRates.Commands.DeleteFindeksCreditRate;
using Application.Features.FindeksCreditRates.Commands.UpdateFindeksCreditRate;
using Application.Features.FindeksCreditRates.Commands.UpdateFindeksCreditRateFromService;
using Application.Features.FindeksCreditRates.Dtos;
using Application.Features.FindeksCreditRates.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.FindeksCreditRates.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<FindeksCreditRate, CreateFindeksCreditRateCommand>().ReverseMap();
        CreateMap<FindeksCreditRate, CreatedFindeksCreditRateDto>().ReverseMap();
        CreateMap<FindeksCreditRate, UpdateFindeksCreditRateCommand>().ReverseMap();
        CreateMap<FindeksCreditRate, UpdatedFindeksCreditRateDto>().ReverseMap();
        CreateMap<FindeksCreditRate, UpdateFindeksCreditRateFromServiceCommand>().ReverseMap();
        CreateMap<FindeksCreditRate, DeleteFindeksCreditRateCommand>().ReverseMap();
        CreateMap<FindeksCreditRate, DeletedFindeksCreditRateDto>().ReverseMap();
        CreateMap<FindeksCreditRate, FindeksCreditRateDto>().ReverseMap();
        CreateMap<FindeksCreditRate, FindeksCreditRateListDto>().ReverseMap();
        CreateMap<IPaginate<FindeksCreditRate>, FindeksCreditRateListModel>().ReverseMap();
    }
}