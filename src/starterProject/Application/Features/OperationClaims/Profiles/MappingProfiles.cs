using Application.Features.OperationClaims.Commands.Create;
using Application.Features.OperationClaims.Commands.Delete;
using Application.Features.OperationClaims.Commands.Update;
using Application.Features.OperationClaims.Queries.GetById;
using Application.Features.OperationClaims.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using NArchitecture.Core.Security.Entities;

namespace Application.Features.OperationClaims.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<OperationClaim<int, int>, CreateOperationClaimCommand>().ReverseMap();
        CreateMap<OperationClaim<int, int>, CreatedOperationClaimResponse>().ReverseMap();
        CreateMap<OperationClaim<int, int>, UpdateOperationClaimCommand>().ReverseMap();
        CreateMap<OperationClaim<int, int>, UpdatedOperationClaimResponse>().ReverseMap();
        CreateMap<OperationClaim<int, int>, DeleteOperationClaimCommand>().ReverseMap();
        CreateMap<OperationClaim<int, int>, DeletedOperationClaimResponse>().ReverseMap();
        CreateMap<OperationClaim<int, int>, GetByIdOperationClaimResponse>().ReverseMap();
        CreateMap<OperationClaim<int, int>, GetListOperationClaimListItemDto>().ReverseMap();
        CreateMap<IPaginate<OperationClaim<int, int>>, GetListResponse<GetListOperationClaimListItemDto>>().ReverseMap();
    }
}
