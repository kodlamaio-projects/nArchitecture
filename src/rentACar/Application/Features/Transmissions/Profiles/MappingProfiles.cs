using Application.Features.Transmissions.Commands.Create;
using Application.Features.Transmissions.Commands.Delete;
using Application.Features.Transmissions.Commands.Update;
using Application.Features.Transmissions.Queries.GetById;
using Application.Features.Transmissions.Queries.GetList;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Transmissions.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Transmission, CreateTransmissionCommand>().ReverseMap();
        CreateMap<Transmission, CreatedTransmissionResponse>().ReverseMap();
        CreateMap<Transmission, UpdateTransmissionCommand>().ReverseMap();
        CreateMap<Transmission, UpdatedTransmissionResponse>().ReverseMap();
        CreateMap<Transmission, DeleteTransmissionCommand>().ReverseMap();
        CreateMap<Transmission, DeletedTransmissionResponse>().ReverseMap();
        CreateMap<Transmission, GetByIdTransmissionResponse>().ReverseMap();
        CreateMap<Transmission, GetListTransmissionListItemDto>().ReverseMap();
        CreateMap<IPaginate<Transmission>, GetListResponse<GetListTransmissionListItemDto>>().ReverseMap();
    }
}