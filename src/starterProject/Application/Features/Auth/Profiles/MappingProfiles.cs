using Application.Features.Auth.Commands.RevokeToken;
using AutoMapper;
using NArchitecture.Core.Security.Entities;

namespace Application.Features.Auth.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RefreshToken<int, int>, RevokedTokenResponse>().ReverseMap();
    }
}
