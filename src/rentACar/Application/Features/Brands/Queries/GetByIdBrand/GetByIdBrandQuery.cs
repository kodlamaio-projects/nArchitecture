using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Queries.GetByIdBrand;

public class GetByIdBrandQuery : IRequest<BrandDto>
{
    public int Id { get; set; }

    public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, BrandDto>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly BrandBusinessRules _brandBusinessRules;

        public GetByIdBrandQueryHandler(IBrandRepository brandRepository, BrandBusinessRules brandBusinessRules,
                                        IMapper mapper)
        {
            _brandRepository = brandRepository;
            _brandBusinessRules = brandBusinessRules;
            _mapper = mapper;
        }


        public async Task<BrandDto> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
        {
            await _brandBusinessRules.BrandIdShouldExistWhenSelected(request.Id);

            Brand? brand = await _brandRepository.GetAsync(b => b.Id == request.Id);
            BrandDto brandDto = _mapper.Map<BrandDto>(brand);
            return brandDto;
        }
    }
}