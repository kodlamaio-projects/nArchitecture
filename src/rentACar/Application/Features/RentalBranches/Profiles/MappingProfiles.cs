using Application.Features.RentalBranches.Commands.CreateRentalBranch;
using Application.Features.RentalBranches.Commands.DeleteRentalBranch;
using Application.Features.RentalBranches.Commands.UpdateRentalBranch;
using Application.Features.RentalBranches.Dtos;
using Application.Features.RentalBranches.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.RentalBranches.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RentalBranch, CreateRentalBranchCommand>().ReverseMap();
        CreateMap<RentalBranch, CreatedRentalBranchDto>().ReverseMap();
        CreateMap<RentalBranch, UpdateRentalBranchCommand>().ReverseMap();
        CreateMap<RentalBranch, UpdatedRentalBranchDto>().ReverseMap();
        CreateMap<RentalBranch, DeleteRentalBranchCommand>().ReverseMap();
        CreateMap<RentalBranch, DeletedRentalBranchDto>().ReverseMap();
        CreateMap<RentalBranch, RentalBranchDto>().ReverseMap();
        CreateMap<RentalBranch, RentalBranchListDto>().ReverseMap();
        CreateMap<IPaginate<RentalBranch>, RentalBranchListModel>().ReverseMap();
    }
}