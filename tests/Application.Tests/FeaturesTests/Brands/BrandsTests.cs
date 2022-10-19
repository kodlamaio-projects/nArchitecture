using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Commands.DeleteBrand;
using Application.Features.Brands.Commands.UpdateBrand;
using Application.Features.Brands.Profiles;
using Application.Features.Brands.Queries.GetByIdBrand;
using Application.Features.Brands.Queries.GetListBrand;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using Application.Tests.Mocks.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Application.Features.Brands.Commands.CreateBrand.CreateBrandCommand;
using static Application.Features.Brands.Commands.DeleteBrand.DeleteBrandCommand;
using static Application.Features.Brands.Commands.UpdateBrand.UpdateBrandCommand;
using static Application.Features.Brands.Queries.GetByIdBrand.GetByIdBrandQuery;
using static Application.Features.Brands.Queries.GetListBrand.GetListBrandQuery;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Tests.FeaturesTests.Brands
{
    public class BrandsTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IBrandRepository> _mockBrandRepository;
        private readonly BrandBusinessRules _brandBusinessRules;

        public BrandsTests()
        {
            _mockBrandRepository =new BrandMockRepository().GetRepository();

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
        public async Task UpdateBrandWhenExistsBrand()
        {
            UpdateBrandCommandHandler handler = new UpdateBrandCommandHandler(_mockBrandRepository.Object, _mapper);
            UpdateBrandCommand command = new UpdateBrandCommand();
            command.Id = 1;
            command.Name = "Opel";

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal("Opel", result.Name);
        }

        [Fact]
        public async Task UpdateBrandWhenNotExistsBrand()
        {
            UpdateBrandCommandHandler handler = new UpdateBrandCommandHandler(_mockBrandRepository.Object, _mapper);
            UpdateBrandCommand command = new UpdateBrandCommand();
            command.Id = 6;
            command.Name = "Opel";

            await Assert.ThrowsAsync<BusinessException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteBrandWhenExistsBrand()
        {
            DeleteBrandCommandHandler handler = new DeleteBrandCommandHandler(_mockBrandRepository.Object, _mapper);
            DeleteBrandCommand command = new DeleteBrandCommand();
            command.Id = 1;

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteBrandWhenNotExistsBrand()
        {
            DeleteBrandCommandHandler handler = new DeleteBrandCommandHandler(_mockBrandRepository.Object, _mapper);
            DeleteBrandCommand command = new DeleteBrandCommand();
            command.Id = 6;

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

        [Fact]
        public async Task GetByIdBrandWhenExistsBrand()
        {
            GetByIdBrandQueryHandler handler = new GetByIdBrandQueryHandler(_mockBrandRepository.Object, _brandBusinessRules, _mapper);
            GetByIdBrandQuery query = new GetByIdBrandQuery();
            query.Id = 1;

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal("Mercedes", result.Name);
        }

        [Fact]
        public async Task GetByIdBrandWhenNotExistsBrand()
        {
            GetByIdBrandQueryHandler handler = new GetByIdBrandQueryHandler(_mockBrandRepository.Object, _brandBusinessRules, _mapper);
            GetByIdBrandQuery query = new GetByIdBrandQuery();
            query.Id = 6;

            await Assert.ThrowsAsync<BusinessException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
