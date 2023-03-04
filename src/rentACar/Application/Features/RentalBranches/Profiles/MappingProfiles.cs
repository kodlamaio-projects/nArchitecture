using Application.Features.RentalBranches.Commands.Create;
using Application.Features.RentalBranches.Commands.Delete;
using Application.Features.RentalBranches.Commands.Update;
using Application.Features.RentalBranches.Queries.GetById;
using Application.Features.RentalBranches.Queries.GetList;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.RentalBranches.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RentalBranch, CreateRentalBranchCommand>().ReverseMap();
        CreateMap<RentalBranch, CreatedRentalBranchResponse>().ReverseMap();
        CreateMap<RentalBranch, UpdateRentalBranchCommand>().ReverseMap();
        CreateMap<RentalBranch, UpdatedRentalBranchResponse>().ReverseMap();
        CreateMap<RentalBranch, DeleteRentalBranchCommand>().ReverseMap();
        CreateMap<RentalBranch, DeletedRentalBranchResponse>().ReverseMap();
        CreateMap<RentalBranch, GetByIdRentalBranchResponse>().ReverseMap();
        CreateMap<RentalBranch, GetListRentalBranchListItemDto>().ReverseMap();
        CreateMap<IPaginate<RentalBranch>, GetListResponse<GetListRentalBranchListItemDto>>().ReverseMap();
    }
}