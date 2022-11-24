using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Commands.DeleteBrand;
using Application.Features.Brands.Commands.UpdateBrand;
using Application.Features.Brands.Queries.GetByIdBrand;
using Application.Features.Brands.Queries.GetListBrand;
using Application.Tests.Mocks.FakeData;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Tests.DependencyResolvers
{
    public static class BrandServiceRegistration
    {
        public static void AddBrandServices(this IServiceCollection services)
        {
            services.AddTransient<BrandFakeData>();
            services.AddTransient<CreateBrandCommand>();
            services.AddTransient<UpdateBrandCommand>();
            services.AddTransient<DeleteBrandCommand>();
            services.AddTransient<GetByIdBrandQuery>();
            services.AddTransient<GetListBrandQuery>();
            services.AddSingleton<CreateBrandCommandValidator>();
            services.AddSingleton<UpdateBrandCommandValidator>();
        }
    }
}
