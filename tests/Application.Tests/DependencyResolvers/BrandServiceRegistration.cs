using Application.Features.Brands.Commands.Create;
using Application.Features.Brands.Commands.Delete;
using Application.Features.Brands.Commands.Update;
using Application.Features.Brands.Queries.GetById;
using Application.Features.Brands.Queries.GetList;
using Application.Tests.Mocks.FakeData;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Tests.DependencyResolvers;

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
