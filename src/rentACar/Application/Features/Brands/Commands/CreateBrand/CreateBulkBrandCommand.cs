using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public partial class CreateBulkBrandCommand : IRequest<List<CreatedBrandDto>>
    {
        public List<string> NameList { get; set; }

        public class CreateBulkBrandCommandHandler : IRequestHandler<CreateBulkBrandCommand, List<CreatedBrandDto>>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly BrandBusinessRules _brandBusinessRules;

            public CreateBulkBrandCommandHandler(IBrandRepository brandRepository,BrandBusinessRules brandBusinessRules)
            {
                _brandRepository = brandRepository;
                _brandBusinessRules = brandBusinessRules;
            }

            public async Task<List<CreatedBrandDto>> Handle(CreateBulkBrandCommand request, CancellationToken cancellationToken)
            {
                if (request.NameList == null || request.NameList.Count == 0)
                    await _brandBusinessRules.BrandNameListCanNotBeDuplicatedWhenInserted(request.NameList);                

                List<Brand> mappedListBrand = request.NameList.Select(x => new Brand { Name = x, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now }).ToList();                            
                List<Brand> createdListBrand = await _brandRepository.AddRangeAsync(mappedListBrand);                                            
                List<CreatedBrandDto> result = createdListBrand.Select(x => new CreatedBrandDto { Id = x.Id, Name = x.Name }).ToList();                
                return result;

            }
        }
    }

}
