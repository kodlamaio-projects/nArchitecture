using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Commands.UpdateFromAuth;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.GetList;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using Riok.Mapperly.Abstractions;

namespace Application.Features.Users.Mappers;

/// <summary>
/// <see cref="User"/> only persists the e-mail and the credentials; the name and status fields on the
/// commands and responses are therefore not mapped. Add them to the entity to start carrying them.
/// </summary>
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class UserMapper
{
    public static User ToEntity(CreateUserCommand command) => new() { Email = command.Email };

    public static void UpdateEntity(UpdateUserCommand command, User user) => user.Email = command.Email;

    [MapperIgnoreTarget(nameof(CreatedUserResponse.FirstName))]
    [MapperIgnoreTarget(nameof(CreatedUserResponse.LastName))]
    [MapperIgnoreTarget(nameof(CreatedUserResponse.Status))]
    public static partial CreatedUserResponse ToCreatedResponse(User user);

    [MapperIgnoreTarget(nameof(UpdatedUserResponse.FirstName))]
    [MapperIgnoreTarget(nameof(UpdatedUserResponse.LastName))]
    [MapperIgnoreTarget(nameof(UpdatedUserResponse.Status))]
    public static partial UpdatedUserResponse ToUpdatedResponse(User user);

    [MapperIgnoreTarget(nameof(UpdatedUserFromAuthResponse.FirstName))]
    [MapperIgnoreTarget(nameof(UpdatedUserFromAuthResponse.LastName))]
    [MapperIgnoreTarget(nameof(UpdatedUserFromAuthResponse.AccessToken))]
    public static partial UpdatedUserFromAuthResponse ToUpdatedFromAuthResponse(User user);

    public static partial DeletedUserResponse ToDeletedResponse(User user);

    [MapperIgnoreTarget(nameof(GetByIdUserResponse.FirstName))]
    [MapperIgnoreTarget(nameof(GetByIdUserResponse.LastName))]
    [MapperIgnoreTarget(nameof(GetByIdUserResponse.Status))]
    public static partial GetByIdUserResponse ToGetByIdResponse(User user);

    [MapperIgnoreTarget(nameof(GetListUserListItemDto.FirstName))]
    [MapperIgnoreTarget(nameof(GetListUserListItemDto.LastName))]
    [MapperIgnoreTarget(nameof(GetListUserListItemDto.Status))]
    public static partial GetListUserListItemDto ToGetListItemDto(User user);

    public static partial GetListResponse<GetListUserListItemDto> ToGetListResponse(IPaginate<User> paginate);
}
