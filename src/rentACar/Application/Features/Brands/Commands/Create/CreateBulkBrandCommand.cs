using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.Create;

public class CreateBulkBrandCommand : IRequest<List<CreatedBrandResponse>>
{
    public List<string> NameList { get; set; }

    public class CreateBulkBrandCommandHandler : IRequestHandler<CreateBulkBrandCommand, List<CreatedBrandResponse>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly BrandBusinessRules _brandBusinessRules;

        public CreateBulkBrandCommandHandler(IBrandRepository brandRepository, BrandBusinessRules brandBusinessRules)
        {
            _brandRepository = brandRepository;
            _brandBusinessRules = brandBusinessRules;
        }

        public async Task<List<CreatedBrandResponse>> Handle(CreateBulkBrandCommand request, CancellationToken cancellationToken)
        {
            if (request.NameList == null || request.NameList.Count == 0)
                await _brandBusinessRules.BrandNameListCanNotBeDuplicatedWhenInserted(request.NameList);

            List<Brand> mappedListBrand = request.NameList
                .Select(
                    x =>
                        new Brand
                        {
                            Name = x,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        }
                )
                .ToList();
            IList<Brand> createdListBrand = await _brandRepository.AddRangeAsync(mappedListBrand);
            List<CreatedBrandResponse> result = createdListBrand
                .Select(x => new CreatedBrandResponse { Id = x.Id, Name = x.Name })
                .ToList();
            return result;
        }
    }
}
