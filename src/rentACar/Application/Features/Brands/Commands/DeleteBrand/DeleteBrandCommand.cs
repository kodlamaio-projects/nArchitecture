using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;
using static Application.Features.Brands.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Brands.Commands.DeleteBrand;

public class DeleteBrandCommand : IRequest<DeletedBrandDto>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }

    public bool BypassCache { get; }
    public string CacheKey => "brands-list";
    public string[] Roles => new[] { Admin, BrandDelete };

    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, DeletedBrandDto>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly BrandBusinessRules _brandBusinessRules;

        public DeleteBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper,
                                         BrandBusinessRules brandBusinessRules)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _brandBusinessRules = brandBusinessRules;
        }

        public async Task<DeletedBrandDto> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            await _brandBusinessRules.BrandIdShouldExistWhenSelected(request.Id);
            Brand mappedBrand = _mapper.Map<Brand>(request);
            Brand deletedBrand = await _brandRepository.DeleteAsync(mappedBrand);
            DeletedBrandDto deletedBrandDto = _mapper.Map<DeletedBrandDto>(deletedBrand);
            return deletedBrandDto;
        }
    }
}