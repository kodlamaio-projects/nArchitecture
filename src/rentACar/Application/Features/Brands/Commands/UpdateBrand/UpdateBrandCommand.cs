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

namespace Application.Features.Brands.Commands.UpdateBrand;

public class UpdateBrandCommand : IRequest<UpdatedBrandDto>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public bool BypassCache { get; }
    public string CacheKey => "brands-list";
    public string[] Roles => new[] { Admin, BrandAdmin, BrandWrite, BrandUpdate };

    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, UpdatedBrandDto>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly BrandBusinessRules _brandBusinessRules;

        public UpdateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper,
                                         BrandBusinessRules brandBusinessRules)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _brandBusinessRules = brandBusinessRules;
        }

        public async Task<UpdatedBrandDto> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            await _brandBusinessRules.BrandIdShouldExistWhenSelected(request.Id);
            Brand mappedBrand = _mapper.Map<Brand>(request);
            Brand updatedBrand = await _brandRepository.UpdateAsync(mappedBrand);
            UpdatedBrandDto updatedBrandDto = _mapper.Map<UpdatedBrandDto>(updatedBrand);
            return updatedBrandDto;
        }
    }
}