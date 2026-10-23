using Application.Features.OperationClaims.Commands.Create;
using Application.Features.OperationClaims.Commands.Delete;
using Application.Features.OperationClaims.Commands.Update;
using Application.Features.OperationClaims.Queries.GetById;
using Application.Features.OperationClaims.Queries.GetList;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using Riok.Mapperly.Abstractions;

namespace Application.Features.OperationClaims.Mappers;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class OperationClaimMapper
{
    [MapperIgnoreTarget(nameof(OperationClaim.Id))]
    [MapperIgnoreTarget(nameof(OperationClaim.CreatedDate))]
    [MapperIgnoreTarget(nameof(OperationClaim.UpdatedDate))]
    [MapperIgnoreTarget(nameof(OperationClaim.DeletedDate))]
    public static partial OperationClaim ToEntity(CreateOperationClaimCommand command);

    public static partial CreatedOperationClaimResponse ToCreatedResponse(OperationClaim operationClaim);

    [MapperIgnoreTarget(nameof(OperationClaim.Id))]
    [MapperIgnoreTarget(nameof(OperationClaim.CreatedDate))]
    [MapperIgnoreTarget(nameof(OperationClaim.UpdatedDate))]
    [MapperIgnoreTarget(nameof(OperationClaim.DeletedDate))]
    public static partial void UpdateEntity(UpdateOperationClaimCommand command, OperationClaim operationClaim);

    public static partial UpdatedOperationClaimResponse ToUpdatedResponse(OperationClaim operationClaim);

    public static partial DeletedOperationClaimResponse ToDeletedResponse(OperationClaim operationClaim);

    public static partial GetByIdOperationClaimResponse ToGetByIdResponse(OperationClaim operationClaim);

    public static partial GetListOperationClaimListItemDto ToGetListItemDto(OperationClaim operationClaim);

    public static partial GetListResponse<GetListOperationClaimListItemDto> ToGetListResponse(IPaginate<OperationClaim> paginate);
}
