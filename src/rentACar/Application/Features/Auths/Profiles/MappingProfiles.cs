using Application.Features.Auths.Dtos;
using AutoMapper;
using Core.Domain.Security.Entities;

namespace Application.Features.Auths.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RefreshToken, RevokedTokenDto>().ReverseMap();
    }
}