using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Commands.UpdateFromAuth;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace Application.Features.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User<int, int>, CreateUserCommand>().ReverseMap();
        CreateMap<User<int, int>, CreatedUserResponse>().ReverseMap();
        CreateMap<User<int, int>, UpdateUserCommand>().ReverseMap();
        CreateMap<User<int, int>, UpdatedUserResponse>().ReverseMap();
        CreateMap<User<int, int>, UpdateUserFromAuthCommand>().ReverseMap();
        CreateMap<User<int, int>, UpdatedUserFromAuthResponse>().ReverseMap();
        CreateMap<User<int, int>, DeleteUserCommand>().ReverseMap();
        CreateMap<User<int, int>, DeletedUserResponse>().ReverseMap();
        CreateMap<User<int, int>, GetByIdUserResponse>().ReverseMap();
        CreateMap<User<int, int>, GetListUserListItemDto>().ReverseMap();
        CreateMap<IPaginate<User<int, int>>, GetListResponse<GetListUserListItemDto>>().ReverseMap();
    }
}
