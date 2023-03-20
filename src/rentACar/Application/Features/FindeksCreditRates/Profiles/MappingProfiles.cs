using Application.Features.CorporateCustomers.Queries.GetByCustomerId;
using Application.Features.FindeksCreditRates.Commands.Create;
using Application.Features.FindeksCreditRates.Commands.Delete;
using Application.Features.FindeksCreditRates.Commands.Update;
using Application.Features.FindeksCreditRates.Commands.UpdateByUserIdFromService;
using Application.Features.FindeksCreditRates.Commands.UpdateFromService;
using Application.Features.FindeksCreditRates.Queries.GetByIdFindeksCreditRate;
using Application.Features.FindeksCreditRates.Queries.GetListFindeksCreditRate;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.FindeksCreditRates.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<FindeksCreditRate, CreateFindeksCreditRateCommand>().ReverseMap();
        CreateMap<FindeksCreditRate, CreatedFindeksCreditRateResponse>().ReverseMap();
        CreateMap<FindeksCreditRate, UpdateFindeksCreditRateCommand>().ReverseMap();
        CreateMap<FindeksCreditRate, UpdatedFindeksCreditRateResponse>().ReverseMap();
        CreateMap<FindeksCreditRate, UpdateFindeksCreditRateFromServiceCommand>().ReverseMap();
        CreateMap<FindeksCreditRate, UpdateFindeksCreditRateFromServiceResponse>().ReverseMap();
        CreateMap<FindeksCreditRate, UpdateByUserIdFindeksCreditRateFromServiceCommand>().ReverseMap();
        CreateMap<FindeksCreditRate, UpdateByUserIdFindeksCreditRateFromServiceResponse>().ReverseMap();
        CreateMap<FindeksCreditRate, DeleteFindeksCreditRateCommand>().ReverseMap();
        CreateMap<FindeksCreditRate, DeletedFindeksCreditRateResponse>().ReverseMap();
        CreateMap<FindeksCreditRate, GetByIdFindeksCreditRateResponse>().ReverseMap();
        CreateMap<FindeksCreditRate, GetByCustomerIdCorporateCustomerResponse>().ReverseMap();
        CreateMap<FindeksCreditRate, GetListFindeksCreditRateListItemDto>().ReverseMap();
        CreateMap<IPaginate<FindeksCreditRate>, GetListResponse<GetListFindeksCreditRateListItemDto>>().ReverseMap();
    }
}
