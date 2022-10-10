using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Profiles;
using Application.Features.Brands.Queries.GetListBrand;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using Application.Tests.Mocks.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Application.Features.Brands.Commands.CreateBrand.CreateBrandCommand;
using static Application.Features.Brands.Queries.GetListBrand.GetListBrandQuery;

namespace Application.Tests.FeaturesTests.Brands
{
    public class BrandsTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IBrandRepository> _mockBrandRepository;
        private readonly BrandBusinessRules _brandBusinessRules;

        public BrandsTests()
        {
            _mockBrandRepository = MockBrandRepository.GetBrandRepository();

            _brandBusinessRules = new BrandBusinessRules(_mockBrandRepository.Object);

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfiles>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task AddBrandWhenNotDuplicated()
        {
            CreateBrandCommandHandler handler = new CreateBrandCommandHandler(_mockBrandRepository.Object, _mapper, _brandBusinessRules);
            CreateBrandCommand command = new CreateBrandCommand();
            command.Name = "Audi";

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal("Audi", result.Name);

        }

        [Fact]
        public async Task AddBrandWhenDuplicated()
        {
            CreateBrandCommandHandler handler = new CreateBrandCommandHandler(_mockBrandRepository.Object, _mapper, _brandBusinessRules);
            CreateBrandCommand command = new CreateBrandCommand();
            command.Name = "BMW";

            await Assert.ThrowsAsync<BusinessException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task GetAllBrands()
        {
            GetListBrandQueryHandler handler = new GetListBrandQueryHandler(_mockBrandRepository.Object, _mapper);
            GetListBrandQuery query = new GetListBrandQuery();
            query.PageRequest = new PageRequest
            {
                Page = 0,
                PageSize = 3
            };

            var result = await handler.Handle(query, CancellationToken.None);
            Assert.Equal(2, result.Items.Count);
        }
    }
}
