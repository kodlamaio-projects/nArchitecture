using Application.Features.UserOperationClaims.Commands.Create;
using Application.Features.UserOperationClaims.Commands.Delete;
using Application.Features.UserOperationClaims.Commands.Update;
using Application.Features.UserOperationClaims.Queries.GetById;
using Application.Features.UserOperationClaims.Queries.GetList;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using Riok.Mapperly.Abstractions;

namespace Application.Features.UserOperationClaims.Mappers;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class UserOperationClaimMapper
{
    [MapperIgnoreTarget(nameof(UserOperationClaim.Id))]
    [MapperIgnoreTarget(nameof(UserOperationClaim.CreatedDate))]
    [MapperIgnoreTarget(nameof(UserOperationClaim.UpdatedDate))]
    [MapperIgnoreTarget(nameof(UserOperationClaim.DeletedDate))]
    [MapperIgnoreTarget(nameof(UserOperationClaim.User))]
    [MapperIgnoreTarget(nameof(UserOperationClaim.OperationClaim))]
    public static partial UserOperationClaim ToEntity(CreateUserOperationClaimCommand command);

    public static partial CreatedUserOperationClaimResponse ToCreatedResponse(UserOperationClaim userOperationClaim);

    [MapperIgnoreTarget(nameof(UserOperationClaim.Id))]
    [MapperIgnoreTarget(nameof(UserOperationClaim.CreatedDate))]
    [MapperIgnoreTarget(nameof(UserOperationClaim.UpdatedDate))]
    [MapperIgnoreTarget(nameof(UserOperationClaim.DeletedDate))]
    [MapperIgnoreTarget(nameof(UserOperationClaim.User))]
    [MapperIgnoreTarget(nameof(UserOperationClaim.OperationClaim))]
    public static partial void UpdateEntity(UpdateUserOperationClaimCommand command, UserOperationClaim userOperationClaim);

    public static partial UpdatedUserOperationClaimResponse ToUpdatedResponse(UserOperationClaim userOperationClaim);

    public static partial DeletedUserOperationClaimResponse ToDeletedResponse(UserOperationClaim userOperationClaim);

    public static partial GetByIdUserOperationClaimResponse ToGetByIdResponse(UserOperationClaim userOperationClaim);

    public static partial GetListUserOperationClaimListItemDto ToGetListItemDto(UserOperationClaim userOperationClaim);

    public static partial GetListResponse<GetListUserOperationClaimListItemDto> ToGetListResponse(
        IPaginate<UserOperationClaim> paginate
    );
}
